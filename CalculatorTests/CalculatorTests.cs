using NUnit.Framework;
using ReversePolishNotationCalculator;

namespace ReversePolishNotationCalculatorTests
{
    [TestFixture]
    class CalculatorTests
    {
        [Test]
        double Calculate_EmptyString_Throws()
        {
            var calc = new Calculator();
            var result = calc.Calculate("");
        }
    }
}
