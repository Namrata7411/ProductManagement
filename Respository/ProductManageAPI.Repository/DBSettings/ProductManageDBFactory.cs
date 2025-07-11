using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManageAPI.Repository.DBSettings
{
    public static class ProductManageDBFactoryHelper
    {
        private static IConfiguration Configuration { get; set; }
        internal const string Provider = LinqToDB.ProviderName.SqlServer;

        public static string GetConnectionString(string ConnectionName)
        {
            switch (ConnectionName)
            {
                case "ProductDB":
                    return Configuration["ConnectionString:DefaultConnection"];
                default:
                    throw new Exception("Not connection found");
            }
        }
    }

    public class ProductManageDBFactory : LinqToDB.Data.DataConnection
    {
        public ProductManageDBFactory(string connectionName) : base(ProductManageDBFactoryHelper.Provider, ProductManageDBFactoryHelper.GetConnectionString(connectionName)) { }
    }
}
