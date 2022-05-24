using System;
using System.Linq;
using System.Collections.Generic;

namespace NeuralNetworkCSharp.Neuron
{
    //Implementatie van een Neuron
    public class Neuron : INeuron
    {
        private IActivationFunction _activationFunction;
        private IInputFunction _inputFunction;

        //Input connecties van de neuron
        public List<ISynapse> Inputs { get; set; }

        //Output connecties van de neuron
        public List<ISynapse> Outputs { get; set; }

        public Guid Id { get; private set; }

        //Berekend "partial derivate" van de vorige iteratie van de training
        public double PreviousPartialDerivate { get; set; }

        public Neuron(IActivationFunction activationFunction, IInputFunction inputFunction)
        {
            Id = Guid.NewGuid();
            Inputs = new List<ISynapse>();
            Outputs = new List<ISynapse>();

            _activationFunction = activationFunction;
            _inputFunction = inputFunction;
        }

        //Verbind van de neurons Connect two neurons. 
        //Input neuron zal werken als de input van de nieuwe connectie
        public void AddInputNeuron(INeuron inputNeuron)
        {
            var synapse = new Synapse(inputNeuron, this);
            Inputs.Add(synapse);
            inputNeuron.Outputs.Add(synapse);
        }

        //Verbind van de neurons Connect two neurons. 
        //Output neuron die de output van die nieuwe connectie zal weergeven
        public void AddOutputNeuron(INeuron outputNeuron)
        {
            var synapse = new Synapse(this, outputNeuron);
            Outputs.Add(synapse);
            outputNeuron.Inputs.Add(synapse);
        }

        //Output van de neuron berekenen 
        //returned de output neuron 
        public double CalculateOutput()
        {
            return _activationFunction.CalculateOutput(_inputFunction.CalculateInput(this.Inputs));
        }

        //Input laag van neuronen die alleen inputs ontvangt
        //Eerste waarde die als eerste input dient
        public void AddInputSynapse(double inputValue)
        {
            var inputSynapse = new InputSynapse(this, inputValue);
            Inputs.Add(inputSynapse);
        }

        //Set een nieuwe waarde voor de input
        //Input is de nieuwe waarde die als input wordt gepushed
        public void PushValueOnInput(double inputValue)
        {
            ((InputSynapse)Inputs.First()).Output = inputValue;
        }
    }
}
