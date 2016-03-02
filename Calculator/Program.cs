using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONP
{
    static class Program
    {
        static void Main()
        {
            Calculator calc = new Calculator();
			Console.WriteLine(calc.Calculate("((2+7)/3+(14-3)*4)/2"));
			Console.WriteLine(calc.Calculate("((2+7)/3+(14-3)*4)"));
			Console.ReadLine();
        }
    }
}
