using ONP;

namespace Calculator
{
	public class SimpleCalculations
	{
		public static double Calculate(string operation, double leftHandSideNumber, double rightHandSideNumber)
		{
			switch (operation)
			{
				case "+": return leftHandSideNumber + rightHandSideNumber;
				case "-": return leftHandSideNumber - rightHandSideNumber;
				case "*": return leftHandSideNumber * rightHandSideNumber;
				case "/": return leftHandSideNumber / rightHandSideNumber;
				default: throw new IncorrectOperationStringException(operation);
			}
		}
	}
}
