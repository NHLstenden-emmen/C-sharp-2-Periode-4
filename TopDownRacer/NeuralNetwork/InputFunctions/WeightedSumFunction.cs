using System.Collections.Generic;
using System.Linq;
using TopDownRacer.NeuralNetwork.Synapses;

namespace TopDownRacer.NeuralNetwork.InputFunctions
{
    //Implementatie van de weighted sum functie
    public class WeightedSumFunction : IInputFunction
    {
        public double CalculateInput(List<ISynapse> inputs)
        {
            return inputs.Select(x => x.Weight * x.GetOutput()).Sum();
        }
    }
}
