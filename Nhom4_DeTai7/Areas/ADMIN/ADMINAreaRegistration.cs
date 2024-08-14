using System.Web.Mvc;

namespace Nhom4_DeTai7.Areas.ADMIN
{
    public class ADMINAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ADMIN";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ADMIN_default",
                "ADMIN/{controller}/{action}/{id}",
                defaults: new { controller = "SanPham", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Nhom4_DeTai7.Areas.ADMIN.Controllers" }
            );
        }
    }
}