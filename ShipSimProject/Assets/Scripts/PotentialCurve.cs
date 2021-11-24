using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentialCurve : ICurve
{
    public PotentialCurve(float a, float b, float o)
    {
        A = a;
        B = b;
        O = o;
    }

    public float A { get; }
    public float B { get; }
    public float O { get; }

    public float F(float x)
    {
        return (B * Mathf.Pow(x, A)) + O;
    }
}
