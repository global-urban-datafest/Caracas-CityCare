using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CityCare.Models;
using FORCOUtils;
using FORCOUtils.DALUtils;

namespace CityCare.Utils
{
    public class SecurityUtils
    {
        protected static IList<CCUser> fAuthUser;
        private static IGenericRepository<CCUser> fUserRepository = new EntityFrameworkDataRepository<CCUser>(new CityCareEntities());

        public static bool IsUserAuthenticated()
        {
            if (HttpContext.Current.Session["Email"] == null || HttpContext.Current.Session["Password"] == null)
                return false;

            var _UserAuthEmail = HttpContext.Current.Session["Email"].ToString();
            var _UserAuthPassword = HttpContext.Current.Session["Password"].ToString();

            fAuthUser = fUserRepository.GetListLazyLoading(aUser => aUser.Email == _UserAuthEmail && aUser.Password == _UserAuthPassword);

            return fAuthUser != null;
        }

    }
}