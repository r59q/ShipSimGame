using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipFactory
{
    Mesh CreateMesh();
    HandlingProfile CreateHandlingProfile();
    Material CreateMaterial();
    float CreateDetectionRange();
    float CreateMass();
    float CreateSize();
}
