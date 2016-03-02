using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ONP.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
		[TestCase("-3+2", -1)]
		[TestCase("(-3+2)",-1)]
		public void Calculate_WhenNegativeNumbersAreFirst(string expression, double expected)
		{
			var calc = new Calculator();

			var result = calc.Calculate(expression);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void Calculate_WhenExpressionContainsJustBracketsAndOperators()
		{
			var calc = new Calculator();

			TestDelegate td= ()=> calc.Calculate("(){}*/+-");

			Assert.Throws<IncorrectOperationStringException>(td);
		}

		[Test]
		public void Calculate_WhenExpressionIsJustANumber()
		{
			var calc = new Calculator();

			var result = calc.Calculate("2");

			Assert.That(result, Is.EqualTo(2));
		}

		[Test]
		public void Calculate_DividingByZero()
		{
			var calc = new Calculator();

			TestDelegate td = ()=> calc.Calculate("2/0");

			Assert.Throws<DivideByZeroException>(td);
		}

		[Test]
        public void Calculate_EmptyString_ThrowsEmptyStringException()
        {
            var calc = new Calculator();
            Assert.Throws<EmptyStringException>(() => calc.Calculate(""));
        }
		[Test]
		public void Calculate_EmptyString_ThrowsEmptyStringException()
		{
			//Assign
			var calc = new Calculator();

			//Act
			TestDelegate t = () => calc.Calculate("");

			//Assert
			Assert.Throws<EmptyStringException>(t);
		}

		[Test]
		public void Calculate_ValidOperationString_Returns1()
		{
			var calc = new Calculator();
			var expectation = 5;

			var sumResult = calc.Calculate("3+2");

			Assert.AreEqual(expectation, sumResult);
		}

		[Test]
		public void Calculate_WhenAllOperatorUsedInExpression_Returns23_5()
		{
			var calc = new Calculator();

			var resultOfAllOperatorUsedInExpression = calc.Calculate("((2+7)/3+(14-3)*4)/2");

			var expectation = 23.5;
			Assert.AreEqual(expectation, resultOfAllOperatorUsedInExpression);
		}

		[Test]
		public void GetOperationElements_InvalidOperationString_ThrowsIncorrectOperationStringException()
		{
			var calc = new Calculator();

			TestDelegate t = () => calc.GetOperationElements("2!2");

			Assert.Throws<IncorrectOperationStringException>(t);
		}
		[Test]
		public void GetOperationElements_ValidOperationString_ElementsInQueue()
		{
			var calc = new Calculator();
			var expectation = new OperationElements
			{
				{ OperationElementType.Number,"3" },
				{ OperationElementType.Operator, "+"},
				{ OperationElementType.Number, "2"}
			}.Cast<OperationElement>();

			var result = calc.GetOperationElements("3+2");

			Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
		}

		[Test]
		public void OperationToElements_validOperationString_ElementsInQueue2()
		{
			var calc = new Calculator();
			var expectation = new OperationElements
			{
				{ OperationElementType.Operator, "(" },
				{ OperationElementType.Number, "3" },
				{ OperationElementType.Operator, "+" },
				{ OperationElementType.Number, "2" },
				{ OperationElementType.Operator, ")" },
				{ OperationElementType.Operator, "-" },
				{ OperationElementType.Number, "2" }
			}.Cast<OperationElement>();

			var result = calc.GetOperationElements("(3+2)-2");

			Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
		}

		[Test]
		public void SortElementsInOnpOrder_validOperationElements_ElementsInONPQueue()
		{
			var calc = new Calculator();
			var operationElements = calc.GetOperationElements("3+2");
			var expectation = new OperationElements
			{
				{ OperationElementType.Number, "3" },
				{ OperationElementType.Number, "2" },
				{ OperationElementType.Operator, "+" }
			}.Cast<OperationElement>();

			var result = calc.SortElementsInOnpOrder(operationElements);

			Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
		}
		[Test]
		public void SortElementsInOnpOrder_validOperationElements_ElementsInONPQueue2()
		{
			var calc = new Calculator();
			var operationElements = calc.GetOperationElements("2+3*5");
			var expectation = new OperationElements
			{
				{ OperationElementType.Number, "2" },
				{ OperationElementType.Number, "3" },
				{ OperationElementType.Number, "5" },
				{ OperationElementType.Operator, "*" },
				{ OperationElementType.Operator, "+" }
			}.Cast<OperationElement>();

			var result = calc.SortElementsInOnpOrder(operationElements);

			Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
		}
		[Test]
		public void SortElementsInOnpOrder_validOperationElements_ElementsInONPQueue3()
		{
			var calc = new Calculator();
			var operationElements = calc.GetOperationElements("(2+3)*5");
			var expectation = new OperationElements
			{
				{ OperationElementType.Number, "2" },
				{ OperationElementType.Number, "3"},
				{ OperationElementType.Operator, "+"},
				{ OperationElementType.Number, "5"},
				{ OperationElementType.Operator, "*"}
			}.Cast<OperationElement>();

			var result = calc.SortElementsInOnpOrder(operationElements);

			Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
		}
		[Test]
		public void SortElementsInOnpOrder_validOperationElements_ElementsInONPQueue4()
		{
			var calc = new Calculator();
			var operationElements = calc.GetOperationElements("((2+7)/3+(14-3)*4)/2");
			var expectation = new OperationElements
			{
				{ OperationElementType.Number, "2" },
				{ OperationElementType.Number, "7" },
				{ OperationElementType.Operator, "+" },
				{ OperationElementType.Number, "3" },
				{ OperationElementType.Operator, "/" },
				{ OperationElementType.Number, "14" },
				{ OperationElementType.Number, "3" },
				{ OperationElementType.Operator, "-" },
				{ OperationElementType.Number, "4" },
				{ OperationElementType.Operator, "*" },
				{ OperationElementType.Operator, "+" },
				{ OperationElementType.Number, "2" },
				{ OperationElementType.Operator, "/" }
			}.Cast<OperationElement>();

			var result = calc.SortElementsInOnpOrder(operationElements);

			Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
		}

		private class OperationElements : IEnumerable
		{
			public void Add(OperationElementType type, string value)
			{
				operations.Add(new OperationElement(type, value));
			}

			public IEnumerator GetEnumerator()
			{
				return operations.GetEnumerator();
			}

			internal double Select(Func<object, string> p)
			{
				throw new NotImplementedException();
			}

			readonly List<OperationElement> operations = new List<OperationElement>();
		}
	}

	public static class TestValues
	{
		public static string ToTestValue(this OperationElement oe)
		{
			return $"type: {oe.Type} value: {oe.Value}";
		}
	}
}
