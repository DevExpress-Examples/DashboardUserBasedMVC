# MVC Dashboard - How to implement multi-tenant Dashboard architecture

This example shows how to configure the Dashboard control so that it works in the multi-user environment. You can limit access to the following information depending on the current user's ID:

- **Dashboards**

	Create custom dahboard storage and specify which dashboards the user can access, edit, and save.
- **Data sources** 

	Create custom data source storage and specify which data sources are available to the user.
- **Data source schema** 

	Create a custom data source schema provider and filter the data source for different users to show only a part of the data source.
- **Connection strings** 

	Create a custom connection string provider and specify connection strings depending on the user's access rights.
- **Working mode** 

	Handle the _VerifyClientTrustLevel_ event and specify whether the user can access the Designer mode.

When the application starts, you see the [Index](./CS/MVCDashboard/Views/Home/Index.cshtml) view with a ComboBox in which you can select a user and the **Sign in** button. When you click the button, the ID of the selected user is passed to the `HttpContext.Session["CurrentUser"]` variable and you are redirected to the [Dashboard](./CS/MVCDashboard/Views/Home/Dashboard.cshtml) view. In this view, the Web Dashboard control demonstrates the features according to the selected user. Below is a table that illustrates the user IDs and their associated rights in this example:

| Role  | Dashboard Storage | DataSource Storage | ConnectionString Provider | DBSchema Provider | Working Mode | Create/Edit |
| --- | --- | --- | --- | --- | --- | --- |
| Admin | dashboard1_admin, dashboard2_admin | SqlDataSource, JsonDataSource | Northwind, CarsXtraScheduling | All (Categories, Products, Cars,...) | Designer, Viewer | Yes |
| User | dashboard1_user | SqlDataSource | CarsXtraScheduling | Cars | Designer, Viewer | No |
| Guest | dashboard1_guest | - | - | - | ViewerOnly | - |
| Unauthorized | - | - | - | - | ViewerOnly | - |

The example implements a set of custom providers/storages for user-specific processing:

- [IEditableDashboardStorage Interface](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.IEditableDashboardStorage) ([CustomDashboardStorage.cs](./CS/MVCDashboard/Code/CustomDashboardStorage.cs))
- [IDataSourceStorage Interface](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.IDataSourceStorage) ([CustomDataSourceStorage.cs](./CS/MVCDashboard/Code/CustomDataSourceStorage.cs))
- [IDataSourceWizardConnectionStringsProvider Interface](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Web.IDataSourceWizardConnectionStringsProvider) ([CustomConnectionStringProvider.cs](./CS/MVCDashboard/Code/CustomConnectionStringProvider.cs))
- [DBSchemaProviderEx Class](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.DBSchemaProviderEx) ([CustomDBSchemaProvider.cs](./CS/MVCDashboard/Code/CustomDBSchemaProvider.cs))

Every custom store/provider uses the `System.Web.HttpContext.Current.Session["CurrentUser"]` variable to detect the current user and return user-specific content.

For unauthorized users and guests, you can display dashboards in `ViewerOnly` mode to prevent inadvertent or unauthorized modifications to dashboards stored on a server. To do this, handle the [DashboardConfigurator.VerifyClientTrustLevel](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.VerifyClientTrustLevel) event and set the set the `e.ClientTrustLevel` property to `Restricted` ([Dashboard.cshtml](./CS/MVCDashboard/Views/Home/Dashboard.cshtml) and [DashboardConfig.cs](./CS/MVCDashboard/App_Start/DashboardConfig.cs)). In this case, the unauthorized users can only view the dashboards. You can find more information in the following help section: [Security Considerations - Web Dashboard Working Modes](https://docs.devexpress.com/Dashboard/118651/web-dashboard/general-information/security-considerations#web-dashboard-working-modes).

## See Also

- [T590909 - Web Dashboard - How to load dashboards based on user roles](https://supportcenter.devexpress.com/ticket/details/t590909/web-dashboard-how-to-load-dashboards-based-on-user-roles)
- [T896804 - ASP.NET Core Dashboard - How to implement authentication](https://supportcenter.devexpress.com/ticket/details/t896804/asp-net-core-dashboard-how-to-implement-authentication)
- [T400693 - MVC Dashboard - How to load and save dashboards from/to a database](https://supportcenter.devexpress.com/ticket/details/t400693/mvc-dashboard-how-to-load-and-save-dashboards-from-to-a-database)
