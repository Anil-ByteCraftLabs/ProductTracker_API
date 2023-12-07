using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public static class PlantQueries
    {
        public static string AllPlants => "usp_GetPlants";

        public static string PlantById => "SELECT * FROM [Plant] (NOLOCK) WHERE [Id] = @PlantId";


        public static string SavePlant => "usp_SavePlant";



        public static string DeletePlant => "DELETE FROM [Plant] WHERE [Id] = @PlantId";

        public static string CanPlantBeDeleted => "usp_CanPlantBeDeleted";
    }
}
