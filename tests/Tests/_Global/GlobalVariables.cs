using BindOpen.Framework.Runtime.Scopes;
using BindOpen.Framework.MetaData.Helpers.Files;
using BindOpen.Framework.MetaData.Stores;
using BindOpen.Framework.Extensions.References;
using BindOpen.Databases.Tests.PostgreSql.Data.Models;
using BindOpen.Databases.Tests.Settings;
using BindOpen.Databases.Stores.Depots;

namespace BindOpen.Tests.Databases
{
    public static class GlobalVariables
    {
        static string _workingFolder = null;
        static IBdoHost _appHost = null;

        public static string WorkingFolder
        {
            get
            {
                if (_workingFolder == null)
                {
                    _workingFolder = (FileHelper.GetAppRootFolderPath() + @"bdo\temp\").ToPath();
                }

                return _workingFolder;
            }
        }

        public static IBdoHost AppHost
        {
            get
            {
                return _appHost ?? (_appHost = BdoHostFactory.CreateBindOpenHost<TestAppSettings>(
                        options => options
                            .SetModule("app.test")
                            .AddExtensions(p => p.AddMSSqlServer().AddPostgreSql())
                            .AddDataStore(s => s
                                .RegisterDatasources(m => m
                                    .AddFromConfiguration(options)
                                    .AddDatasource(m.CreatePostgreSqlDatasource("db.testA", "connectionStringA")))
                                .RegisterDbModels((m, l) => m.AddFromAssembly<TestDbModel>(l)))
                        //.AddDefaultFileLogger()
                        //.ThrowExceptionOnStartFailure()
                        //.AddLoggers(
                        //    BdoLoggerFactory.Create<BdoSnapLogger>(null, BdoLoggerMode.Auto).AddConsoleOutput())
                        ));
            }
        }
    }

}
