using NumberGenerators.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberGenerators
{
    /// <summary>
    /// This class represents a number generator that returns numbers based on
    /// a given distribution of probabilities.
    /// </summary>
    public class ProbabilisticNumberGenerator : INumberGenerator
    {
        // Values that may be returned by NextNum()
        private List<int> _permissibleValues;

        // Probability of the occurence of each permissible value
        private List<double> _probabilities;

        // Cumulative probabilities of our permissible values
        private List<double> _cumulativeProbabilities;

        // Random number generator used by NextNum()
        private Random _randGenerator;

        /// <summary>
        /// Constructs a RandomGen with the given permissible values and their respevtive probabilities.
        /// </summary>
        /// <param name="permissibleValues">The set of permissible return values</param>
        /// <param name="probabilities">The probabilities of returning the respective permissible value</param>
        public ProbabilisticNumberGenerator(
            IEnumerable<int> permissibleValues,
            IEnumerable<double> probabilities)
        {
            ValidateArgs(permissibleValues, probabilities);
            _permissibleValues = permissibleValues.ToList();
            _probabilities = probabilities.ToList();
        }

        /// <summary>
        /// When this method is called multiple times over a long period, it should return the
        /// permissible values roughly with the initialized probabilities.
        /// </summary>
        /// <returns>
        /// One of the permissible values.
        /// </returns>
        public int NextNum()
        {
            // First we setup an array of cumulative probabilities where _cumulativeProbabilities[n] =
            // _probabilities[n] + _probabilities[n - 1] + ... + _probabilities[0]
            if (_cumulativeProbabilities is null)
            {
                InitializeGenerator();
            }

            // Next, we generate a random number between 0 and 1, then find the index in our
            // cumulative probability array whos value is < that random number. We then return
            // the value at that index in our permissible values array. This will give us the distribution
            // of output values that we are looking for.
            var randomNumber = _randGenerator.NextDouble();
            var finalIndex = _cumulativeProbabilities.Count() - 1;
            for (var i = 0; i < finalIndex; i++)
            {
                if (randomNumber < _cumulativeProbabilities[i])
                {
                    return _permissibleValues[i];
                }
            }

            // It's guaranteed to be the last element if it gets to this point
            return _permissibleValues[finalIndex];
        }

        /// <summary>
        /// This method initializes this class to a useable state by calculating
        /// the cumulative probability array that is required by NextNum()
        /// </summary>
        private void InitializeGenerator()
        {
            _randGenerator = new Random();
            _cumulativeProbabilities = _probabilities.CumulativeSum().ToList();
        }

        /// <summary>
        /// This method is responsible for verifying the input for this class
        /// </summary>
        /// <param name="permissibleValues">An array of permissible values input by the user</param>
        /// <param name="probabilities">An array of probabilities for each permissible value</param>
        private void ValidateArgs(IEnumerable<int> permissibleValues, IEnumerable<double> probabilities)
        {
            // Make sure the user provided random numbers and their respective probabilities.
            if (permissibleValues is null || !permissibleValues.Any())
            {
                throw new ArgumentException("The permissibleValues input cannot be null or empty");
            }

            if (probabilities is null || !probabilities.Any())
            {
                throw new ArgumentException("The probabilities input cannot be null or empty");
            }

            if (permissibleValues.Count() != probabilities.Count())
            {
                throw new ArgumentException("permissibleValues and probabilities must be the same length");
            }

            // The sum of the probabilities must equal 1
            var sumOfProbabilities = probabilities.Aggregate((prob1, prob2) => prob1 + prob2);
            if (!sumOfProbabilities.AppoximatelyEqual(1.0, EPSILON))
            {
                throw new ArgumentException("The input probabilities must sum to 1");
            }
        }

        /// <summary>
        /// The allowable difference for floating point equality
        /// </summary>
        private const double EPSILON = 0.000001;
    }
}
