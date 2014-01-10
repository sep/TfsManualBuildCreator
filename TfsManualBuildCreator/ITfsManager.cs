using System;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Build.Client;

namespace SepLabs.Projects.TfsManualBuildCreator
{
    /// <summary>
    /// Interface for TFS integration.
    /// </summary>
    public interface ITfsManager
    {
        /// <summary>
        /// Connects to TFS Project Collection.
        /// </summary>
        /// <param name="tfsUri">The TFS collection URI.</param>
        /// <exception cref="TeamFoundationServerException">Thrown if unable to connect to Collection URI.</exception>
        void ConnectToServer(Uri tfsUri);

        /// <summary>
        /// Loads the build service.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if no build service is found.</exception>
        void LoadBuildService();

        /// <summary>
        /// Loads the Build Definition in a project by name.
        /// </summary>
        /// <param name="buildName">Name of the build.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <exception cref="BuildDefinitionNotFoundException">Thrown if unable to find build definition.</exception>
        void LoadBuildInProjectByName(string buildName, string projectName);

        /// <summary>
        /// Creates the build.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="dropLocation">The drop location.</param>
        /// <exception cref="TeamFoundationServerException">Thrown if unable to create build.</exception>
        void CreateBuild(string label, string dropLocation);
    }
}
