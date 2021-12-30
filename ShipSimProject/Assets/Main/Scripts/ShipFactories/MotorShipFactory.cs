using System;
using UnityEngine;


public class MotorShipFactory : IShipFactory

{
    private static float accelerationMultiplier = 2;

    public Mesh CreateMesh()
    {
        return ResourceLoader.Load.shipMeshes.defaultShip;
    }

    public Material CreateMaterial()
    {
        return ResourceLoader.Load.materials.defaultMat;
    }

    public float CreateDetectionRange()
    {
        return ShipData.DetectionRange;
    }

    public HandlingProfile CreateHandlingProfile()
    {
        return ShipData.HandlingProfile ;
    }

    public float CreateMass()
    {
        return ShipData.Mass;
    }

    public float CreateSize()
    {
        return ShipData.Size;
    }

    private LoadableShipData ShipData => ResourceLoader.Load.shipDatas.defaultData;
}