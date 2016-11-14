using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Helper
{
    public class RoleName
    {
        public string Get(string Id, string Name)
        {
            switch (Id)
            {
                case "1": return "Admin"; break;
                case "2": return "User"; break;
            }

            switch (Name)
            {
                case "Admin": return "1"; break;
                case "User": return "2"; break;
            }

            return "";
        }
        

        public List<string> List()
        {
            List<string> list = new List<string>();
            list.Add("User");
            list.Add("Admin");

            return list;
        }
        
    }
}