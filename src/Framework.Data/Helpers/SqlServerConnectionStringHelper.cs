using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data
{
    class SqlServerConnectionStringHelper
    {
        static string SqlServer = string.Empty;
        static bool IntegratedSecurity = true;
        static string Username = string.Empty;
        static string Password = string.Empty;
        
        static SqlServerConnectionStringHelper()
        {
            SqlServer = ConfigurationManager.AppSettings["SqlServer"];
            IntegratedSecurity = Convert.ToBoolean(ConfigurationManager.AppSettings["IntegratedSecurity"]);
            Username = ConfigurationManager.AppSettings["Username"];
            Password = ConfigurationManager.AppSettings["Password"];
        }

        public static string GetEntityConnectionString(string database)
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = SqlServer;
            sqlBuilder.InitialCatalog = string.Format("banka-{0}", database);

            if (IntegratedSecurity)
            {
                sqlBuilder.IntegratedSecurity = true;
            }
            else
            {
                sqlBuilder.UserID = Username;
                sqlBuilder.Password = Password;
            }

            sqlBuilder.MultipleActiveResultSets = true;

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.ProviderConnectionString = sqlBuilder.ToString();
            entityBuilder.Metadata = "res://*/";
            entityBuilder.Provider = "System.Data.SqlClient";

            return entityBuilder.ToString();
        }

        public static string GetSqlConnectionString(string database)
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = SqlServer;
            sqlBuilder.InitialCatalog = string.Format("banka-{0}", database); ;

            if (IntegratedSecurity)
            {
                sqlBuilder.IntegratedSecurity = true;
            }
            else
            {
                sqlBuilder.UserID = Username;
                sqlBuilder.Password = Password;
            }

            sqlBuilder.MultipleActiveResultSets = true;

            return sqlBuilder.ToString();
        }
    }
}
