using System;

namespace SepLabs.Projects.TfsManualBuildCreator
{
    /// <summary>
    /// Entry point for tfs-mbc.exe.
    /// </summary>
    public class Program
    {
        public static int Main(string[] args)
        {
            var options = Options.GetOptions(args);
            var exitCode = 0;

            if (options.IsHelpRequest || !options.IsValid)
            {
                Console.Write(options.GetUsage());
            }
            else
            {
                var buildCreator = new ManualBuildCreator(options, new TfsManager());
                var returnCode = buildCreator.CreateBuild();

                switch (returnCode)
                {
                    case ManualBuildReturnCode.ServerNotAvailable:
                        Console.Error.WriteLine("Unable to find collection {0}", options.TfsServerCollectionUrl);
                        exitCode = -1;
                        break;

                    case ManualBuildReturnCode.ServiceNotAvailable:
                        Console.Error.WriteLine("Unable to find build service");
                        exitCode = -1;
                        break;

                    case ManualBuildReturnCode.BuildNotFound:
                        Console.Error.WriteLine(
                            "Unable to find build {0} in project {1}",
                            options.BuildDefinition,
                            options.ProjectName);
                        exitCode = -1;
                        break;

                    case ManualBuildReturnCode.CreateFailure:
                        Console.Error.WriteLine(
                            "Unable to create manual build. Could be TFS permissions or server offline.");
                        exitCode = -1;
                        break;

                    case ManualBuildReturnCode.Success:
                        Console.WriteLine("Build created successfully");
                        break;
                }
            }

            return exitCode;
        }
    }
}
