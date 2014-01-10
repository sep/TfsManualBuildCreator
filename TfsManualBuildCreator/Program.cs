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
                var returnStatus = buildCreator.CreateBuild();

                var writer = returnStatus.IsError ? Console.Error : Console.Out;
                writer.WriteLine(returnStatus.Message);
                exitCode = returnStatus.ExitCode;
            }

            return exitCode;
        }
    }
}
