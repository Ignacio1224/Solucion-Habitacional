using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Obl2_P3.Utilities
{
    public class Check
    {
        public static bool UserLog()
        {
            if (HttpContext.Current.Session["userLog"] != null || HttpContext.Current.Session["userLog"].ToString() != null) return true;
            else return false;
        }
    }
}