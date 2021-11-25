using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandlingProfile
{
    public HandlingProfile(AnimationCurve accelerationCurve, AnimationCurve turningCurve, float topSpeed, float turningSpeed)
    {
        AccelerationCurve = accelerationCurve;
        TurningCurve = turningCurve;
        TopSpeed = topSpeed;
        TurningSpeed = turningSpeed;
    }

    public AnimationCurve AccelerationCurve { get; }
    public AnimationCurve TurningCurve { get; }
    public float TopSpeed { get; }
    public float TurningSpeed { get; }

    public float GetAccelerationAt(float speed)
    {
        return AccelerationCurve.Evaluate(speed / TopSpeed) * TopSpeed;
    }

    public float GetTurningAt(float speed)
    {
        return TurningCurve.Evaluate(speed / TopSpeed) * TurningSpeed;
    }
}