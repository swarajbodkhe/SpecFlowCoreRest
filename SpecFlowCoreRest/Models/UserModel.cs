using System;
using System.Collections.Generic;
using System.Text;

namespace SpecFlowCoreRest.Models
{
    public class UserModel
    {
        public string user { get; set; }
        public string email { get; set; }
        public string culture { get; set; }
        public string timezone { get; set; }

        public UserModel()
        {
            user = null;
            email = null;
            culture = null;
            timezone = null;
        }
    }
}
