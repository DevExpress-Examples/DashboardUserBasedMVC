using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;
using System;
using System.Collections.Generic;
using System.Web;

public class CustomConnectionStringProvider : IDataSourceWizardConnectionStringsProvider {
    private Dictionary<string, string> connectionStrings = new Dictionary<string, string>();

    public CustomConnectionStringProvider() {
        connectionStrings.Add("NorthwindConnectionString", @"XpoProvider=MSAccess; Provider=Microsoft.Jet.OLEDB.4.0; Data Source=|DataDirectory|\nwind.mdb;");
        connectionStrings.Add("CarsXtraSchedulingConnectionString", @"XpoProvider=MSAccess; Provider=Microsoft.Jet.OLEDB.4.0; Data Source=|DataDirectory|\CarsDB.mdb;");
    }

    public Dictionary<string, string> GetConnectionDescriptions() {
        var connections = new Dictionary<string, string>();
        var userName = (string)HttpContext.Current.Session["CurrentUser"];

        if (userName == "Admin") {
            connections.Add("NorthwindConnectionString", "Northwind Connection");
            connections.Add("CarsXtraSchedulingConnectionString", "CarsXtraScheduling Connection");
        }
        else if (userName == "User") {
            connections.Add("CarsXtraSchedulingConnectionString", "CarsXtraScheduling Connection");
        }

        return connections;
    }

    public DataConnectionParametersBase GetDataConnectionParameters(string name) {
        if (GetConnectionDescriptions().ContainsKey(name)) {
            return new CustomStringConnectionParameters(connectionStrings[name]);
        }
        else {
            throw new ApplicationException("You are not authorized to use this connection.");
        }
    }
}