using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Aoc.Spaceship.Unit.Tests.PropulsionTests
{
    [TestFixture]
    public class AsyncTests
    {
        [Test]
        public async Task TryAsyncIdeas()
        {
            var outputGenerator = new IGenerateOutput();
            var inputAcceptor = new IAcceptInput(outputGenerator);

            var outputTask = outputGenerator.GenerateOutput();
            var inputTask = inputAcceptor.PollForInput();
            Task.WaitAll(outputTask, inputTask);

            Assert.IsNotEmpty(inputAcceptor.Output);
        }
    }

    public class IGenerateOutput
    {
        private int? _outputValue = null;


        public async Task GenerateOutput()
        {
            var i = 0;
            while (i < 40)
            {
                await Task.Delay(10);
                _outputValue = i;
                i++;
            }
        }

        public async Task<int?> GetNewDataAsync()
        {
            var tries = 0;
            while (_outputValue.HasValue == false && tries < 10)
            {
                await Task.Delay(10);
                tries++;
            }
            var returnValue = _outputValue;
            _outputValue = null;

            return returnValue;
        }

    }

    public class IAcceptInput
    {
        public List<int?> Output { get; set; }
        public bool KeepGoing { get; set; }

        private IGenerateOutput _outputGerGenerator;
        public IAcceptInput(IGenerateOutput outputGenerator)
        {
            Output = new List<int?>();
            KeepGoing = true;
            _outputGerGenerator = outputGenerator;
        }


        public async Task PollForInput()
        {
            var i = 0;
            while (KeepGoing && i < 100)
            {
                var newInput = await _outputGerGenerator.GetNewDataAsync();
                if (newInput.HasValue)
                    Output.Add(newInput);
                i++;
            }
        }
    }
}
