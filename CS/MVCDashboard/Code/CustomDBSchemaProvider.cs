using DevExpress.DataAccess.Sql;
using DevExpress.Xpo.DB;
using System.Linq;
using System.Web;

public class CustomDBSchemaProvider : DBSchemaProviderEx {
    public override DBTable[] GetTables(SqlDataConnection connection, params string[] tableList) {
        var result = base.GetTables(connection, tableList);

        var userName = (string)HttpContext.Current.Session["CurrentUser"];

        if (userName == "Admin") {
            return result;
        }
        else if (userName == "User") {
            return result.Where(t => t.Name == "Cars").ToArray();
        }
        else {
            return new DBTable[0];
        }
    }
}