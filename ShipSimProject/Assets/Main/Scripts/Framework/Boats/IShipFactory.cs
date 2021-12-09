using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipFactory
{
    Mesh CreateMesh();
    float CreateMaxSpeed();
    ICurve CreateAccelerationCurve();
    ICurve CreateTurningSpeedCurve();
    HandlingProfile CreateHandlingProfile();
    float CreateOptimalTurnSpeed();
    Material CreateMaterial();
    float CreateDetectionRange();
    float CreateMass();
    float CreateSize();
}
