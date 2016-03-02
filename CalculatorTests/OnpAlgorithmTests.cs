using Calculator;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorTests
{
	[TestFixture]
	internal class OnpAlgorithmTests
	{
		[TestCase("3+2", new[] { "3", "2", "+" })]
		[TestCase("2+3*5", new[] { "2", "3", "5", "*", "+" })]
		[TestCase("(2+3)*5", new[] { "2", "3", "+", "5", "*" })]
		[TestCase("((2+7)/3+(14-3)*4)/2", new[] { "2", "7", "+", "3", "/", "14", "3", "-", "4", "*", "+", "2", "/" })]
		public void SortElementsInOnpOrder_validOperationElements_ElementsInONPQueue(string expression, IEnumerable<string> expectedValues)
		{
			var algorithm = new OnpAlgorithm();
			var expressionTransformer = new ExpressionTransformer();

			var result = algorithm.SortElementsInOnpOrder(expressionTransformer.GetOperationElements(expression));

			Assert.AreEqual(expectedValues, result.Select(r => r.Value));
		}
	}
}
