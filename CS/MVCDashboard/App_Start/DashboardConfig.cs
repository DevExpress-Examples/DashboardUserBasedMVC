using System.Web;
using System.Web.Routing;
using DevExpress.DashboardWeb;
using DevExpress.DashboardWeb.Mvc;

namespace MVCDashboard {
    public static class DashboardConfig {
        public static void RegisterService(RouteCollection routes) {
            routes.MapDashboardRoute("api/dashboard", "DefaultDashboard");

            DashboardConfigurator.Default.SetConnectionStringsProvider(new CustomConnectionStringProvider());
            DashboardConfigurator.Default.SetDataSourceStorage(new CustomDataSourceStorage());
            DashboardConfigurator.Default.SetDashboardStorage(new CustomDashboardStorage("~/App_Data/Dashboards/"));
            DashboardConfigurator.Default.SetDBSchemaProvider(new CustomDBSchemaProvider());

            DashboardConfigurator.Default.VerifyClientTrustLevel += DashboardConfigurator_VerifyClientTrustLevel;
        }

        private static void DashboardConfigurator_VerifyClientTrustLevel(object sender, VerifyClientTrustLevelEventArgs e) {
            var userName = (string)HttpContext.Current.Session["CurrentUser"];

            if (userName == null || userName == "Guest")
                e.ClientTrustLevel = ClientTrustLevel.Restricted;
        }
    }
}