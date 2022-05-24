﻿using System;
using TopDownRace.NeuralNetwork.Neuron;
using TopDownRacer.NeuralNetwork.Synapses;

namespace TopDownRace.NeuralNetwork.Synapses
{
    //Implementatie van een Synapse
    public class Synapse : ISynapse
    {
        internal INeuron _fromNeuron;
        internal INeuron _toNeuron;

        //De weging van de connectie
        public double Weight { get; set; }

        //Weging van de vorige iteratie, belangrijk voor he training proces
        public double PreviousWeight { get; set; }

        public Synapse(INeuron fromNeuraon, INeuron toNeuron, double weight)
        {
            _fromNeuron = fromNeuraon;
            _toNeuron = toNeuron;

            Weight = weight;
            PreviousWeight = 0;
        }

        public Synapse(INeuron fromNeuraon, INeuron toNeuron)
        {
            _fromNeuron = fromNeuraon;
            _toNeuron = toNeuron;

            var tmpRandom = new Random();
            Weight = tmpRandom.NextDouble();
            PreviousWeight = 0;
        }

        //Haal de output waarde van de connectie
        //Returned de output waarde van de connectie
        public double GetOutput()
        {
            return _fromNeuron.CalculateOutput();
        }

        //Controleert of een neuron een bepaald nummer als input neuron gebruikt 
        //fromNeuronId is de neuron die wordt gechecked
        // True - Als de neuron gelijk is aan de input
        // False - Als de neuron niet gelijk is aan de input
        public bool IsFromNeuron(Guid fromNeuronId)
        {
            return _fromNeuron.Id.Equals(fromNeuronId);
        }

        //Update van de weging
        public void UpdateWeight(double learningRate, double delta)
        {
            PreviousWeight = Weight;
            Weight += learningRate * delta;
        }
    }
}