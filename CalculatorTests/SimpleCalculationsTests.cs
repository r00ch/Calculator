using Calculator;
using NUnit.Framework;
using ONP;

namespace CalculatorTests
{
	[TestFixture]
	public class SimpleCalculationsTests
	{
		[TestCase("+", 2, 5, 7)]
		[TestCase("-", 2, 5, -3)]
		[TestCase("*", 2, 5, 10)]
		[TestCase("/", 6, 2, 3)]
		public void Calculate_CalculatesAllPossibleComputations(string operation, double lhs, double rhs, double expectation)
		{
			var result = SimpleCalculations.Calculate(operation, lhs, rhs);

			Assert.That(result, Is.EqualTo(expectation));
		}

		[Test]
		public void Calculate_ThrowsIncorrectOperation()
		{
			TestDelegate t = () => SimpleCalculations.Calculate("!", 1, 1);

			Assert.Throws<IncorrectOperationStringException>(t);
		}

	}
}
