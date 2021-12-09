using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactoryStub : IShipFactory
{
    public ICurve CreateAccelerationCurve()
    {
        return new PotentialCurve(0,0,0);
    }

    public float CreateDetectionRange()
    {
        return TestGM.LoadFromResources().testData.DetectionRange;
    }

    public HandlingProfile CreateHandlingProfile()
    {
        return new HandlingProfile(
            TestGM.LoadFromResources().testData.AccelerationCurve,
            TestGM.LoadFromResources().testData.TurningCurve,
            TestGM.LoadFromResources().testData.TopSpeed,
            TestGM.LoadFromResources().testData.TurningSpeed
            );
    }

    public float CreateMass()
    {
        return TestGM.LoadFromResources().testData.Mass;
    }

    public Material CreateMaterial()
    {
        return ResourceLoader.Load.materials.defaultMat;
    }

    public float CreateMaxSpeed()
    {
        return CreateHandlingProfile().TopSpeed;
    }

    public Mesh CreateMesh()
    {
        return ResourceLoader.Load.shipMeshes.defaultShip;
    }

    public float CreateOptimalTurnSpeed()
    {
        return -1;
    }

    public float CreateSize()
    {
        return TestGM.LoadFromResources().testData.Size;
    }

    public ICurve CreateTurningSpeedCurve()
    {
        return new PotentialCurve(0, 0, 0);
    }
}
