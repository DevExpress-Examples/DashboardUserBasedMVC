using DevExpress.DashboardWeb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

public class CustomDashboardStorage : IEditableDashboardStorage {
    private string dashboardStorageFolder;

    public CustomDashboardStorage(string dashboardStorageFolder) {
        this.dashboardStorageFolder = dashboardStorageFolder;
    }

    public IEnumerable<DashboardInfo> GetAvailableDashboardsInfo() {
        var dashboardInfos = new List<DashboardInfo>();

        var files = Directory.GetFiles(HttpContext.Current.Server.MapPath(dashboardStorageFolder), "*.xml");

        foreach (var item in files) {
            var name = Path.GetFileNameWithoutExtension(item);
            var userName = (string)HttpContext.Current.Session["CurrentUser"];

            if (userName != null && name.EndsWith(userName, System.StringComparison.InvariantCultureIgnoreCase))
                dashboardInfos.Add(new DashboardInfo() { ID = name, Name = name });
        }

        return dashboardInfos;
    }

    public XDocument LoadDashboard(string dashboardID) {
        if (GetAvailableDashboardsInfo().Any(di => di.Name == dashboardID)) {
            var path = HttpContext.Current.Server.MapPath(dashboardStorageFolder + dashboardID + ".xml");
            var content = File.ReadAllText(path);
            return XDocument.Parse(content);
        }
        else {
            throw new ApplicationException("You are not authorized to view this dashboard.");
        }
    }

    public string AddDashboard(XDocument dashboard, string dashboardName) {
        var userName = (string)HttpContext.Current.Session["CurrentUser"];

        if (userName == null || userName != "Admin")
            throw new ApplicationException("You are not authorized to add dashboards.");

        var path = HttpContext.Current.Server.MapPath(dashboardStorageFolder + dashboardName + "_" + userName + ".xml");

        File.WriteAllText(path, dashboard.ToString());

        return Path.GetFileNameWithoutExtension(path);
    }

    public void SaveDashboard(string dashboardID, XDocument dashboard) {
        var userName = (string)HttpContext.Current.Session["CurrentUser"];

        if (userName == null || userName != "Admin")
            throw new ApplicationException("You are not authorized to save dashboards.");

        var path = HttpContext.Current.Server.MapPath(dashboardStorageFolder + dashboardID + ".xml");

        File.WriteAllText(path, dashboard.ToString());
    }
}