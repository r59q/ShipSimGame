using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownAccelerationCurve : ICurve
{
    public UpAndDownAccelerationCurve(Func<float, float> downCurve, Func<float, float> upCurve, float accelerationMultiplier)
    {
        DownCurve = downCurve;
        UpCurve = upCurve;
        AccelerationMultiplier = accelerationMultiplier;
    }

    public Func<float, float> UpCurve { get ; private set ; }
    public Func<float, float> DownCurve { get; private set; }
    public float AccelerationMultiplier { get; private set; }

    public float F(float x)
    {
        return Mathf.Min(UpCurve(x), DownCurve(x)) * AccelerationMultiplier;
    }

}