using System;
using System.Collections.Generic;
using TopDownRacer.NeuralNetwork.Synapses;

namespace TopDownRace.NeuralNetwork.Neuron
{
    //Interface van een Neuron
    public interface INeuron
    {
        Guid Id { get; }
        double PreviousPartialDerivate { get; set; }

        List<ISynapse> Inputs { get; set; }
        List<ISynapse> Outputs { get; set; }

        void AddInputNeuron(INeuron inputNeuron);
        void AddOutputNeuron(INeuron inputNeuron);
        double CalculateOutput();

        void AddInputSynapse(double inputValue);
        void PushValueOnInput(double inputValue);
    }
}