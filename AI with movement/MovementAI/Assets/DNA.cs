using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{

    List<int> genes = new List<int>();
    int dnaLenght = 0;
    int maxValues = 0;

    public DNA(int l, int v)
    {
        dnaLenght = l;
        maxValues = v;
        setRandom();
    }

    public void setRandom()
    {
        genes.Clear();
        for(int i = 0; i < dnaLenght; i++)
        {
            genes.Add(Random.Range(0,maxValues));
        }
    }

    public void SetInt(int pos, int value)
    {
        genes[pos] = value;
    }

    public void Combine(DNA d1, DNA d2)
    {
        for (int i = 0; i < dnaLenght; i++)
        {
            if(i < dnaLenght/2.0)
            {
                int c = d1.genes[i];
                genes[i] = c;
            }
            else
            {
                int c = d2.genes[i];
                genes[i] = c;
            }
        }
    }

    public void Mutate()
    {
        genes[Random.Range(0,dnaLenght)] = Random.Range(0, maxValues);
    }

    public int GetGene(int pos)
    {
        return genes[pos];
    }
}
