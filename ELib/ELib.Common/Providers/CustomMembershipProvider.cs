using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using ELib.BL.Services.Abstract;
using ELib.BL.DtoEntities;

namespace ELib.Common.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private IPersonService Personservice { get; set; }
        public CustomMembershipProvider(IPersonService person)
        {
            
            Personservice = person;
        }
        private string applicationName;


        public override bool ValidateUser(string login, string password)
        {
            return Personservice.Validate(login, password);
        }

        public override MembershipUser GetUser(string login, bool update)
        {
            var user = Personservice.GetByLogin(login);
            if (update && user != null)
                Personservice.UpdateLastActivity(user.Login);

            return user == null
                       ? null
                       : new MembershipUser("CustomMembershipProvider", user.Login, user.Id, user.Email, null, user.Phone, true,
                                            false,
                                            user.RegistrationDate,
                                            user.LastActivationDate == null ? user.RegistrationDate : (DateTime)user.LastActivationDate,
                                            user.LastActivationDate == null ? user.RegistrationDate : (DateTime)user.LastActivationDate,
                                            user.LastActivationDate == null ? user.RegistrationDate : (DateTime)user.LastActivationDate,
                                            user.RegistrationDate);
        }


        public override string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }

        public override bool ChangePassword(string login, string oldPassword, string newPassword)
        {
            if (Personservice.CheckPassword(newPassword))
                return Personservice.ChangePassword(login, Personservice.EncodePassword(oldPassword), Personservice.EncodePassword(newPassword), false);
            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return false;
        }

        public override MembershipUser CreateUser(string login, string password, string email,
                                                  string passwordQuestion, string passwordAnswer,
                                                  bool isApproved, object providerUserKey,
                                                  out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public MembershipUser CreateUser(string login, string password, string email,
                                         string firstName, string lastName, string phone,
                                         byte[] photo, byte[] smallPhoto,
                                         out MembershipCreateStatus status)
        {
            try
            {
                if (Personservice.ExistLogin(login))
                {
                    status = MembershipCreateStatus.InvalidUserName;
                    return null;
                }
                if (Personservice.ExistEmail(email))
                {
                    status = MembershipCreateStatus.InvalidEmail;
                    return null;
                }

                if (!Personservice.CheckPassword(password))
                {
                    status = MembershipCreateStatus.InvalidPassword;
                    return null;
                }

                PersonDto user = new PersonDto();
                user.Login = login;
                user.Email = email;
                user.Password = Personservice.EncodePassword(password);
                user.RoleId =1;//!!!!!треба щось краще.
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Phone = phone;
                user.Photo = photo;
                user.SmallPhoto = smallPhoto;
                user.RegistrationDate = DateTime.Now;
                user.LastActivationDate = DateTime.Now;

                Personservice.Insert(user);
                status = MembershipCreateStatus.Success;

                return GetUser(login, false);
            }
            catch
            {
                throw;
            };
        }



        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
    }
}
