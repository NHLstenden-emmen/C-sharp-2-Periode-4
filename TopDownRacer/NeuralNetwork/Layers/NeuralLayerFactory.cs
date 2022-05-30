using System;
using System.Collections.Generic;
using System.Text;
using TopDownRace.NeuralNetwork.Neuron;
using TopDownRacer.NeuralNetwork.ActivationFunctions;
using TopDownRacer.NeuralNetwork.InputFunctions;

namespace TopDownRacer.NeuralNetwork.Layers
{
    class NeuralLayerFactory
    {
        //Een factory die gebruikt kan worden om neural layers te maken
        public NeuralLayer CreateNeuralLayer(int numberOfNeurons, IActivationFunction activationFunction, IInputFunction inputFunction)
        {
            var layer = new NeuralLayer();

            for (int i = 0; i < numberOfNeurons; i++)
            {
                var neuron = new Neuron(activationFunction, inputFunction);
                layer.Neurons.Add(neuron);
            }

            return layer;
        }
    }
}
