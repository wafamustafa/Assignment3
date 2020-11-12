using System.Web;
using System.Web.Mvc;

namespace Assignment3_N01450753_WafaMustafa_5101B
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
