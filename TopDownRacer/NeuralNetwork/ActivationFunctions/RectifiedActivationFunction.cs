using System;

namespace TopDownRacer.NeuralNetwork.ActivationFunctions
{
    //Implementatie van de rectifier activation function
    public class RectifiedActivationFuncion : IActivationFunction
    {
        public double CalculateOutput(double input)
        {
            return Math.Max(0, input);
        }
    }
}