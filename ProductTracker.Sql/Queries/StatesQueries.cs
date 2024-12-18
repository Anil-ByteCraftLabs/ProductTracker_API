using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Sql.Queries
{
    public class StatesQueries
    {
        public static string AllStates => "usp_GetStates";


        public static string StatesDistrict => "usp_GetDistricts";
    }
}
