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
            var elem = calc.OperationToElements("((2+7)/3+(14-3)*4)/2");
            var onpElem = calc.OperationElementsToONP(elem);
            foreach (var el in onpElem)
            {
                Console.WriteLine(el.Value + " " + el.Type);
            }
            Console.ReadLine();
        }
    }
}
