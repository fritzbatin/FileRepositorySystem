using System.Web;
using System.Web.Mvc;

namespace Smits.Etg.FileRepositorySystem.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
