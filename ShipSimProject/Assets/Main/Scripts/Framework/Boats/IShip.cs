using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShip 
{

    public void Build(IShipFactory shipFactory);

    public bool TurnRudderTo(float rudderPos);

    public bool SetPropulsion(bool state);


    public bool SetPropulsionMultiplier(float multiplier);


    public bool StartPropulsion()
    {
        return SetPropulsion(true);
    }

    public bool StopPropulsion()
    {
        return SetPropulsion(false);
    }

    public float Speed { get; }
    public ICurve AccelerationCurve { get; }
    public ICurve TurningSpeedCurve { get; }
    public float TurningSpeed { get; } 
    public float TopTurningSpeed { get; } 
    public float OptimalTurnSpeed { get; }
    public float PropulsionMultiplier { get; }
    public float RudderPos { get; }
    public float DetectionRange { get; }
    public bool IsPropelling { get; }
    public float CompassDirection { get; }
    public Rigidbody Rigidbody { get; }
    public IDetectableEntity[] DetectedEntities { get; }
    public HandlingProfile HandlingProfile { get; }
    public float Mass { get; }
    public float TopSpeed { get; }
    public float Size { get; }
    public SphereCollider DetectionCollider { get; }

    public SphereCollider Collider { get; }
}
