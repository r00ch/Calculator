namespace ONP
{
	//w mojej konwencji wolę mieć konstruktor na górze (rzecz gustu i przyzwyczajeń)
	public class OperationElement
    {
		public OperationElement(OperationElementType type, string value)
		{
			Type = type;
			Value = value;
		}

		public OperationElementType Type { get; }
        public string Value { get; }
    }
}   
