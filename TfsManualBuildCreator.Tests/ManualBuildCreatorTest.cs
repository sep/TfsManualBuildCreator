using System;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SepLabs.Projects.TfsManualBuildCreator.Tests
{
    [TestClass]
    public class ManualBuildCreatorTest
    {
        [TestMethod]
        public void ShouldReportErrorIfInvalidServerCollection()
        {
            var options = new Options
                {
                    TfsServerCollectionUrl = "http://broken"
                };
            var tfsInterfaceMock = new Mock<ITfsManager>();
            tfsInterfaceMock.Setup(mock => mock.ConnectToServer(It.IsAny<Uri>()))
                            .Throws(new TeamFoundationServiceUnavailableException(""));
            var buildCreator = new ManualBuildCreator(options, tfsInterfaceMock.Object);

            var returnCode = buildCreator.CreateBuild();

            Assert.AreEqual(ManualBuildReturnCode.ServerNotAvailable, returnCode);
        }

        [TestMethod]
        public void ShouldReportErrorIfBuildServiceNotAvailable()
        {
            var options = new Options
            {
                TfsServerCollectionUrl = "http://working"
            };
            var tfsInterfaceMock = new Mock<ITfsManager>();
            tfsInterfaceMock.Setup(mock => mock.GetBuildService()).Throws<NullReferenceException>();
            var buildCreator = new ManualBuildCreator(options, tfsInterfaceMock.Object);

            var returnCode = buildCreator.CreateBuild();

            Assert.AreEqual(ManualBuildReturnCode.ServiceNotAvailable, returnCode);
        }

        [TestMethod]
        public void ShouldReportErrorIfBuildOrProjectNotFound()
        {
            var options = new Options
            {
                TfsServerCollectionUrl = "http://working"
            };
            var tfsInterfaceMock = new Mock<ITfsManager>();
            tfsInterfaceMock.Setup(mock => mock.FindBuildInProjectByName(It.IsAny<string>(), It.IsAny<string>()))
                            .Throws(new BuildDefinitionNotFoundException("", ""));
            var buildCreator = new ManualBuildCreator(options, tfsInterfaceMock.Object);

            var returnCode = buildCreator.CreateBuild();

            Assert.AreEqual(ManualBuildReturnCode.BuildNotFound, returnCode);
        }

        [TestMethod]
        public void ShouldReportErrorIfUnableToCreateBuild()
        {
            var options = new Options
            {
                TfsServerCollectionUrl = "http://working"
            };
            var tfsInterfaceMock = new Mock<ITfsManager>();
            tfsInterfaceMock.Setup(mock => mock.CreateBuild(It.IsAny<string>(), It.IsAny<string>()))
                            .Throws<TeamFoundationServerException>();
            var buildCreator = new ManualBuildCreator(options, tfsInterfaceMock.Object);

            var returnCode = buildCreator.CreateBuild();

            Assert.AreEqual(ManualBuildReturnCode.CreateFailure, returnCode);
        }
    }
}
