using System.CommandLine;
using Aoc.Config;
using Aoc.Domain;
using Aoc.Domain.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Aoc.Commands._2019.Day0;

public class AddCommand : Command
{
    public AddCommand() : base("add", "Add values.")
    {
        var value1Option = CreateValue1Option();
        var value2Option = CreateValue2Option();
        this.SetHandler((value1, value2) =>
        {
            Add(value1, value2);
        }, value1Option, value2Option);
    }

    public void Add(int value1, int value2)
    {
        var serviceProvider = DependencyInjection.GetServiceProvider();
        var logger = serviceProvider.GetService<ILogger>();
        var adder = serviceProvider.GetService<IAdder>();
        var sum = adder.Add(value1, value2);
        logger.Log($"Adding {value1} to {value2} = {sum}");
    }

    private Option<int> CreateValue1Option()
    {
        //When set to true the dignostics will write to log if possible.
        var description = "Value 1";
        var valueOption = new Option<int>(new string[2]{"--value-1", "-v1"}, description );
        valueOption.IsRequired = true;
        valueOption.Arity = ArgumentArity.ExactlyOne;
        Add(valueOption);
        return valueOption;
    }

    private Option<int> CreateValue2Option()
    {
        //When set to true the dignostics will write to log if possible.
        var description = "Value 2";
        var valueOption = new Option<int>(new string[2]{"--value-2", "-v2"}, description );
        valueOption.IsRequired = true;
        valueOption.Arity = ArgumentArity.ExactlyOne;
        Add(valueOption);
        return valueOption;
    }
}