namespace SepLabs.Projects.TfsManualBuildCreator
{
    /// <summary>
    /// Statuses for build creation return values.
    /// </summary>
    public class ManualBuildCreationStatus
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the exit code.
        /// </summary>
        /// <value>The exit code.</value>
        public int ExitCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is error.
        /// </summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
        public bool IsError { get; set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="ManualBuildCreationStatus"/> class from being created.
        /// </summary>
        private ManualBuildCreationStatus()
        {
            // Empty
        }

        /// <summary>
        /// Gets the Success status.
        /// </summary>
        /// <returns>ManualBuildCreationStatus.</returns>
        public static ManualBuildCreationStatus GetSuccessStatus()
        {
            return new ManualBuildCreationStatus
                {
                    Message = "Build created successfully",
                    ExitCode = 0,
                    IsError =  false
                };
        }

        /// <summary>
        /// Gets the Creation Failure status.
        /// </summary>
        /// <returns>ManualBuildCreationStatus.</returns>
        public static ManualBuildCreationStatus GetCreateFailureStatus()
        {
            return new ManualBuildCreationStatus
            {
                Message = "Unable to create manual build. Could be TFS permissions or server offline.",
                ExitCode = -1,
                IsError = true
            };
        }

        /// <summary>
        /// Gets the Build Not Found status.
        /// </summary>
        /// <returns>ManualBuildCreationStatus.</returns>
        public static ManualBuildCreationStatus GetBuildNotFoundStatus()
        {
            return new ManualBuildCreationStatus
            {
                Message = "Unable to find build in project.",
                ExitCode = -1,
                IsError = true
            };
        }

        /// <summary>
        /// Gets the Build Service Not Available status.
        /// </summary>
        /// <returns>ManualBuildCreationStatus.</returns>
        public static ManualBuildCreationStatus GetBuildServiceNotAvailableStatus()
        {
            return new ManualBuildCreationStatus
            {
                Message = "Unable to find build service.",
                ExitCode = -1,
                IsError = true
            };
        }

        /// <summary>
        /// Gets the Build Server Not Available status.
        /// </summary>
        /// <returns>ManualBuildCreationStatus.</returns>
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
