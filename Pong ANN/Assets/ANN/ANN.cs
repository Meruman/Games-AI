using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activation
{
    public int ActivationType;
    public int ActivationTypeO;
    public Activation()
    {
        ActivationType = 2;
        ActivationTypeO = 2;
    }

}
public class ANN{

	public int numInputs;
	public int numOutputs;
	public int numHidden;
	public int numNPerHidden;
	public double alpha;
	List<Layer> layers = new List<Layer>();

	public Activation activation = new Activation();

	public ANN(int nI, int nO, int nH, int nPH, double a)
	{
		numInputs = nI;
		numOutputs = nO;
		numHidden = nH;
		numNPerHidden = nPH;
		alpha = a;

		if(numHidden > 0)
		{
			layers.Add(new Layer(numNPerHidden, numInputs));

			for(int i = 0; i < numHidden-1; i++)
			{
				layers.Add(new Layer(numNPerHidden, numNPerHidden));
			}

			layers.Add(new Layer(numOutputs, numNPerHidden));
		}
		else
		{
			layers.Add(new Layer(numOutputs, numInputs));
		}
	}

	public List<double> Train(List<double> inputValues, List<double> desiredOutput)
	{
		List<double> outputValues = new List<double>();
		outputValues = CalcOutput(inputValues, desiredOutput);
		UpdateWeights(outputValues, desiredOutput);
		return outputValues;
	}

	public List<double> CalcOutput(List<double> inputValues, List<double> desiredOutput)
	{
		List<double> inputs = new List<double>();
		List<double> outputValues = new List<double>();
		int currentInput = 0;

		if(inputValues.Count != numInputs)
		{
			Debug.Log("ERROR: Number of Inputs must be " + numInputs);
			return outputValues;
		}

		inputs = new List<double>(inputValues);
		for(int i = 0; i < numHidden + 1; i++)
		{
				if(i > 0)
				{
					inputs = new List<double>(outputValues);
				}
				outputValues.Clear();

				for(int j = 0; j < layers[i].numNeurons; j++)
				{
					double N = 0;
					layers[i].neurons[j].inputs.Clear();

					for(int k = 0; k < layers[i].neurons[j].numInputs; k++)
					{
					    layers[i].neurons[j].inputs.Add(inputs[currentInput]);
						N += layers[i].neurons[j].weights[k] * inputs[currentInput];
						currentInput++;
					}

					N -= layers[i].neurons[j].bias;

					if(i == numHidden)
						layers[i].neurons[j].output = ActivationFunctionO(N);
					else
						layers[i].neurons[j].output = ActivationFunction(N);
					
					outputValues.Add(layers[i].neurons[j].output);
					currentInput = 0;
				}
		}
		return outputValues;
	}

	public string PrintWeights()
	{
		string weightStr = "";
		foreach(Layer l in layers)
		{
			foreach(Neuron n in l.neurons)
			{
				foreach(double w in n.weights)
				{
					weightStr += w + ",";
				}
			}
		}
		return weightStr;
	}

	public void LoadWeights(string weightStr)
	{
		if(weightStr == "") return;
		string[] weightValues = weightStr.Split(',');
		int w = 0;
		foreach(Layer l in layers)
		{
			foreach(Neuron n in l.neurons)
			{
				for(int i = 0; i < n.weights.Count; i++)
				{
					n.weights[i] = System.Convert.ToDouble(weightValues[w]);
					w++;
				}
			}
		}
	}
	
	void UpdateWeights(List<double> outputs, List<double> desiredOutput)
	{
		double error;
		for(int i = numHidden; i >= 0; i--)
		{
			for(int j = 0; j < layers[i].numNeurons; j++)
			{
				if(i == numHidden)
				{
					error = desiredOutput[j] - outputs[j];
					layers[i].neurons[j].errorGradient = GradientFunctionO(outputs[j], error);
				}
				else
				{
					layers[i].neurons[j].errorGradient = GradientFunction(i,j);
					double errorGradSum = 0;
					for(int p = 0; p < layers[i+1].numNeurons; p++)
					{
						errorGradSum += layers[i+1].neurons[p].errorGradient * layers[i+1].neurons[p].weights[j];
					}
					layers[i].neurons[j].errorGradient *= errorGradSum;
				}	
				for(int k = 0; k < layers[i].neurons[j].numInputs; k++)
				{
					if(i == numHidden)
					{
						error = desiredOutput[j] - outputs[j];
						layers[i].neurons[j].weights[k] += alpha * layers[i].neurons[j].inputs[k] * error;
					}
					else
					{
						layers[i].neurons[j].weights[k] += alpha * layers[i].neurons[j].inputs[k] * layers[i].neurons[j].errorGradient;
					}
				}
				layers[i].neurons[j].bias += alpha * -1 * layers[i].neurons[j].errorGradient;
			}

		}

	}

	double ActivationFunction(double value)
	{
		//1: Step, 2: Sigmoid, 3: TanH, 4: ReLU, 5: LeakyReLu
        activation.ActivationType = 2;
		return TanH(value);
	}

	double ActivationFunctionO(double value)
	{
		//1: Step, 2: Sigmoid, 3: TanH, 4: ReLU, 5: LeakyReLu
        activation.ActivationTypeO = 2;
		return TanH(value);
	}

	double TanH(double value)
	{
		double k = (double) System.Math.Exp(-2*value);
    	return 2 / (1.0f + k) - 1;
	}

	double Sigmoid(double value) 
	{
    	double k = (double) System.Math.Exp(value);
    	return k / (1.0f + k);
	}

	double GradientFunction(int i, int j)
    {
        double result = 0;
        switch(activation.ActivationType)
        {
            case 1:
                result = layers[i].neurons[j].output * (1-layers[i].neurons[j].output);
                break;
            case 2:
                result = layers[i].neurons[j].output * (1-layers[i].neurons[j].output);
                break;
            case 3:
                result = 1 - System.Math.Pow(System.Math.Tanh(layers[i].neurons[j].output),2);
                break;
            case 4:
                if(layers[i].neurons[j].output > 0) //Not differentiable by zero
                    result = 1;
                else
                    result = 0;
                break;
            case 5:
                if(layers[i].neurons[j].output >=0)
                {
                    result = 1;
                }
                else
                {
                    result = 0.01;
                }
                break;
        }
        return result;
    }

    double GradientFunctionO(double outputs, double error)
    {
        double result = 0;
        switch(activation.ActivationTypeO)
        {
            case 1:
                result = outputs * (1-outputs) * error;
                break;
            case 2:
                result = outputs * (1-outputs) * error;
                break;
            case 3:
                result = 1 - System.Math.Pow(System.Math.Tanh(outputs),2) * error;
                break;
            case 4:
                if(outputs > 0) //Not differentiable by zero
                    result = error;
                else
                    result = 0;
                break;
            case 5:
                if(outputs >=0)
                {
                    result = error;
                }
                else
                {
                    result = 0.01 * error;
                }
                break;
        }
        return result;
    }
}
