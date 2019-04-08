using System.Web.Mvc;

namespace SisvonMVC.Areas.Adm
{
    public class AdmAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Adm";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Adm_Sem_ActionController",
                "Adm",
                new { Controller = "Home", action = "Index" }
            );
            context.MapRoute(
                "Adm_Sem_Action",
                "Adm/{controller}",
                new { Controller = "Home", action = "Index" }
            );
            context.MapRoute(
                "Adm_1",
                "Adm/{controller}/{action}/{id}/{nome}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional, nome = UrlParameter.Optional }
            );

            context.MapRoute(
                "Adm_default",
                "Adm/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}