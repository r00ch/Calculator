using ONP;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
	public class ExpressionTransformer
	{
		public List<OperationElement> GetOperationElements(string expression)
		{
			if (string.IsNullOrEmpty(expression))
				throw new EmptyStringException();
			var operationElements = new List<OperationElement>();
			var builder = new StringBuilder();

			foreach (char character in expression)
			{
				if (char.IsDigit(character))
				{
					builder.Append(character);
				}
				else if (IsOperator(character))
				{
					if (builder.Length != 0)
					{
						operationElements.Add(new OperationElement(OperationElementType.Number, builder.ToString()));
						builder.Clear();
					}
					operationElements.Add(new OperationElement(OperationElementType.Operator, character.ToString()));
				}
				else
				{
					throw new IncorrectOperationStringException(expression);
				}
			}

			if (builder.Length != 0)
				operationElements.Add(new OperationElement(OperationElementType.Number, builder.ToString()));
			return operationElements;
		}

		private bool IsOperator(char character)
		{
			return (character == '+'
				|| character == '-'
				|| character == '*'
				|| character == '/'
				|| character == '('
				|| character == ')');
		}
	}
}
