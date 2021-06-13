using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NumberGenerators.Tests
{
    [TestClass]
    public class NumberGeneratorTests
    {

        [TestMethod]
        public void RandomGen_NullValues()
        {
            int[] values = null;
            double[] probs = { 1 };
            Assert.ThrowsException<ArgumentException>(() => new ProbabilisticNumberGenerator(values, probs));
        }

        [TestMethod]
        public void RandomGen_NullProbabilities()
        {
            int[] values = { 5 };
            double[] probs = null;
            Assert.ThrowsException<ArgumentException>(() => new ProbabilisticNumberGenerator(values, probs));
        }

        [TestMethod]
        public void RandomGen_NullValsAndProbs()
        {
            int[] values = null;
            double[] probs = null;
            Assert.ThrowsException<ArgumentException>(() => new ProbabilisticNumberGenerator(values, probs));
        }

        [TestMethod]
        public void RandomGen_EmptyValues()
        {
            int[] values = { };
            double[] probs = { 1 };
            Assert.ThrowsException<ArgumentException>(() => new ProbabilisticNumberGenerator(values, probs));
        }

        [TestMethod]
        public void RandomGen_EmptyProbabilities()
        {
            int[] values = { 5 };
            double[] probs = { };
            Assert.ThrowsException<ArgumentException>(() => new ProbabilisticNumberGenerator(values, probs));
        }

        [TestMethod]
        public void RandomGen_EmptyValsAndProbs()
        {
            int[] values = { };
            double[] probs = { };
            Assert.ThrowsException<ArgumentException>(() => new ProbabilisticNumberGenerator(values, probs));
        }

        [TestMethod]
        public void RandomGen_DifferentSizes()
        {
            int[] values = { 5, 2 };
            double[] probs = { 1 };
            Assert.ThrowsException<ArgumentException>(() => new ProbabilisticNumberGenerator(values, probs));
        }

        [TestMethod]
        public void RandomGen_SingleValue()
        {
            int[] values = { 5 };
            double[] probs = { 1 };
            var randomGen = new ProbabilisticNumberGenerator(values, probs);
            for (var i = 0; i < 100; i++)
            {
                Assert.AreEqual(5, randomGen.NextNum());
            }
        }

        [TestMethod]
        public void RandomGen_ProbsSumOverOne()
        {
            int[] values = { 5 };
            double[] probs = { 0.4, 0.4, 0.21 };
            Assert.ThrowsException<ArgumentException>(() => new ProbabilisticNumberGenerator(values, probs));
        }

        [TestMethod]
        public void RandomGen_ProbsSumUnderOne()
        {
            int[] values = { 5 };
            double[] probs = { 0.4, 0.4, 0.19 };
            Assert.ThrowsException<ArgumentException>(() => new ProbabilisticNumberGenerator(values, probs));
        }
    }
}