using ONP;
using System.Collections;
using System.Collections.Generic;

namespace CalculatorTests
{
	public class OperationElementsHelper : IEnumerable
	{
		public void Add(OperationElementType type, string value)
		{
			operations.Add(new OperationElement(type, value));
		}

		public IEnumerator GetEnumerator()
		{
			return operations.GetEnumerator();
		}

		readonly List<OperationElement> operations = new List<OperationElement>();
	}
}
