using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
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

        public List<string> GetFinalImage()
        {
            var finalImage = new List<int[]>();
            //Initialize to the first layer
            finalImage.AddRange(Layers[0].Rows.ToList());
            for (int layer=0; layer < Layers.Count; layer++)
            {
                for (int row = 0; row < Layers[layer].Rows.Count; row++)
                {
                    for (var pixel = 0; pixel < Layers[layer].Rows[row].Length; pixel++)
                    {
                        if (finalImage[row][pixel] == 2)
                        {
                            finalImage[row][pixel] = Layers[layer].Rows[row][pixel];
                        }
                    }
                }
            }

            var finalImageString = new List<string>();
            foreach(var row in finalImage)
            {
                var rowString = "";
                foreach(var i in row)
                {
                    rowString += i.ToString().Replace("0", " ");
                }
                finalImageString.Add(rowString);
            }

            return finalImageString;
        }
    }
}
