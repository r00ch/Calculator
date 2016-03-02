using ONP;
using System.Collections.Generic;

namespace Calculator
{
	public class OnpAlgorithm : IOnpAlgorithm
	{
		public double Calculate(IList<OperationElement> operationElements)
		{
			var operationInOnpOrder = SortElementsInOnpOrder(operationElements);
			var operationNumbers = new Stack<double>();
			foreach (var operationElement in operationInOnpOrder)
			{
				if (operationElement.Type == OperationElementType.Number)
				{
					operationNumbers.Push(double.Parse(operationElement.Value));
				}
				else
				{
					var secondNumber = operationNumbers.Pop();
					var firstNumber = operationNumbers.Pop();
					operationNumbers.Push(SimpleCalculations.Calculate(operationElement.Value, firstNumber, secondNumber));
				}
			}
			return operationNumbers.Pop();
		}

		public Queue<OperationElement> SortElementsInOnpOrder(IList<OperationElement> operationElements)
		{
			elementsInOnpOrder.Clear();
			operatorsStack.Clear();
			foreach (var element in operationElements)
			{
				if (element.Type == OperationElementType.Number)
				{
					HandleNumber(element);
				}
				else
				{
					HandleOperator(element);
				}
			}
			HandleRestOperationsOnStack();
			return elementsInOnpOrder;
		}

		private void HandleRestOperationsOnStack()
		{
			while (operatorsStack.Count != 0)
				elementsInOnpOrder.Enqueue(operatorsStack.Pop());
		}

		private void HandleNumber(OperationElement element)
		{
			elementsInOnpOrder.Enqueue(element);
		}

		private void HandleOperator(OperationElement element)
		{
			if (element.Value == "(")
			{
				operatorsStack.Push(element);
			}
			else if (element.Value == ")")
			{
				while (operatorsStack.Peek().Value != "(")
					elementsInOnpOrder.Enqueue(operatorsStack.Pop());
				operatorsStack.Pop();
			}
			else
			{
				while (operatorsStack.Count != 0
					&& GetOperatorPriority(element) <= GetOperatorPriority(operatorsStack.Peek()))
				{
					elementsInOnpOrder.Enqueue(operatorsStack.Pop());
				}
				operatorsStack.Push(element);
			}
		}

		private int GetOperatorPriority(OperationElement element)
		{
			if (element.Value == "(")
				return 0;
			else if (element.Value == "+"
				  || element.Value == "-"
				  || element.Value == ")")
				return 1;
			else return 2;
		}

		private Queue<OperationElement> elementsInOnpOrder = new Queue<OperationElement>();
		private Stack<OperationElement> operatorsStack = new Stack<OperationElement>();
	}
}
