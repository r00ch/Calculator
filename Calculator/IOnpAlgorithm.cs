using ONP;
using System.Collections.Generic;

namespace Calculator
{
	public interface IOnpAlgorithm
	{
		double Calculate(IList<OperationElement> operationElements);
	}
}
