using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Spaceship.Utilities
{
    public static class ArrayFunctions
    {
        public static IList<IList<int>> GetPermutations(this int[] nums)
        {
            var list = new List<IList<int>>();
            return DoPermute(nums, 0, nums.Length - 1, list);
        }

        private static IList<IList<int>> DoPermute(int[] nums, int start, int end, IList<IList<int>> list)
        {
            if (start == end)
            {
                // We have one of our possible n! solutions,
                // add it to the list.
                list.Add(new List<int>(nums));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    Swap(ref nums[start], ref nums[i]);
                    DoPermute(nums, start + 1, end, list);
                    Swap(ref nums[start], ref nums[i]);
                }
            }

            return list;
        }

        private static void Swap(ref int a, ref int b)
        {
            (a, b) = (b, a);
        }

        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }

        public static List<string> ToStringList(this IList<IList<int>> intList)
        {
            var returnList = new List<string>();
            foreach (var numbers in intList)
            {
                var line = "";
                foreach (var number in numbers)
                    line += number.ToString();
                returnList.Add(line);
            }
            return returnList;
        }

        public static string ToIntString(this IList<int> intList)
        {
            //var line = "";
            var intStringProgress = 0;
            var line = new StringBuilder();
            foreach (var number in intList)
            {
                line.Append(number.ToString());
                //if (intStringProgress % 1000 == 0)
                //    Console.WriteLine($"ToIntString progress: {intStringProgress}");
                //intStringProgress++;
            }
            return line.ToString();
        }
    }
}
