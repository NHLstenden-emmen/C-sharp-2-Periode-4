using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TopDownRacer.NeuralNetwork.Synapses;

namespace TopDownRacer.NeuralNetwork.InputFunctions
{
    //Implementatie van de weighted sum functie
    public class WeightedSumFunction : IInputFunction
    {
        public double CalculateInput(List<ISynapse> inputs)
        {
            //Debug.WriteLine("input = " + inputs.Select(x => x.Weight * x.GetOutput()).Sum());
            return inputs.Select(x => x.Weight * x.GetOutput()).Sum();
        }
    }
}
