using Calculator;
using NUnit.Framework;
using ONP;
using System.Linq;

namespace CalculatorTests
{
	[TestFixture]
	internal class ExpressionTransformerTests
	{
		[Test]
		public void GetOperationElements_InvalidOperationString_ThrowsIncorrectOperationStringException()
		{
			var expressionTransformer = new ExpressionTransformer();

			TestDelegate t = () => expressionTransformer.GetOperationElements("2!2");

			Assert.Throws<IncorrectOperationStringException>(t);
		}
		[Test]
		public void GetOperationElements_ValidOperationString_ElementsInQueue()
		{
			var expressionTransformer = new ExpressionTransformer();
			var expectation = new OperationElementsHelper
			{
				{ OperationElementType.Number,"3" },
				{ OperationElementType.Operator, "+"},
				{ OperationElementType.Number, "2"}
			}.Cast<OperationElement>();

			var result = expressionTransformer.GetOperationElements("3+2");

			Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
		}

		[Test]
		public void GetOperationElements_validOperationString_ElementsInQueue2()
		{
			var expressionTransformer = new ExpressionTransformer();
			var expectation = new OperationElementsHelper
			{
				{ OperationElementType.Operator, "(" },
				{ OperationElementType.Number, "3" },
				{ OperationElementType.Operator, "+" },
				{ OperationElementType.Number, "2" },
				{ OperationElementType.Operator, ")" },
				{ OperationElementType.Operator, "-" },
				{ OperationElementType.Number, "2" }
			}.Cast<OperationElement>();

			var result = expressionTransformer.GetOperationElements("(3+2)-2");

			Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
		}
	}
}
