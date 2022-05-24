using TopDownRacer.NeuralNetwork.ActivationFunctions;
using TopDownRacer.NeuralNetwork.InputFunctions;

namespace TopDownRace.NeuralNetwork
{
    //Factory voor het maken van layers
    public class NeuralLayerFactory
    {
        public NeuralLayer CreateNeuralLayer(int numberOfNeurons, IActivationFunction activationFunction, IInputFunction inputFunction)
        {
            var layer = new NeuralLayer();

            for (int i = 0; i < numberOfNeurons; i++)
            {
                var neuron = new Neuron.Neuron(activationFunction, inputFunction);
                layer.Neurons.Add(neuron);
            }

            return layer;
        }
    }
}