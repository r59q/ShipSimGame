using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship data", menuName = "Loadable Ship Data")]
public class LoadableShipData : ScriptableObject
{
    [SerializeField]
    private float topSpeed, turningSpeed, mass, size;

    [SerializeField]
    private AnimationCurve accelerationCurve, turningCurve;

    public float TopSpeed => topSpeed;

    public float TurningSpeed => turningSpeed;

    public float Size => size;

    public float Mass => mass;

    public AnimationCurve AccelerationCurve => accelerationCurve; 

    public AnimationCurve TurningCurve => turningCurve;

    public HandlingProfile HandlingProfile { get { return new HandlingProfile(AccelerationCurve, TurningCurve, TopSpeed, TurningSpeed); } }

}
