using NUnit.Framework;
using ONP;
using System;
using System.Collections.Generic;

namespace ONP.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Calculate_EmptyString_Throws()
        {
            var calc = new Calculator();
            Assert.Throws<NotImplementedException>(() => calc.Calculate(""));
        }
        [Test]
        public void OperationToElements_invalidOperationString_ThrowsIncorrectOperationStringException()
        {
            var calc = new Calculator();
            Assert.Throws<IncorrectOperationStringException>(() => calc.OperationToElements("2!2"));
        }
        [Test]
        public void OperationToElements_validOperationString_ElementsInQueue()
        {
            var calc = new Calculator();
            var result = calc.OperationToElements("3+2");
            var expectation = new Queue<OperationElement>();
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER,"3"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "+"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "2"));
            Assert.AreEqual(expectation, result);
        }
    }
}
