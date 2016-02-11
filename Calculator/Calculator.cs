
using System;
using System.Collections.Generic;

namespace ONP
{
    public class Calculator
    {
        public object Calculate(string operation)
        {
            var operationElements = OperationToElements(operation);

            throw new NotImplementedException();
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
    }
}
