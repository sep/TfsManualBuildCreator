namespace SepLabs.Projects.TfsManualBuildCreator
{
    /// <summary>
    /// Status codes for the 'create build' operation.
    /// </summary>
    public enum ManualBuildReturnCode
    {
        /// <summary>
        /// The server is not available.
        /// </summary>
        ServerNotAvailable,

        /// <summary>
        /// Successfully created manual build.
        /// </summary>
        Success,

        /// <summary>
        /// The TFS Build Service is unavailable.
        /// </summary>
        ServiceNotAvailable,

        /// <summary>
        /// The given build name was not found under the given project name.
        /// </summary>
        BuildNotFound,

        /// <summary>
        /// Unable to create the manual build, possibly due to permissions issues in TFS or the server going offline.
        /// </summary>
        CreateFailure
    }
}