using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNPTPZ1;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTest
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber(10, 20);
            ComplexNumber b = new ComplexNumber(1, 2);
            ComplexNumber shouldBe = new ComplexNumber(11, 22);
            ComplexNumber actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            string expectedString = "(10 + 20i)";
            string resultString = a.ToString();
            Assert.AreEqual(expectedString, resultString);

            expectedString = "(1 + 2i)";
            resultString = b.ToString();
            Assert.AreEqual(expectedString, resultString);

            a = new ComplexNumber(1, -1);
            b = new ComplexNumber(0, 0);
            shouldBe = new ComplexNumber(1, -1);
            actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            expectedString = "(1 + -1i)";
            resultString = a.ToString();
            Assert.AreEqual(expectedString, resultString);

            expectedString = "(0 + 0i)";
            resultString = b.ToString();
            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber(1, 0));
            polynomial.Coefficients.Add(new ComplexNumber(0, 0));
            polynomial.Coefficients.Add(new ComplexNumber(1, 0));
            ComplexNumber result = polynomial.Eval(new ComplexNumber(0, 0));
            ComplexNumber expected = new ComplexNumber(1, 0);
            Assert.AreEqual(expected, result);

            result = polynomial.Eval(new ComplexNumber(1, 0));
            expected = new ComplexNumber(2, 0);
            Assert.AreEqual(expected, result);

            result = polynomial.Eval(new ComplexNumber(2, 0));
            expected = new ComplexNumber(5.0000000000, 0);
            Assert.AreEqual(expected, result);

            var resultString = polynomial.ToString();
            var expectedString = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expectedString, resultString);
        }
    }
}


