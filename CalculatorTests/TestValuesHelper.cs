using ONP;

namespace CalculatorTests
{
	public static class TestValuesHelper
	{
		public static string ToTestValue(this OperationElement oe)
		{
			return $"type: {oe.Type} value: {oe.Value}";
		}
	}
}
