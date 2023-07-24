using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Infrastructure.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _loginConnectionString;
        private readonly string _adminConnectionString;
        private readonly string _manufacturerConnectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _loginConnectionString = _configuration.GetConnectionString("LoginConnection");
            _adminConnectionString = _configuration.GetConnectionString("AdminConnection");
            _manufacturerConnectionString = _configuration.GetConnectionString("ManufacturerConnection");
        }

        public IDbConnection CreateDefaultConnection() => new SqlConnection(_loginConnectionString);
        public IDbConnection CreateAdminConnection() => new SqlConnection(_adminConnectionString);
        public IDbConnection CreateManufacturerConnection() => new SqlConnection(_manufacturerConnectionString);
    }
}
