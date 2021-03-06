﻿using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Abstract
{
    public interface ICurrentProfileService : IBaseService<Person,CurrentPersonDto>
    {

        CurrentPersonDto GetByApplicationUserId(string id);
    }
}
