using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp.Utilities
{
    public static class RNG
    {
        static readonly Random _rng = new();
        
        // Gets a "random" value using the configured weights.
        // Not used in this test task but is here to show my awareness how the casino RNG "works".
        public static int GetWeightedValue(List<int> weights, List<int> values)
        {
            if (weights.Count != values.Count)
                throw new ArgumentException("Count of weights and values is not the same");
            
            int sum = weights.Sum();
            int rnd = _rng.Next(0, sum);
            int result = -1;
            
            for (int i = 0; i < weights.Count; i++)
            {
                if (rnd < weights[i])
                {
                    result = values[i];
                    break;
                }
                
                rnd -= weights[i];
            }
            
            return result;
        }
    }
}
