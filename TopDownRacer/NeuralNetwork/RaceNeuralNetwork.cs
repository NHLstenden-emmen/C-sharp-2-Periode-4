using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopDownRacer.NeuralNetwork.ActivationFunctions;
using TopDownRacer.NeuralNetwork.InputFunctions;
using TopDownRacer.NeuralNetwork.Layers;

namespace TopDownRacer.NeuralNetwork
{
    //De implementatie van het neural network om de race game te spelen 
    class RaceNeuralNetwork
    {
        //Eerst moet het mogelijk zijn om Neural Layers te maken dus maken we een factory aan
        private NeuralLayerFactory _layerFactory;

        //Hierna moeten we wat basis elementen opstellen die nodig zijn voor het netwerk om te functioneren

        //een lijst met alle neural layers
        internal List<NeuralLayer> _layers;

        //een collectie van de errros van de neuronen aan de hand van een key/value paar
        internal Dictionary<int, double[]> _neuronErrors;

        //De learningrate en verwachte uitkomst 
        internal double _learningRate;
        internal double[][] _expectedResult; 

        //de constructor van het neural network, waarbij de het aantal neurons in de input layer de input is
        public RaceNeuralNetwork(int numberOfInputNeurons)
        {
            //initialiseren van de list, dictionary en factory
            _layers = new List<NeuralLayer>();
            _neuronErrors = new Dictionary<int, double[]>();
            _layerFactory = new NeuralLayerFactory();

            //ook moet een input layer worden aangemaakt om inputs te verzamelen
            CreateInputLayer(numberOfInputNeurons);

            _learningRate = 2.95;
        }

        //een methode om een neural layer toe te voegen aan het neural network
        //deze worden automatisch toegevoegd als de output layer als laatste layer in het netwerk
        public void AddLayer(NeuralLayer newLayer)
        {
            //any() kijkt of een element in een sequence voldoet aan een condition 
            if(_layers.Any())
            {
                var lastLayer = _layers.Last();
                newLayer.ConnectLayers(lastLayer);
            }

            _layers.Add(newLayer);
            _neuronErrors.Add(_layers.Count - 1, new double[newLayer.Neurons.Count]);
        }

        //Een methode om input waarde in het neural network te zetten
        public void PushInput(double[] inputs)
        {
            _layers.First().Neurons.ForEach(x => x.PushValueOnInput(inputs[_layers.First().Neurons.IndexOf(x)]));
        }

        //Een methode om de verwachte waardes te zetten
        public void PushExpectedValues(double[][] expectedOutputs)
        {
            _expectedResult = expectedOutputs;
        }

        //Een methode om de output van het netwerk te berekenen
        public List<double> GetOutput()
        {
            var returnValue = new List<double>();

            _layers.Last().Neurons.ForEach(neuron =>
            {
                returnValue.Add(neuron.CalculateOutput());
            });

            return returnValue;
        }

        //hier moet nog een train functie komen

        //Functie die een input layer van het neural network maakt
        private void CreateInputLayer(int numberOfInputNeurons)
        {
            var inputLayer = _layerFactory.CreateNeuralLayer(numberOfInputNeurons, new RectifiedActivationFuncion(), new WeightedSumFunction());
            inputLayer.Neurons.ForEach(x => x.AddInputSynapse(0));
            this.AddLayer(inputLayer);
        }


    }
}
