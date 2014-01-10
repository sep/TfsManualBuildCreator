﻿using System;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Build.Client;

namespace SepLabs.Projects.TfsManualBuildCreator
{
    /// <summary>
    /// Creates a manual build in TFS.
    /// </summary>
    public class ManualBuildCreator
    {
        private readonly Options options;
        private readonly ITfsManager tfsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManualBuildCreator"/> class.
        /// </summary>
        /// <param name="ops">The options for the manual build.</param>
        /// <param name="tm">The TFS Manager implementation.</param>
        public ManualBuildCreator(Options ops, ITfsManager tm)
        {
            options = ops;
            tfsManager = tm;
        }

        /// <summary>
        /// Creates the manual build.
        /// </summary>
        /// <returns>A code representing success (or what error occurred).</returns>
        public ManualBuildReturnCode CreateBuild()
        {
            var tfsUri = new Uri(options.TfsServerCollectionUrl);

            if (!AttemptAndCatch<TeamFoundationServerException>(
                () =>
                tfsManager.ConnectToServer(tfsUri)))
            {
                return ManualBuildReturnCode.ServerNotAvailable;
            }

            if (!AttemptAndCatch<NullReferenceException>(
                () =>
                tfsManager.GetBuildService()))
            {
                return ManualBuildReturnCode.ServiceNotAvailable;
            }

            if (!AttemptAndCatch<BuildDefinitionNotFoundException>(
                () =>
                tfsManager.FindBuildInProjectByName(options.BuildDefinition, options.ProjectName)))
            {
                return ManualBuildReturnCode.BuildNotFound;
            }

            if (!AttemptAndCatch<TeamFoundationServerException>(
                () =>
                tfsManager.CreateBuild(options.BuildLabel, options.DropLocation)))
            {
                return ManualBuildReturnCode.CreateFailure;
            }

            return ManualBuildReturnCode.Success;
        }

        private static bool AttemptAndCatch<TE>(Action tryFunction)
            where TE:Exception
        {
            try
            {
                tryFunction();
            }
            catch (TE)
            {
                return false;
            }

            return true;
        }
    }
}
