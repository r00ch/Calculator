using System.Collections.Generic;
using System.Text; //dla StringBuilder

namespace ONP
{
	public class Calculator
    {
        public double Calculate(string expression)
        {
			//a co jak null? proponuję:
			if (string.IsNullOrEmpty(expression)) throw new EmptyStringException();
            var operationElements = GetOperationElements(expression);
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
					var firstNumber = operationNumbers.Pop(); //Kolejność ściągania ze stosu jest tutaj kluczowa
					operationNumbers.Push(CalculateOperation(operationElement.Value, firstNumber, secondNumber));
				}
			}
			return operationNumbers.Pop();
        }

		//nazwy metod powinny zawierać w sobie czasownik, dzięki temu od razu można odgadnąć, co robi dana metoda
		//czy potrzeba tutaj kolejki? Tutaj następuje zwykła transformacja
		public List<OperationElement> GetOperationElements(string operation)
        {
            var operationElements = new List<OperationElement>();
			//buffor w postaci stringa to zły pomysł
			//stringi w C# są niemutuwalne, czyli dodając znak do stringa nie zmieniasz wcześniejszej instancji
			//lecz dostajesz nową instancję!!
			//istnieje dedykowany mechanizm StringBuilder dla takich rzeczy
			var builder = new StringBuilder();

            foreach (char character in operation)
            {
				//u mnie w pracy jest konwencja, że jeżeli gdziekolwiek w warunku
				//zapiszesz w {} to wszystkie inne też należy pisać
				//we wcześniejszej pracy chcieli zawsze pisać w {}
				//wynika to z tego, że łatwo się pomylić
				//patrz przykład w nowym pliku ExampleIfProblem.txt
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
					throw new IncorrectOperationStringException(operation);
				}
            }
            if (builder.Length != 0)
				operationElements.Add(new OperationElement(OperationElementType.Number, builder.ToString()));
            return operationElements;
        }

		//podobnie jak wcześniej nie ma czasownika i nie do końca wiadomo co robi metoda
		//ta metoda również nie musiałaby zwracać kolejki, lecz mogłaby listę
		public Queue<OperationElement> SortElementsInOnpOrder(IList<OperationElement> operationElements)
        {
            var elementsInOnpOrder = new Queue<OperationElement>();
            var operatorsStack = new Stack<OperationElement>();
            foreach (var element in operationElements)
            {
				if (element.Type == OperationElementType.Number)
				{
					elementsInOnpOrder.Enqueue(element);
				}
				else //wiem, że mamy zagnieżdżenie jednakże gdybyśmy to wyciągnęli to możemy to jeszcze ładniej zapisać
				{
					if (element.Value == "(")
					{
						operatorsStack.Push(element);
					}
					else if (element.Value == ")")
					{
						//? ta pętla się zapętla?!
						while (operatorsStack.Peek().Value != "(")
							elementsInOnpOrder.Enqueue(operatorsStack.Pop());
						operatorsStack.Pop();
					}
					else
					{
						while (operatorsStack.Count != 0 && GetOperatorPriority(element) <= GetOperatorPriority(operatorsStack.Peek()))
							elementsInOnpOrder.Enqueue(operatorsStack.Pop());
						operatorsStack.Push(element);
					}
				}
            }
            while (operatorsStack.Count != 0)
				elementsInOnpOrder.Enqueue(operatorsStack.Pop());
            return elementsInOnpOrder;
        }

		//Jedna z konwencji proponuje mieć publiczne rzeczy na górze potem prywatne
		//są różne konwencje
		//ważne by zespół miał wspólną, dzieki czemu łatwiej się czyta kod, bo wiadomo, gdzie spodziewać się danych rzeczy

		//Preferuję proste czasowniki :P
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

		//Commit - nie kojarzy mi się z tą operacją
		//http://www.oxforddictionaries.com/definition/english/commit przykłady użycia też nie odnoszą się raczej do tego
		//Tutaj chcesz wyliczyć daną operację
		private double CalculateOperation(string operation, double firstNumber, double secondNumber)
		{
			switch (operation)
			{
				case "+": return firstNumber + secondNumber;
				case "-": return firstNumber - secondNumber;
				case "*": return firstNumber * secondNumber;
				case "/": return firstNumber / secondNumber;
				default: throw new IncorrectOperationStringException(operation);
			}
		}

		//proponuję krótszą nazwę
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
