using CommandLine;
using CommandLine.Text;

namespace SepLabs.Projects.TfsManualBuildCreator
{
    /// <summary>
    /// Command-line options parser.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the TFS server collection URL.
        /// </summary>
        /// <value>The TFS server collection URL.</value>
        [Option('s', "server", HelpText = "Location of TFS collection instance (http[s]://server:port/path/CollectionName)")]
        public string TfsServerCollectionUrl { get; set; }

        /// <summary>
        /// Gets or sets the build label.
        /// </summary>
        /// <value>The build label.</value>
        [Option('b', "build", HelpText = "Label applied to manual build")]
        public string BuildLabel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is help request.
        /// </summary>
        /// <value><c>true</c> if this instance is help request; otherwise, <c>false</c>.</value>
        [Option('h', "help")]
        public bool IsHelpRequest { get; set; }

        /// <summary>
        /// Gets or sets the name of the TFS project that the Build Definition is associated with.
        /// </summary>
        /// <value>The name of the project.</value>
        [Option('p', "project-name", HelpText = "Name of TFS Project to create manual build against")]
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the drop location, usually a network share.
        /// </summary>
        /// <value>The drop location.</value>
        [Option('d', "drop-folder", HelpText = "Location of build DLLs")]
        public string DropLocation { get; set; }

        /// <summary>
        /// Gets or sets the build definition name to create manual build under.
        /// </summary>
        /// <value>The build definition.</value>
        [Option("build-definition", HelpText = "Build Definition to create manual build under")]
        public string BuildDefinition { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [Option("impersonate-user", HelpText = "The user to impersonate when connecting to TFS server")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [Option("impersonate-user-password", HelpText = "The password of the user to impersonate when connecting to TFS Server")]
        public string Password { get; set; }

        public bool ImpersonateUser
        {
            get { return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid { get; set; }

        /// <summary>
        /// Factory method that creates an Options object.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        /// <returns>A configured Options object.</returns>
        public static Options GetOptions(string[] args)
        {
            var options = new Options();
       
            using (var parser = Parser.Default)
            {
                if (parser.ParseArguments(args, options))
                {
                    options.IsValid = GetIsValid(options);
                }
            }

            return options;
        }

        /// <summary>
        /// Gets the usage string for the application.
        /// </summary>
        /// <returns>Usage information.</returns>
        /// <remarks>
        /// Also used by the command-line parsing library to automatically display usage on parse failure.
        /// </remarks>
        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
                {
                    Heading = new HeadingInfo("TFS Manual Build Creator", "1.0.0.0"),
                    Copyright = new CopyrightInfo("SEP Labs", 2014),
                    AdditionalNewLineAfterOption = true,
                    AddDashesToOption = true
                };
            help.AddPreOptionsLine("Released under BSD License");
            help.AddPreOptionsLine("Usage:");
            help.AddPreOptionsLine(
                "\ttfs-mbc.exe -s <server> -b <build label> -d <drop folder>" +
                " -p <project name> --build-definition <build definition>");
            help.AddPreOptionsLine("\ttfs-mbc.exe -h");
            help.AddOptions(this);

            return help;
        }

        private static bool GetIsValid(Options options)
        {
            return
                options.IsHelpRequest ||
                !string.IsNullOrEmpty(options.BuildDefinition) &&
                !string.IsNullOrEmpty(options.BuildLabel) &&
                !string.IsNullOrEmpty(options.DropLocation) &&
                !string.IsNullOrEmpty(options.ProjectName) &&
                !string.IsNullOrEmpty(options.TfsServerCollectionUrl) &&
                !((string.IsNullOrEmpty(options.UserName) && !string.IsNullOrEmpty(options.Password)) ||
                  (!string.IsNullOrEmpty(options.UserName) && string.IsNullOrEmpty(options.Password)));

        }
    }
}
