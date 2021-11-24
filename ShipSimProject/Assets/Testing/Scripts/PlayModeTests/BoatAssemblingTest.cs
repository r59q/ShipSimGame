using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoatAssemblingTest
{

    public GameObject motorBoat;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        motorBoat = new GameObject("Test Boat");
        motorBoat.AddComponent<Boat>();
        IShip boatComponent = motorBoat.GetComponent<IShip>();
        boatComponent.Build(new MotorBoatFactory());
        yield return new EnterPlayMode();
    }

    [UnityTest]
    public IEnumerator HasBoatComponent()
    {
        Assert.That(motorBoat, Is.Not.Null);
        Assert.That(motorBoat.GetComponent<Boat>(),Is.Not.Null);
        yield return null;
    }

    [UnityTest]
    public IEnumerator HasMeshRendererAndMesh()
    {
        MeshRenderer mr = motorBoat.GetComponent<MeshRenderer>();
        MeshFilter mf = motorBoat.GetComponent<MeshFilter>();
        Assert.That(mr, Is.Not.Null);
        Assert.That(mf.mesh, Is.Not.Null);
        Assert.That(mf.mesh.vertexCount, Is.Not.EqualTo(0));
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldHaveMaterial()
    {
        MeshRenderer mr = motorBoat.GetComponent<MeshRenderer>();
        Assert.That(mr.materials.Length, Is.Not.EqualTo(0));
        Material mat = mr.sharedMaterial;
        Assert.That(mat.name, Is.EqualTo(new MotorBoatFactory().CreateMaterial().name + " (Instance)"));
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldHaveDetectionRange()
    {
        IShip boatInterface = motorBoat.GetComponent<IShip>();

        Assert.That(boatInterface.DetectionRange, Is.Not.EqualTo(0));

        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldHaveDetectionSphereWithRadiusOfDetectionRange()
    {
        IShip boatInterface = motorBoat.GetComponent<IShip>();

        SphereCollider collider = boatInterface.DetectionCollider;

        float detectionRange = boatInterface.DetectionRange;

        Assert.That(collider, Is.Not.Null);
        Assert.That(collider.radius, Is.EqualTo(detectionRange));

        yield return null;
    }
    [UnityTest]
    public IEnumerator DetectionSphereShouldBeTrigger()
    {
        IShip boatInterface = motorBoat.GetComponent<IShip>();

        SphereCollider collider = boatInterface.DetectionCollider;

        float detectionRange = boatInterface.DetectionRange;

        Assert.That(collider, Is.Not.Null);
        Assert.That(collider.isTrigger, Is.EqualTo(true));

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(motorBoat);
        motorBoat = null;
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