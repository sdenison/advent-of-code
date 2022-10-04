using System;

namespace Aoc.Domain.Compute.Instructions
{
    public class Display : IOperateOnInputInstruction
    {
        public int Length => 2;
        public int Input { get; set; }

        public void DoOperation()
        {
            Console.WriteLine(Input);
        }
    }
}
