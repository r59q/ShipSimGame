using System;
using UnityEngine;


public class MotorBoatFactory : IShipFactory
{
    private static float accelerationMultiplier = 2;

    public float CreateMaxSpeed()
    {
        return Boat.MStoKnots(13.353f);
    }

    public Mesh CreateMesh()
    {
        return ResourceLoader.Load.boatMeshes.testBoat;
    }

    public ICurve CreateAccelerationCurve()
    {
        return new UpAndDownAccelerationCurve(
            x => 0.1f * Mathf.Pow(x , 2.5f) + 0.3f, 
            x => 50 * Mathf.Pow(x , -3f)- 0.021f,
            accelerationMultiplier
        );
    }

    public ICurve CreateTurningSpeedCurve()
    {
        return new PotentialCurve(0.7f, 6, 0);
    }

    public float CreateOptimalTurnSpeed()
    {
        return 0.3f;
    }

    public Material CreateMaterial()
    {
        return ResourceLoader.Load.materials.testMat;
    }

    public float CreateDetectionRange()
    {
        return 1000f;
    }
}