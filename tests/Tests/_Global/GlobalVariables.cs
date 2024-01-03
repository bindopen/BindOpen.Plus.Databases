using BindOpen.Data.Helpers;
using BindOpen.Databases.Tests.Fakes;
using BindOpen.Scoping;

namespace BindOpen.Plus.Databases.Tests
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

        public static IBdoScope Scope
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
                                .RegisterDbModels((m, l) => m.AddFromAssembly<DbModelFake>(l)))
                        //.AddDefaultFileLogger()
                        //.ThrowExceptionOnStartFailure()
                        //.AddLoggers(
                        //    BdoLoggerFactory.Create<BdoSnapLogger>(null, BdoLoggerMode.Auto).AddConsoleOutput())
                        ));
            }
        }
    }

}
