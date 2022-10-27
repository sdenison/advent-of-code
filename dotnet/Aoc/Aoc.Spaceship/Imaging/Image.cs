using System.Collections.Generic;
using Aoc.Spaceship.Utilities;

namespace Aoc.Spaceship.Imaging
{
    public class Image
    {
        public IList<Layer> Layers { get; }

        public Image(string imageData, int height, int width) : this(ConvertToInts(imageData), height, width)
        {
        }

        public Image(int[] imageData, int height, int width)
        {
            Layers = new List<Layer>();
            var imagePointer = 0;
            while (imagePointer < imageData.Length)
            {
                var layer = new Layer();
                for(int i=1; i <= height; i++)
                {
                    layer.AddRow(imageData.SubArray(imagePointer, width));
                    imagePointer += width;
                }
                Layers.Add(layer);
            }
        }
        
        private static int[] ConvertToInts(string stringToConvert)
        {
            var imageDataInts = new List<int>();
            for(var i = 0; i < stringToConvert.Length; i++)
            {
                imageDataInts.Add(int.Parse(stringToConvert[i].ToString()));
            }
            return imageDataInts.ToArray();
        }
    }
}
