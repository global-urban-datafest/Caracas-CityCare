using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FORCOUtils;

namespace CityCare.Models
{
    public partial class CCUser
    {
        public bool EmailExists(IGenericRepository<CCUser> aUserRepository)
        {
            var _User = aUserRepository.GetSingleLazyLoading(aUser => aUser.Email == Email);
            return _User != null;
        }
    }
}