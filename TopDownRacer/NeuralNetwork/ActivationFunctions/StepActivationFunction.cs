﻿using System;

namespace TopDownRacer.NeuralNetwork.ActivationFunctions
{
    //Implementatie van de step activation function
    public class StepActivationFunction : IActivationFunction
    {
        private double _treshold;

        public StepActivationFunction(double treshold)
        {
            _treshold = treshold;
        }

        public double CalculateOutput(double input)
        {
            return Convert.ToDouble(input > _treshold);
        }
    }
}