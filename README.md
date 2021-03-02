# MVC Dashboard - How to implement multi-tenant Dashboard architecture

This example shows how to configure the Dashboard control so that it works in the multi-user environment. You can limit access to the following information depending on the current user's ID:

|    | Description | API | File |
| --- | --- | --- | --- |
| **Dashboards** | Create custom dahboard storage and specify which dashboards the user can access, edit, and save. | [IEditableDashboardStorage Interface](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.IEditableDashboardStorage) | [CustomDashboardStorage.cs](./CS/MVCDashboard/Code/CustomDashboardStorage.cs) |
| **Data sources**  | Create custom data source storage and specify which data sources are available to the user. | [IDataSourceStorage Interface](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.IDataSourceStorage) | [CustomDataSourceStorage.cs](./CS/MVCDashboard/Code/CustomDataSourceStorage.cs) |
| **Data source schema**  | Create a custom data source schema provider and filter the data source for different users to show only a part of the data source. | [DBSchemaProviderEx Class](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.DBSchemaProviderEx) | [CustomDBSchemaProvider.cs](./CS/MVCDashboard/Code/CustomDBSchemaProvider.cs) |
| **Connection strings**  | Create a custom connection string provider and specify connection strings depending on the user's access rights. | [IDataSourceWizardConnectionStringsProvider Interface](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Web.IDataSourceWizardConnectionStringsProvider) | [CustomConnectionStringProvider.cs](./CS/MVCDashboard/Code/CustomConnectionStringProvider.cs) |
| **Working mode**  | Configure the security level and specify whether the user can access the Designer mode. | [DashboardConfigurator.VerifyClientTrustLevel](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.VerifyClientTrustLevel) | [Dashboard.cshtml](./CS/MVCDashboard/Views/Home/Dashboard.cshtml) <br/> [DashboardConfig.cs](./CS/MVCDashboard/App_Start/DashboardConfig.cs) |

Every custom store/provider uses the `HttpContext.Current.Session["CurrentUser"]` value to detect the current user and return user-specific content.

When the application starts, you see the [Index](./CS/MVCDashboard/Views/Home/Index.cshtml) view with a ComboBox in which you can select a user. When you click the **Sign in** button, the ID of the selected user is passed to the `HttpContext.Current.Session["CurrentUser"]` variable and you are redirected to the [Dashboard](./CS/MVCDashboard/Views/Home/Dashboard.cshtml) view. In this view, the Web Dashboard control demonstrates the features according to the selected user. Below is a table that illustrates the user IDs and their associated rights in this example:

| Role  | Dashboard Storage | DataSource Storage | ConnectionString Provider | DBSchema Provider | Working Mode | Create/Edit |
| --- | --- | --- | --- | --- | --- | --- |
| Admin | dashboard1_admin, dashboard2_admin | SqlDataSource, JsonDataSource | Northwind, CarsXtraScheduling | All (Categories, Products, Cars,...) | Designer, Viewer | Yes |
| User | dashboard1_user | SqlDataSource | CarsXtraScheduling | Cars | Designer, Viewer | No |
| Guest | dashboard1_guest | - | - | - | ViewerOnly | - |


In this example, the Web Dashboard control operates in `ViewerOnly` mode for unauthorized users and guests. To do this, handle the [DashboardConfigurator.VerifyClientTrustLevel](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.VerifyClientTrustLevel) event and set the `e.ClientTrustLevel` property to `Restricted` ([Dashboard.cshtml](./CS/MVCDashboard/Views/Home/Dashboard.cshtml) and [DashboardConfig.cs](./CS/MVCDashboard/App_Start/DashboardConfig.cs)). This setting prevents inadvertent or unauthorized modifications of dashboards stored on a server. You can find more information in the following help section: [Security Considerations - Web Dashboard Working Modes](https://docs.devexpress.com/Dashboard/118651/web-dashboard/general-information/security-considerations#web-dashboard-working-modes).

## See Also

- [T590909 - Web Dashboard - How to load dashboards based on user roles](https://supportcenter.devexpress.com/ticket/details/t590909/web-dashboard-how-to-load-dashboards-based-on-user-roles)
- [T896804 - ASP.NET Core Dashboard - How to implement authentication](https://supportcenter.devexpress.com/ticket/details/t896804/asp-net-core-dashboard-how-to-implement-authentication)
- [T400693 - MVC Dashboard - How to load and save dashboards from/to a database](https://supportcenter.devexpress.com/ticket/details/t400693/mvc-dashboard-how-to-load-and-save-dashboards-from-to-a-database)
