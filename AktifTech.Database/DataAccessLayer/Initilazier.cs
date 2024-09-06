using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Database.DataAccessLayer
{
    public class Initilazier
    {
        public static IConfiguration Configuration;

        public static void Build()
        {
            var builderMigration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("dbsettings.json", optional: true, reloadOnChange: true);
            var builder = new ConfigurationBuilder().SetBasePath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)).AddJsonFile("dbsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }
    }
}
