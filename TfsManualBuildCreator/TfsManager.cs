using System;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;

namespace SepLabs.Projects.TfsManualBuildCreator
{
    /// <summary>
    /// Implementation of <see cref="ITfsManager"/>.
    /// </summary>
    public class TfsManager : ITfsManager
    {
        private TfsTeamProjectCollection teamProjectCollection;
        private IBuildServer buildServer;
        private IBuildDefinition buildDefinition;

        /// <summary>
        /// Connects to TFS Project Collection.
        /// </summary>
        /// <param name="tfsUri">The TFS collection URI.</param>
        /// <exception cref="TeamFoundationServerException">Thrown if unable to connect to Collection URI.</exception>
        public void ConnectToServer(Uri tfsUri)
        {
            teamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(tfsUri);
        }

        /// <summary>
        /// Loads the build service.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if no build service is found.</exception>
        public void LoadBuildService()
        {
            buildServer = teamProjectCollection.GetService<IBuildServer>();

            if (buildServer == null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// Loads the Build Definition in a project by name.
        /// </summary>
        /// <param name="buildName">Name of the build.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <exception cref="BuildDefinitionNotFoundException">Thrown if unable to find build definition.</exception>
        public void LoadBuildInProjectByName(string buildName, string projectName)
        {
            buildDefinition = buildServer.GetBuildDefinition(projectName, buildName);
        }

        /// <summary>
        /// Creates the build.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="dropLocation">The drop location.</param>
        /// <exception cref="TeamFoundationServerException">Thrown if unable to create build.</exception>
        /// <remarks>This has not been tested with incorrect TFS permissions.</remarks>
        public void CreateBuild(string label, string dropLocation)
        {
            var manualBuild = buildDefinition.CreateManualBuild(label);
            manualBuild.FinalizeStatus(BuildStatus.Succeeded);
            manualBuild.CompilationStatus = BuildPhaseStatus.Succeeded;
            manualBuild.DropLocation = dropLocation;
            manualBuild.Save();
        }
    }
}
