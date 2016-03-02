using Calculator;

namespace ONP
{
	public class Calculator
	{
		public Calculator(ExpressionTransformer expressionTransformer, IOnpAlgorithm onpAlgorithm)
		{
			this.expressionTransformer = expressionTransformer;
			this.onpAlgorithm = onpAlgorithm;
		}

		public double Calculate(string expression)
		{
			var operationElements = expressionTransformer.GetOperationElements(expression);
			return onpAlgorithm.Calculate(operationElements);
		}

		private ExpressionTransformer expressionTransformer;
		private IOnpAlgorithm onpAlgorithm;
	}
}
