using System;

namespace ONP
{
	public class EmptyStringException : Exception
	{
		public EmptyStringException(string message)
		: base(message)
		{
		}
		public EmptyStringException()
		{
		}
	}
}

