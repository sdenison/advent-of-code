using Aoc.Spaceship.Imaging;
using NUnit.Framework;

namespace Aoc.Spaceship.Unit.Tests.ImagingTests
{
    [TestFixture]
    public class ImageTests
    {

        [Test]
        public void Can_parse_image_data_from_string()
        {
            var imageData = "123456789012";
            var height = 2;
            var width = 3;
            var image = new Imaging.Image(imageData, height, width);
            Assert.AreEqual(new[] { 1, 2, 3 }, image.Layers[0].Rows[0]);
            Assert.AreEqual(new[] { 0, 1, 2 }, image.Layers[1].Rows[1]);
        }

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

        [Test]
        public void Can_solve_day_8_step_1()
        {
            var imageData = DataGenerator.GetDay8PuzzleData();
            var height = 6;
            var width = 25;
            var image = new Imaging.Image(imageData, height, width);
            Layer layerWithFewest0Dights = null;
            var fewestZeros = int.MaxValue;
            var numberOfOneDigits = 0;
            var numberOfTwoDigits = 0;
            foreach(var layer in image.Layers)
            {

                var oneDigitsForLayer = 0;
                var twoDigitsForLayer = 0;
                var zeroCount = 0;
                foreach(var row in layer.Rows)
                {
                    zeroCount += row.Where(x => x.Equals(0)).Count();
                    oneDigitsForLayer += row.Where(x => x.Equals(1)).Count();
                    twoDigitsForLayer += row.Where(x => x.Equals(2)).Count();
                }
                if (fewestZeros > zeroCount)
                {
                    fewestZeros = zeroCount;
                    numberOfOneDigits = oneDigitsForLayer;
                    numberOfTwoDigits = twoDigitsForLayer;
                }
            }

            var answer = numberOfOneDigits * numberOfTwoDigits;
            Assert.AreEqual(1072, answer);
        }

        [Test]
        public void Can_determine_pixel_colors_from_example()
        {
            var imageData = new[] {0, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1, 2, 0, 0, 0, 0};
            var height = 2;
            var width = 2;
            var image = new Image(imageData, height, width);

            var finalImage = image.GetFinalImage();
            Assert.AreEqual(" 1", finalImage[0]);
            Assert.AreEqual("1 ", finalImage[1]);
            File.WriteAllLines("D:\\temp\\day8_puzzle_example_output.txt", finalImage);
        }

        [Test]
        public void Can_get_message_for_day_8_step_2()
        {
            var imageData = DataGenerator.GetDay8PuzzleData();
            var height = 6;
            var width = 25;
            var image = new Image(imageData, height, width);

            var finalImage = image.GetFinalImage();
            File.WriteAllLines("D:\\temp\\day8_puzzle_example_answer.txt", finalImage);
        }
    }
}
