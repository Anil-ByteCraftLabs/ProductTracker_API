using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.Entities
{
    public class CommonDescription: Entity
    {
        public string Description { get; set; }
    }

    // Execute SP in Dapper

    //public async Task<Company> GetCompanyByEmployeeId(int id)
    //{
    //    var procedureName = "ShowCompanyForProvidedEmployeeId";
    //    var parameters = new DynamicParameters();
    //    parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
    //    using (var connection = _context.CreateConnection())
    //    {
    //        var company = await connection.QueryFirstOrDefaultAsync<Company>
    //            (procedureName, parameters, commandType: CommandType.StoredProcedure);
    //        return company;
    //    }
    //}

}
