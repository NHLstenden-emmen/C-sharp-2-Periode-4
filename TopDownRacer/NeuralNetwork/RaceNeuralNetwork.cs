using System;
using System.Collections.Generic;
using System.Linq;
using TopDownRacer.NeuralNetwork.ActivationFunctions;
using TopDownRacer.NeuralNetwork.InputFunctions;
using TopDownRacer.NeuralNetwork.Layers;

namespace TopDownRacer.NeuralNetwork
{
    // The neural network class that will be used to play the topdownracing game
    internal class RaceNeuralNetwork
    {
        private NeuralLayerFactory _layerFactory;

        internal List<NeuralLayer> _layers;
        internal double _learningRate;
        internal double[][] _expectedResult;

        // This is the constructor to create the neural network
        // This function will also create the input layer of the neural network
        public RaceNeuralNetwork(int numberOfInputNeurons)
        {
            _layers = new List<NeuralLayer>();
            _layerFactory = new NeuralLayerFactory();

            CreateInputLayer(numberOfInputNeurons);

            _learningRate = 2.95;
        }

        // This function is used to add a layer to the neural network
        // This function will also change the last layer to the output layer
        public void AddLayer(NeuralLayer newLayer)
        {
            //any() kijkt of een element in een sequence voldoet aan een condition
            if (_layers.Any())
            {
                var lastLayer = _layers.Last();
                newLayer.ConnectLayers(lastLayer);
            }

            _layers.Add(newLayer);
            //_neuronErrors.Add(_layers.Count - 1, new double[newLayer.Neurons.Count]);
        }

        // This function is used to set the input values which will be used to generate the output
        public void PushInputValues(double[] inputs)
        {
            _layers.First().Neurons.ForEach(x => x.PushValueOnInput(inputs[_layers.First().Neurons.IndexOf(x)]));
        }

        // This function is used to set the expected values so they can be used to train the neural network
        public void PushExpectedValues(double[][] expectedOutputs)
        {
            _expectedResult = expectedOutputs;
        }

        // This function is used to calculate the output
        public List<double> GetOutput()
        {
            var returnValue = new List<double>();

            _layers.Last().Neurons.ForEach(neuron =>
            {
                returnValue.Add(neuron.CalculateOutput());
            });

            return returnValue;
        }

        // This function is used to train the neural network.
        // The number of epochs is the amount of passes that will be done over the entire dataset
        public void Train(double[][] inputs, int numberOfEpochs)
        {

            for (int i = 0; i < numberOfEpochs; i++)
            {
                for (int row = 0; row < inputs.GetLength(0); row++)
                {
                    PushInputValues(inputs[row]);

                    var outputs = new List<double>();

                    _layers.Last().Neurons.ForEach(neuron => { outputs.Add(neuron.CalculateOutput()); });

                    HandleOutputLayer(row);
                    HandleHiddenLayers();
                }
            }
        }


        // This function creates the input layer and the connected input neurons
        private void CreateInputLayer(int numberOfInputNeurons)
        {
            var inputLayer = _layerFactory.CreateNeuralLayer(numberOfInputNeurons, new RectifiedActivationFuncion(), new WeightedSumFunction());
            inputLayer.Neurons.ForEach(x => x.AddInputSynapse(0));
            this.AddLayer(inputLayer);
        }

        // This function changes the weight of the synapses of the output layer based on the results
        private void HandleOutputLayer(int row)
        {
            _layers.Last().Neurons.ForEach(neuron =>
            {
                neuron.Inputs.ForEach(connection =>
                {
                    var output = neuron.CalculateOutput();
                    var netInput = connection.GetOutput();

                    var expectedOutput = _expectedResult[row][_layers.Last().Neurons.IndexOf(neuron)];

                    var nodeDelta = (expectedOutput - output) * output * (1 - output);
                    var delta = -1 * netInput * nodeDelta;
                    //Debug.WriteLine(delta + " / " + nodeDelta + " / " + netInput + " / " + expectedOutput + " / " + output);
                    connection.UpdateWeight(_learningRate, delta);

                    neuron.PreviousPartialDerivate = nodeDelta;
                });
            });
        }

        // This function changes the weight of the synapses of the hidden layers based on the results
        private void HandleHiddenLayers()
        {
            for (int k = _layers.Count - 2; k > 0; k--)
            {
                _layers[k].Neurons.ForEach(neuron =>
                {
                    neuron.Inputs.ForEach(connection =>
                    {
                        var output = neuron.CalculateOutput();
                        var netInput = connection.GetOutput();
                        double sumPartial = 0;

                        _layers[k + 1].Neurons
                        .ForEach(outputNeuron =>
                        {
                            outputNeuron.Inputs.Where(i => i.IsFromNeuron(neuron.Id))
                            .ToList()
                            .ForEach(outConnection =>
                            {
                                sumPartial += outConnection.PreviousWeight * outputNeuron.PreviousPartialDerivate;
                            });
                        });

                        var delta = -1 * netInput * sumPartial * output * (1 - output);
                        connection.UpdateWeight(_learningRate, delta);
                    });
                });
            }
        }
    }
}