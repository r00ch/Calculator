using NUnit.Framework;
using ONP;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
        }
        [Test]
        public void OperationToElements_validOperationString_ElementsInQueue2()
        {
            var calc = new Calculator();
            var result = calc.OperationToElements("(3+2)-2");
            var expectation = new Queue<OperationElement>();
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "("));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "3"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "+"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "2"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, ")"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "-"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "2"));
            Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
        }
        [Test]
        public void OperationElementsToONP_validOperationElements_ElementsInONPQueue()
        {
            var calc = new Calculator();
            var operationElements = new Queue<OperationElement>();
            operationElements = calc.OperationToElements("3+2");
            var expectation = new Queue<OperationElement>();
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "3"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "2"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "+"));
            var result = calc.OperationElementsToONP(operationElements);
            Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
        }
        [Test]
        public void OperationElementsToONP_validOperationElements_ElementsInONPQueue2() 
        {
            var calc = new Calculator();
            var operationElements = new Queue<OperationElement>();
            operationElements = calc.OperationToElements("2+3*5");
            var expectation = new Queue<OperationElement>();
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "2"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "3"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "5"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "*"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "+"));
            var result = calc.OperationElementsToONP(operationElements);
            Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
        }
        [Test]
        public void OperationElementsToONP_validOperationElements_ElementsInONPQueue3()
        {
            var calc = new Calculator();
            var operationElements = new Queue<OperationElement>();
            operationElements = calc.OperationToElements("(2+3)*5");
            var expectation = new Queue<OperationElement>();
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "2"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "3"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "+"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "5"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "*"));
            var result = calc.OperationElementsToONP(operationElements);
            Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
        }
        [Test]
        public void OperationElementsToONP_validOperationElements_ElementsInONPQueue4()
        {
            var calc = new Calculator();
            var operationElements = new Queue<OperationElement>();
            operationElements = calc.OperationToElements("((2+7)/3+(14-3)*4)/2");
            var expectation = new Queue<OperationElement>();
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "2"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "7"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "+"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "3"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "/"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "14"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "3"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "-"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "4"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "*"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "+"));
            expectation.Enqueue(new OperationElement(OperationElementType.NUMBER, "2"));
            expectation.Enqueue(new OperationElement(OperationElementType.OPERATOR, "/"));
            var result = calc.OperationElementsToONP(operationElements);
            Assert.AreEqual(expectation.Select(r => r.ToTestValue()), result.Select(r => r.ToTestValue()));
        }
    }
    public static class TestValues
    {
        public static string ToTestValue(this OperationElement oe)
        {
            return $"type: {oe.Type} value: {oe.Value}";
        }
    }
}
