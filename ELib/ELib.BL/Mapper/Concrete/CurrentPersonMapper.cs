﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELib.BL.Mapper.Abstract;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;

namespace ELib.BL.Mapper.Concrete
{
    public class CurrentPersonMapper : IMapper<Person, CurrentPersonDto>
    {
        public Person Map(CurrentPersonDto input)
        {
            if (input == null)
                return null;
            Person result = new Person()
            {
                Id = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                ImageHash = input.ImageHash,
                ApplicationUserId = input.ApplicationUserId,
                State = input.State
            };
            return result;
        }

        public CurrentPersonDto Map(Person input)
        {
            if (input == null)
                return null;
            CurrentPersonDto result = new CurrentPersonDto() {
                Id = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserName = input.ApplicationUser.UserName,
                Email = input.ApplicationUser.Email,
                ImageHash = input.ImageHash,
                ApplicationUserId = input.ApplicationUserId,
                State = input.State
            };
            return result;
        }
    }
}
