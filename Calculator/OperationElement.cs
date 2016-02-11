using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONP
{
    public class OperationElement
    {
        public OperationElementType Type { get; }
        public string Value { get; }
        public OperationElement(OperationElementType type, string value)
        {
            Type = type;
            Value = value;
        }
        
    }
}   
