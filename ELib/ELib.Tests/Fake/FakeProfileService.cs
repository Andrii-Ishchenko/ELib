using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using ELib.BL.DtoEntities;

namespace ELib.Tests.Fake
{
    public class FakeProfileService : IProfileService
    {
        public int TotalCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Delete(PersonDto entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public PersonDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public List<PersonDto> GetUsersByArray(int[] id)
        {
            throw new NotImplementedException();
        }

        public PersonDto Insert(PersonDto entity)
        {
            throw new NotImplementedException();
        }

        public void Update(PersonDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
