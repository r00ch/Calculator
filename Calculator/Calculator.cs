using System;
using System.Collections.Generic;

namespace ONP
{
    public class Calculator
    {
        public double Calculate(string operation)
        {
			if (operation == "") throw new EmptyStringException();
            var operationElements = OperationToElements(operation);
			var operationONP = OperationElementsToONP(operationElements);

			Stack<double> operationNumbers = new Stack<double>();
			

			foreach (var operationElement in operationONP)
			{
				if (operationElement.Type == OperationElementType.NUMBER)
					operationNumbers.Push(Double.Parse(operationElement.Value));
				else
				{
					var secondNumber = operationNumbers.Pop();
					var firstNumber = operationNumbers.Pop(); //Kolejność ściągania ze stosu jest tutaj kluczowa
					operationNumbers.Push(CommitOperation(operationElement.Value, firstNumber, secondNumber));
				}
			}
			return operationNumbers.Pop();
        }

		private double CommitOperation(string operation, double firstNumber, double secondNumber)
		{
			switch (operation)
			{
				case "+": return firstNumber + secondNumber;
				case "-": return firstNumber - secondNumber;
				case "*": return firstNumber * secondNumber;
				case "/": return firstNumber / secondNumber;
				default: throw new IncorrectOperationStringException(String.Format("Not supported operator used: {0}", operation));
			}
		}

		public Queue<OperationElement> OperationToElements(string operation)
        {
            var operationElements = new Queue<OperationElement>();
            string bufforForLongerNumbers = "";

            foreach (char character in operation)
            {
                if (Char.IsDigit(character)) bufforForLongerNumbers += character;
                else if (IsCharacterAnOperator(character))
                {
                    if (bufforForLongerNumbers != "")
                    {
                        operationElements.Enqueue(new OperationElement(OperationElementType.NUMBER, bufforForLongerNumbers));
                        bufforForLongerNumbers = "";
                    }
                    operationElements.Enqueue(new OperationElement(OperationElementType.OPERATOR,character.ToString()));
                }
                else throw new IncorrectOperationStringException("Incorrect operation string: " + operation);
            }
            if (bufforForLongerNumbers != "") operationElements.Enqueue(new OperationElement(OperationElementType.NUMBER, bufforForLongerNumbers));
            return operationElements;
        }

        private bool IsCharacterAnOperator(char character)
        {
            return (character == '+'
                || character == '-'
                || character == '*'
                || character == '/'
                || character == '('
                || character == ')') ;
        }

        public Queue<OperationElement> OperationElementsToONP(Queue<OperationElement> operationElements)
        {
            Queue<OperationElement> result = new Queue<OperationElement>();
            Stack<OperationElement> operators = new Stack<OperationElement>();
            foreach (var element in operationElements)
            {
                if (element.Type == OperationElementType.NUMBER)
                    result.Enqueue(element);
                else if (element.Value == "(") operators.Push(element);
                else if (element.Value == ")")
                {
                    while (operators.Peek().Value != "(") result.Enqueue(operators.Pop());
                    operators.Pop();
                }
                else if (element.Type == OperationElementType.OPERATOR)
                {
                    while (operators.Count != 0 && DesignateOperatorPriority(element) <= DesignateOperatorPriority(operators.Peek()))
                        result.Enqueue(operators.Pop());
                    operators.Push(element);
                }
            }
            while (operators.Count != 0) result.Enqueue(operators.Pop());
            return result;
        }

        private int DesignateOperatorPriority(OperationElement element)
        {
            if (element.Value == "(")
                return 0;
            else if (element.Value == "+"
                  || element.Value == "-"
                  || element.Value == ")")
                return 1;
            else return 2;
        }

    }
}
