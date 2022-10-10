# Advent of Code

This repository contains code produced to solve Advent of Code challenges we're doing at work.

So far, the only software artifact that has been created is the **IntcodeComputer**.

**IntcodeComputer** has one method and that's **run_program**. **run_program** expects an array of integers (the program you want to run) and a single integer which is the **input** talked about in the problem description.

**IntcodeComputer** returns the final memory after your program runs. There is also a property **output** on the computer object where you can inspect the output that was produced.

If you'd like to have your very own **IntcodeComputer** you can import the package with:

...
py -m pip install --index-url https://test.pypi.org/simple --no-deps compute_package_sdenison
...

In the day five problem description, a program is given that outputs whatever is input. You can run that program and inspect the output by running:

...
from compute_package_sdenison import compute

computer = compute.IntcodeComputer()
program = [ 3, 0, 4, 0, 99 ]
memory_after_program_runs = computer.run_program(program, 55)
print(computer.output)
print(memory_after_program_runs)
...