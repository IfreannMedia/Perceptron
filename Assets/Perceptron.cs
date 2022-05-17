using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainingSet
{
    public double[] input;
    public double output;
}

public class Perceptron : MonoBehaviour
{

    public TrainingSet[] set;
    double[] weights = { 0, 0 };
    double bias = 0;
    double totalError = 0;
    public int epochs = 8;

    void InitializeWeights()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(-1.0f, 1.0f);
        }
        bias = Random.Range(-1.0f, 1.0f);
    }

    private void Start()
    {
        // train perceptron
        Train(epochs);
        // test perceptron
        Debug.Log("Test 0 0: " + CalcOutput(0, 0));
        Debug.Log("Test 0 1: " + CalcOutput(0, 1));
        Debug.Log("Test 1 0: " + CalcOutput(1, 0));
        Debug.Log("Test 1 1: " + CalcOutput(1, 1));
    }


    void Train(int epochs)
    {
        InitializeWeights();

        for (int i = 0; i < epochs; i++)
        {
            totalError = 0;
            for (int j = 0; j < set.Length; j++)
            {
                UpdateWeights(j);
                Debug.Log("W1: " + (weights[0]) + " W2: " + (weights[1])+  " B: " + bias);
            }
            Debug.Log("TOTAL ERROR: " + totalError);
        }
    }

    //v1 wiehgts
    //v2 inputs
    double DotProductBias(double[] v1, double[] v2)
    {
        if((v1 == null || v2 == null) || (v1.Length != v2.Length))
        {
            return -1;
        }
        double d = 0;
        for (int i = 0; i < v1.Length; i++)
        {
            // multiply weight by input, add it to previous weight * input
            d += v1[i] * v2[i];
        }

        d += bias;

        return d;
    }
    // i is line of training set
    double CalcOutput(int i)
    {
        double dp = DotProductBias(weights, set[i].input);
        if (dp > 0) return (1);
        return (0);
    }

    // calculate given two inputs, uses the DotProduct method
    // but no udpate weights - test perceptron if it still can calculate the OR set
    double CalcOutput(double i1, double i2)
    {
        double[] inp = new double[] { i1, i2 };
        double dp = DotProductBias(weights, inp);
        if (dp > 0) return (1);
        return (0);
    }

    // line of the training set
    void UpdateWeights(int line)
    {
        double error = set[line].output - CalcOutput(line);
        totalError += Mathf.Abs((float)error);
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = weights[i] + error * set[line].input[i];
        }
        bias += error;
    }
}
