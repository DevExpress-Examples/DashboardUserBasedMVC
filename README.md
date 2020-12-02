# MVC Dashboard - How to implement multi-tenant Dashboard architecture

This example illustrates how to configure the Dashboard component to work in the muti-user environment. The following user-specific features are implemented here:

* Load different lists of dashboards based on user ([CustomDashboardStorage.cs](./CS/MVCDashboard/Code/CustomDashboardStorage.cs))
* Load different lists of datasources based on user ([CustomDataSourceStorage.cs](./CS/MVCDashboard/Code/CustomDataSourceStorage.cs))
* Load different lists of connection strings based on user ([CustomConnectionStringProvider.cs](./CS/MVCDashboard/Code/CustomConnectionStringProvider.cs))
* Load different datasource schema based on user ([CustomDBSchemaProvider.cs](./CS/MVCDashboard/Code/CustomDBSchemaProvider.cs))
* Apply the `ViewerOnly` mode for some users ([Dashboard.cshtml](./CS/MVCDashboard/Views/Home/Dashboard.cshtml) and [DashboardConfig.cs](./CS/MVCDashboard/App_Start/DashboardConfig.cs))

Initially, the end-user opens the [Index.cshtml](./CS/MVCDashboard/Views/Home/Index.cshtml) view with the ComboBox and the Submit button for logging in. Aftrer that, the selected user Id is passed to the `HttpContext.Session["CurrentUser"]` variable and the end-user is redirected to the [Dashboard.cshtml](./CS/MVCDashboard/Views/Home/Dashboard.cshtml) view where the Dashboard component demonstrates feeatures listed above. We implement a set of custom providers/storages to achieve user-specific processing:

[IEditableDashboardStorage Interface](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.IEditableDashboardStorage)
[IDataSourceStorage Interface](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.IDataSourceStorage)
[IDataSourceWizardConnectionStringsProvider Interface](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Web.IDataSourceWizardConnectionStringsProvider)
[DBSchemaProviderEx Class](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.DBSchemaProviderEx)

Eevery custom store/provider uses the `System.Web.HttpContext.Current.Session["CurrentUser"]` variable to detect the current user and return the user-specific content.

As for `ViewerOnly` mode we also handle the [DashboardConfigurator.VerifyClientTrustLevel Event](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.VerifyClientTrustLevel) and set the `e.ClientTrustLevel` argument to the `Restricted` value as recommended in this help section: [Security Considerations > Web Dashboard Working Modes](https://docs.devexpress.com/Dashboard/118651/web-dashboard/general-information/security-considerations#web-dashboard-working-modes).

## See Also

- [T590909 - Web Dashboard - How to load dashboards based on user roles](https://supportcenter.devexpress.com/ticket/details/t590909/web-dashboard-how-to-load-dashboards-based-on-user-roles)