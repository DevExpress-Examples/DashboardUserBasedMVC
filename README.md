# MVC Dashboard - How to implement multi-tenant Dashboard architecture

This example illustrates how to configure the Dashboard component so that it works in the multi-user environment. The following user-specific features are implemented here:

* Load different lists of dashboards depending on the current user's ID ([CustomDashboardStorage.cs](./CS/MVCDashboard/Code/CustomDashboardStorage.cs))
* Load different lists of datasources depending on the current user's ID ([CustomDataSourceStorage.cs](./CS/MVCDashboard/Code/CustomDataSourceStorage.cs))
* Load different lists of connection strings depending on the current user's ID ([CustomConnectionStringProvider.cs](./CS/MVCDashboard/Code/CustomConnectionStringProvider.cs))
* Load different datasource schema depending on the current user's ID ([CustomDBSchemaProvider.cs](./CS/MVCDashboard/Code/CustomDBSchemaProvider.cs))
* Allow creating/editing dashboards depending on the current user's ID ([CustomDashboardStorage.cs](./CS/MVCDashboard/Code/CustomDashboardStorage.cs))
* Apply the `ViewerOnly` mode for some users ([Dashboard.cshtml](./CS/MVCDashboard/Views/Home/Dashboard.cshtml) and [DashboardConfig.cs](./CS/MVCDashboard/App_Start/DashboardConfig.cs))

At the application startup, an end-user opens the [Index.cshtml](./CS/MVCDashboard/Views/Home/Index.cshtml) view with the ComboBox and the Submit button for logging in. After that, the ID of the selected user is passed to the `HttpContext.Session["CurrentUser"]` variable and the end-user is redirected `HttpContext.Session["CurrentUser"]` variable and the end-user is redirected to the [Dashboard.cshtml](./CS/MVCDashboard/Views/Home/Dashboard.cshtml) view where the Dashboard component demonstrates the features listed above. We implement a set of custom providers/storages to for user-specific processing:

[IEditableDashboardStorage Interface](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.IEditableDashboardStorage)

[IDataSourceStorage Interface](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.IDataSourceStorage)

[IDataSourceWizardConnectionStringsProvider Interface](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Web.IDataSourceWizardConnectionStringsProvider)

[DBSchemaProviderEx Class](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.DBSchemaProviderEx)

Every custom store/provider uses the `System.Web.HttpContext.Current.Session["CurrentUser"]` variable to detect the current user and return user-specific content.

As for `ViewerOnly` mode, we also handle the [DashboardConfigurator.VerifyClientTrustLevel Event](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.VerifyClientTrustLevel) and set the `e.ClientTrustLevel` argument to the `Restricted` value as recommended in this help section: [Security Considerations > Web Dashboard Working Modes](https://docs.devexpress.com/Dashboard/118651/web-dashboard/general-information/security-considerations#web-dashboard-working-modes).

Here is a table that illustrates the user IDs and their associated rights in this example:

| Role  | Dashboard Storage | DataSource Storage | ConnectionString Provider | DBSchema Provider | Create/Edit |
| --- | --- | --- | --- | --- | --- |
| Admin | dashboard1_admin, dashboard2_admin | SqlDataSource, JsonDataSource | Northwind, CarsXtraScheduling | All (Categories, Products, Cars,...) | Yes |
| User | dashboard1_user | SqlDataSource | CarsXtraScheduling | Cars | No |
| Guest | dashboard1_guest (ViewerOnly mode) | - | - | - | - |

## See Also

- [T590909 - Web Dashboard - How to load dashboards based on user roles](https://supportcenter.devexpress.com/ticket/details/t590909/web-dashboard-how-to-load-dashboards-based-on-user-roles)
- [T896804 - ASP.NET Core Dashboard - How to implement authentication](https://supportcenter.devexpress.com/ticket/details/t896804/asp-net-core-dashboard-how-to-implement-authentication)
