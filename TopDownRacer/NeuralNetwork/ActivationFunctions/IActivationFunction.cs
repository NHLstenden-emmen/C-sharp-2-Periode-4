namespace TopDownRacer.NeuralNetwork.ActivationFunctions
{
    //Interface voor activation functions
    public interface IActivationFunction
    {
        double CalculateOutput(double input);
    }
}