using System;
using System.Diagnostics;

namespace TopDownRacer.NeuralNetwork.ActivationFunctions
{
    //Implementatie van de sigmoid activation function
    public class SigmoidActivationFunction : IActivationFunction
    {
        private double _coeficient;

        public SigmoidActivationFunction(double coeficient)
        {
            _coeficient = coeficient;
        }

        public double CalculateOutput(double input)
        {

            //Debug.WriteLine(input + " / " + (1 / (1 + Math.Exp(-(input/500) * _coeficient))));
            return (1 / (1 + Math.Exp(-(input/500) * _coeficient)));
        }
    }
}
