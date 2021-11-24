using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ShipAssemblingTest
{

    public GameObject motorShip;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        motorShip = new GameObject("Test Ship");
        motorShip.AddComponent<Ship>();
        IShip shipComponent = motorShip.GetComponent<IShip>();
        shipComponent.Build(new MotorShipFactory());
        yield return new EnterPlayMode();
    }

    [UnityTest]
    public IEnumerator HasShipComponent()
    {
        Assert.That(motorShip, Is.Not.Null);
        Assert.That(motorShip.GetComponent<Ship>(),Is.Not.Null);
        yield return null;
    }

    [UnityTest]
    public IEnumerator HasMeshRendererAndMesh()
    {
        MeshRenderer mr = motorShip.GetComponent<MeshRenderer>();
        MeshFilter mf = motorShip.GetComponent<MeshFilter>();
        Assert.That(mr, Is.Not.Null);
        Assert.That(mf.mesh, Is.Not.Null);
        Assert.That(mf.mesh.vertexCount, Is.Not.EqualTo(0));
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldHaveMaterial()
    {
        MeshRenderer mr = motorShip.GetComponent<MeshRenderer>();
        Assert.That(mr.materials.Length, Is.Not.EqualTo(0));
        Material mat = mr.sharedMaterial;
        Assert.That(mat.name, Is.EqualTo(new MotorShipFactory().CreateMaterial().name + " (Instance)"));
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldHaveDetectionRange()
    {
        IShip shipInterface = motorShip.GetComponent<IShip>();

        Assert.That(shipInterface.DetectionRange, Is.Not.EqualTo(0));

        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldHaveDetectionSphereWithRadiusOfDetectionRange()
    {
        IShip shipInterface = motorShip.GetComponent<IShip>();

        SphereCollider collider = shipInterface.DetectionCollider;

        float detectionRange = shipInterface.DetectionRange;

        Assert.That(collider, Is.Not.Null);
        Assert.That(collider.radius, Is.EqualTo(detectionRange));

        yield return null;
    }
    [UnityTest]
    public IEnumerator DetectionSphereShouldBeTrigger()
    {
        IShip shipInterface = motorShip.GetComponent<IShip>();

        SphereCollider collider = shipInterface.DetectionCollider;

        float detectionRange = shipInterface.DetectionRange;

        Assert.That(collider, Is.Not.Null);
        Assert.That(collider.isTrigger, Is.EqualTo(true));

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(motorShip);
        motorShip = null;
        yield return new ExitPlayMode();
    }
}




















/*
// A Test behaves as an ordinary method
[Test]
public void BoatAssemblingTestSimplePasses()
{
    // Use the Assert class to test conditions
}

// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
// `yield return null;` to skip a frame.
[UnityTest]
public IEnumerator BoatAssemblingTestWithEnumeratorPasses()
{
    // Use the Assert class to test conditions.
    // Use yield to skip a frame.
    yield return null;
}
*/