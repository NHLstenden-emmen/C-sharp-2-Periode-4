using System;

namespace TopDownRacer.NeuralNetwork.Synapses
{
    //interface voor synapses
    public interface ISynapse
    {
        double Weight { get; set; }
        double PreviousWeight { get; set; }
        double GetOutput();

        bool IsFromNeuron(Guid fromNeuronId);
        void UpdateWeight(double learningRate, double delta);
    }
}