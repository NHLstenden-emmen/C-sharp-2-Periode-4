﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopDownRace.NeuralNetwork.Neuron;

namespace TopDownRacer.NeuralNetwork.Layers
{
    
    //Implementatie van een laag in een Neural Network
    class NeuralLayer
    {
        public List<INeuron> Neurons;

        public NeuralLayer()
        {
            Neurons = new List<INeuron>();
        }

        //Verbinden van twee lagen
        public void ConnectLayers(NeuralLayer inputLayer)
        {
            var combos = Neurons.SelectMany(neuron => inputLayer.Neurons, (neuron, input) => new { neuron, input });
            combos.ToList().ForEach(x => x.neuron.AddInputNeuron(x.input));
        }
    }
}