using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public class TemplateQueries
    {
        public static string AllTemplates => "usp_GetTemplates";

        public static string DeleteTemplate => "DELETE FROM [Template] WHERE [Id] = @TemplateId";

        public static string SaveTemplate => "usp_SaveTemplate";
    }
}
