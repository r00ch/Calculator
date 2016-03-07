using NUnit.Framework;
using Calculator;
using System;

namespace ONP.Tests
{
	[TestFixture]
	internal class CalculatorTests
	{
		[SetUp]
		public void InitializeCalculator()
		{
			calculator = new Calculator(new ExpressionTransformer(), new OnpAlgorithm());
		}

		[TestCase("-3+2", -1)]
		[TestCase("(-3+2)",-1)]
		public void Calculate_WhenNegativeNumbersAreFirst(string expression, double expected)
		{
			var result = calculator.Calculate(expression);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void Calculate_WhenExpressionContainsJustBracketsAndOperators()
		{
			TestDelegate td= ()=> calculator.Calculate("(){}*/+-");

			Assert.Throws<IncorrectOperationStringException>(td);
		}

		[Test]
		public void Calculate_WhenExpressionIsJustANumber()
		{
			var result = calculator.Calculate("2");

			Assert.That(result, Is.EqualTo(2));
		}

		[Test]
		public void Calculate_DividingByZero()
		{
			TestDelegate td = ()=> calculator.Calculate("2/0");

			Assert.Throws<DivideByZeroException>(td);
		}

		[Test]
        public void Calculate_EmptyString_ThrowsEmptyStringException()
        {
			TestDelegate td = () => calculator.Calculate("");

			Assert.Throws<EmptyStringException>(td);
        }

		[Test]
		public void Calculate_ValidOperationString_Returns1()
		{
			var sumResult = calculator.Calculate("3+2");

			Assert.AreEqual(5, sumResult);
		}

		[Test]
		public void Calculate_WhenAllOperatorUsedInExpression_Returns23_5()
		{
			var resultOfAllOperatorUsedInExpression = calculator.Calculate("((2+7)/3+(14-3)*4)/2");

			Assert.AreEqual(23.5, resultOfAllOperatorUsedInExpression);
		}

		private Calculator calculator;
	}
}
