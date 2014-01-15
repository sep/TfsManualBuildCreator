using System;
using SimpleImpersonation;

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
                exitCode = options.ImpersonateUser
                               ? RunAsImpersonatedUser(CreateBuild, options)
                               : CreateBuild(options);
            }

            return exitCode;
        }

        private static int RunAsImpersonatedUser(Func<Options, int> thingToRun, Options options)
        {
            int exitCode;
            var userNameParts = options.UserName.Split('\\');
            using (Impersonation.LogonUser(userNameParts[0], userNameParts[1], options.Password, LogonType.NewCredentials))
            {
                exitCode = thingToRun(options);
            }

            return exitCode;
        }
        
        private static int CreateBuild(Options options)
        {
            var buildCreator = new ManualBuildCreator(options, new TfsManager());
            var returnStatus = buildCreator.CreateBuild();

            var writer = returnStatus.IsError ? Console.Error : Console.Out;
            writer.WriteLine(returnStatus.Message);
            
            return returnStatus.ExitCode;
        }
    }
}
