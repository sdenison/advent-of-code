using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc.Commands.Communications
{
    public class CommunicationsCommands : Command
    {
        public CommunicationsCommands() : base("communications", "Communications commands")
        {
            Add(new FftCommand());
        }
    }
}
