namespace SepLabs.Projects.TfsManualBuildCreator
{
    public class ManualBuildCreationStatus
    {
        public string Message { get; set; }

        public int ExitCode { get; set; }

        public bool IsError { get; set; }

        private ManualBuildCreationStatus()
        {
            // Empty
        }

        public static ManualBuildCreationStatus GetSuccessStatus()
        {
            return new ManualBuildCreationStatus
                {
                    Message = "Build created successfully",
                    ExitCode = 0,
                    IsError =  false
                };
        }

        public static ManualBuildCreationStatus GetCreateFailureStatus()
        {
            return new ManualBuildCreationStatus
            {
                Message = "Unable to create manual build. Could be TFS permissions or server offline.",
                ExitCode = -1,
                IsError = true
            };
        }

        public static ManualBuildCreationStatus GetBuildNotFoundStatus()
        {
            return new ManualBuildCreationStatus
            {
                Message = "Unable to find build in project.",
                ExitCode = -1,
                IsError = true
            };
        }

        public static ManualBuildCreationStatus GetBuildServiceNotAvailableStatus()
        {
            return new ManualBuildCreationStatus
            {
                Message = "Unable to find build service.",
                ExitCode = -1,
                IsError = true
            };
        }

        public static ManualBuildCreationStatus GetBuildServerNotAvailableStatus()
        {
            return new ManualBuildCreationStatus
            {
                Message = "Unable to find collection at URL.",
                ExitCode = -1,
                IsError = true
            };
        }
    }
}
