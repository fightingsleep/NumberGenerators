using System;
using System.Collections.Generic;
using System.Text;

namespace NumberGenerators
{
    /// <summary>
    /// This class contains a set of extension methods that are commonly used
    /// </summary>
    public static class CommonExtensionMethods
    {
        /// <summary>
        /// This extension method returns an <see cref="IEnumerable{T}"> where each
        /// element is the sum of every element before it in the original sequence.
        /// </summary>
        /// <param name="sequence">The <see cref="IEnumerable{T}"> of elements to cumulatively sum</param>
        /// <returns>The resultant <see cref="IEnumerable{T}"> of cumulatively summed values</returns>
        public static IEnumerable<double> CumulativeSum(this IEnumerable<double> sequence)
        {
            double sum = 0;
            foreach (var el in sequence)
            {
                sum += el;
                yield return sum;
            }
        }

        /// <summary>
        /// This method determines if two floating double numbers are approximately equal
        /// given some allowable threshold
        /// </summary>
        /// <param name="left">The first number</param>
        /// <param name="right">The second number</param>
        /// <param name="threshold">The allowable difference between the numbers</param>
        /// <returns>True if the numbers are approximately equal, false otherwise</returns>
        public static bool AppoximatelyEqual(this double left, double right, double threshold)
        {
            if (Math.Abs(left - right) < threshold)
            {
                return true;
            }

            return false;
        }
    }
}
