using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityCareWP.Utils
{
    class WebApiRequests
    {
        public static string WebApiAddress
        {
            get { return "http://service-dev.mysonicwallet.com/api/"; }
        }

        public static string PostValidateUser
        {
            get
            {
                return WebApiAddress + "/CCUser/PostValidateUser";
            }
        }


        public static string  PostAddReports
        {
            get
            {
                return WebApiAddress + " CCUser/PostAddReports";
            }
        }   
             


    }
}
