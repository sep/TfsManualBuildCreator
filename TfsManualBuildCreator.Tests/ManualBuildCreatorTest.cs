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

            Assert.AreEqual(ManualBuildCreationStatus.GetBuildServerNotAvailableStatus().Message, returnCode.Message);
        }

        [TestMethod]
        public void ShouldReportErrorIfBuildServiceNotAvailable()
        {
            var options = new Options
            {
                TfsServerCollectionUrl = "http://working"
            };
            var tfsInterfaceMock = new Mock<ITfsManager>();
            tfsInterfaceMock.Setup(mock => mock.LoadBuildService()).Throws<NullReferenceException>();
            var buildCreator = new ManualBuildCreator(options, tfsInterfaceMock.Object);

            var returnCode = buildCreator.CreateBuild();

            Assert.AreEqual(ManualBuildCreationStatus.GetBuildServiceNotAvailableStatus().Message, returnCode.Message);
        }

        [TestMethod]
        public void ShouldReportErrorIfBuildOrProjectNotFound()
        {
            var options = new Options
            {
                TfsServerCollectionUrl = "http://working"
            };
            var tfsInterfaceMock = new Mock<ITfsManager>();
            tfsInterfaceMock.Setup(mock => mock.LoadBuildInProjectByName(It.IsAny<string>(), It.IsAny<string>()))
                            .Throws(new BuildDefinitionNotFoundException("", ""));
            var buildCreator = new ManualBuildCreator(options, tfsInterfaceMock.Object);

            var returnCode = buildCreator.CreateBuild();

            Assert.AreEqual(ManualBuildCreationStatus.GetBuildNotFoundStatus().Message, returnCode.Message);
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

            Assert.AreEqual(ManualBuildCreationStatus.GetCreateFailureStatus().Message, returnCode.Message);
        }

        [TestMethod]
        public void ShouldReportSuccessIfAbleToCreateBuild()
        {
            var options = new Options
            {
                TfsServerCollectionUrl = "http://working"
            };
            var tfsInterfaceMock = new Mock<ITfsManager>();
            var buildCreator = new ManualBuildCreator(options, tfsInterfaceMock.Object);

            var returnCode = buildCreator.CreateBuild();

            Assert.AreEqual(ManualBuildCreationStatus.GetSuccessStatus().Message, returnCode.Message);
        }
    }
}
