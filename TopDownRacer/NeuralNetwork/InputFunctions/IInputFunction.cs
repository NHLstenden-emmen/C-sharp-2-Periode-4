using System.Collections.Generic;
using TopDownRacer.NeuralNetwork.Synapses;

namespace TopDownRacer.NeuralNetwork.InputFunctions
{
    //Interface voor de input functions
    public interface IInputFunction
    {
        double CalculateInput(List<ISynapse> inputs);
    }
}