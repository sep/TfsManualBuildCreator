using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SepLabs.Projects.TfsManualBuildCreator.Tests
{
    [TestClass]
    public class OptionsTest
    {
        private const string SampleServerCollectionUrl = @"http://SampleServer/tfs/DefaultCollection";
        private const string SampleBuildLabel = @"Sample Build Label v2.1";
        private const string SampleProjectName = @"Sample Project";
        private const string SampleDropLocation = @"\\SAMPLE\Shared\Builds";
        private const string SampleBuildDefinition = @"Sample Build Definition";

        [TestMethod]
        public void ShouldUnderstandTfsCollectionUriOption()
        {
            var args = new[] { "-s", SampleServerCollectionUrl};
            var options = Options.GetOptions(args);

            Assert.AreEqual(SampleServerCollectionUrl, options.TfsServerCollectionUrl);
        }

        [TestMethod]
        public void ShouldUnderstandBuildLabelOption()
        {
            var args = new[] {"-b", SampleBuildLabel};
            var options = Options.GetOptions(args);

            Assert.AreEqual(SampleBuildLabel, options.BuildLabel);
        }

        [TestMethod]
        public void ShouldUnderstandHelpOption()
        {
            var args = new[] {"-h"};
            var options = Options.GetOptions(args);

            Assert.IsTrue(options.IsHelpRequest);
        }

        [TestMethod]
        public void ShouldUnderstandProjectNameOption()
        {
            var args = new[] {"-p", SampleProjectName};
            var options = Options.GetOptions(args);

            Assert.AreEqual(SampleProjectName, options.ProjectName);
        }

        [TestMethod]
        public void ShouldUnderstandDropLocationOption()
        {
            var args = new[] {"-d", SampleDropLocation};
            var options = Options.GetOptions(args);

            Assert.AreEqual(SampleDropLocation, options.DropLocation);
        }

        [TestMethod]
        public void ShouldUnderstandBuildDefinitionNameOption()
        {
            var args = new[] {"--build-definition", SampleBuildDefinition};
            var options = Options.GetOptions(args);

            Assert.AreEqual(SampleBuildDefinition, options.BuildDefinition);
        }

        [TestMethod]
        public void ShouldReportInvalidOptionsIfNotAllRequiredArePresent()
        {
            var args = new[]
                {
                    "-b", SampleBuildLabel,
                    "-s", SampleServerCollectionUrl
                };
            var options = Options.GetOptions(args);

            Assert.IsFalse(options.IsValid);
        }

        [TestMethod]
        public void ShouldReportValidOptionsIfAllRequiredArePresent()
        {
            var args = new[]
                {
                    "--build-definition", SampleBuildDefinition,
                    "-b", SampleBuildLabel,
                    "-d", SampleDropLocation,
                    "-p", SampleProjectName,
                    "-s", SampleServerCollectionUrl
                };
            var options = Options.GetOptions(args);

            Assert.IsTrue(options.IsValid);
        }
    }
}
