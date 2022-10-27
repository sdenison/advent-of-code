using NUnit.Framework;

namespace Aoc.Spaceship.Unit.Tests.ImagingTests
{
    [TestFixture]
    public class ImageTests
    {
        [Test]
        public void Can_parse_image_data()
        {
            var imageData = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2};
            var height = 2;
            var width = 3;
            var image = new Imaging.Image(imageData, height, width);
            Assert.AreEqual(new[] {1, 2, 3}, image.Layers[0].Rows[0]);
            Assert.AreEqual(new[] {0, 1, 2}, image.Layers[1].Rows[1]);
        }
    }
}
