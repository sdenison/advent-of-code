using System.Collections.Generic;

namespace Aoc.Spaceship.Imaging
{
    public class Layer
    {
        public List<int[]> Rows { get; }

        public Layer()
        {
            Rows = new List<int[]>();
        }

        public void AddRow(int[] rowData)
        {
            Rows.Add(rowData);
        }
    }
}
