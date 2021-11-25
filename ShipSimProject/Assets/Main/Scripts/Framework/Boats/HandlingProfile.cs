using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlingProfile
{

    public HandlingProfile(AnimationCurve profileCurve, float topSpeed)
    {
        ProfileCurve = profileCurve;
        TopSpeed = topSpeed;
    }

    public AnimationCurve ProfileCurve { get; }
    public float TopSpeed { get; }
}