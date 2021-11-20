using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoatFactory
{
    Mesh CreateMesh();
    float CreateMaxSpeed();
    ICurve CreateAccelerationCurve();
    ICurve CreateTurningSpeedCurve();
    float CreateOptimalTurnSpeed();
    Material CreateMaterial();
    float CreateDetectionRange();
}
