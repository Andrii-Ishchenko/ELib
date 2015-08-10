﻿using System;
using System.Collections.Generic;
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
        private IPersonService personservice;
        public CustomMembershipProvider(IPersonService person)
        {
            personservice = person;
        }
        private string applicationName;


        public override bool ValidateUser(string login, string password)
        {
            return personservice.Validate(login, password);
        }

        public override MembershipUser GetUser(string login, bool update)
        {
            var user = personservice.GetByLogin(login);
            if (update && user != null)
                personservice.UpdateLastActivity(user.Login);

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
            if (personservice.CheckPassword(newPassword))
                return personservice.ChangePassword(login, personservice.EncodePassword(oldPassword), personservice.EncodePassword(newPassword), false);
            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return false;
        }

        public override MembershipUser CreateUser(string username, string password, string email,
                                                  string passwordQuestion, string passwordAnswer,
                                                  bool isApproved, object providerUserKey,
                                                  out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
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
