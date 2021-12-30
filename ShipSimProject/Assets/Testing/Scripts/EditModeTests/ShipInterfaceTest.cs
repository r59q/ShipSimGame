using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInterfaceTest : MonoBehaviour
{
    IShip mockShip = new MockShip();

    [Test]
    public void HasHandlingProfile()
    {
        HandlingProfile handlingProfile = mockShip.HandlingProfile;
        Assert.That(handlingProfile, Is.Not.Null);
    }
    [Test]
    public void HasMass()
    {
        float mass = mockShip.Mass;
        Assert.That(mass, Is.Not.Null);
    }

    [Test]
    public void MotorShipHasProfile()
    {
        GameObject motorShipObject = new GameObject();
        IShip motorShip = motorShipObject.AddComponent<Ship>();
        motorShip.Build(new MotorShipFactory());

        Assert.That(motorShip.HandlingProfile, Is.Not.Null);
        Assert.That(motorShip.HandlingProfile.AccelerationCurve.keys.Length, Is.Not.EqualTo(0));
        Assert.That(motorShip.HandlingProfile.TurningCurve.keys.Length, Is.Not.EqualTo(0));
    }

    [Test]
    public void MotorShipHasMass()
    {
        GameObject motorShipObject = new GameObject();
        IShip motorShip = motorShipObject.AddComponent<Ship>();
        motorShip.Build(new MotorShipFactory());

        Assert.That(motorShip.Mass, Is.Not.EqualTo(0));
    }


    [Test]
    public void MotorShipHasDetectionRange()
    {
        GameObject motorShipObject = new GameObject();
        IShip motorShip = motorShipObject.AddComponent<Ship>();
        motorShip.Build(new MotorShipFactory());

        Assert.That(motorShip.DetectionRange, Is.Not.EqualTo(0));
    }



    [Test]
    public void MotorShipHasTopSpeed()
    {
        GameObject motorShipObject = new GameObject();
        IShip motorShip = motorShipObject.AddComponent<Ship>();
        motorShip.Build(new MotorShipFactory());

        Assert.That(motorShip.HandlingProfile.TopSpeed, Is.Not.EqualTo(0));
    }

    [Test]
    public void MotorShipHasTurnSpeed()
    {
        GameObject motorShipObject = new GameObject();
        IShip motorShip = motorShipObject.AddComponent<Ship>();
        motorShip.Build(new MotorShipFactory());

        Assert.That(motorShip.HandlingProfile.TurningSpeed, Is.Not.EqualTo(0));
    }
    

    [Test]
    public void ProfileHasCorrectValueSetByScriptableObject()
    {
        GameObject testShipObject = new GameObject();
        IShip testShip = testShipObject.AddComponent<Ship>();
        testShip.Build(new ShipFactoryStub());

        Assert.That(testShip.Mass, Is.EqualTo(10f));
        Assert.That(testShip.TopTurningSpeed, Is.EqualTo(6));
        Assert.That(testShip.TopSpeed, Is.EqualTo(5));
        Assert.That(testShip.DetectionRange, Is.EqualTo(999f));
        Assert.That(testShip.Size, Is.EqualTo(10f)); // Can't be resolved until collider is created. Check #5 on github -> Ship size
    }

    [Test]
    public void ShipHasColliderWithSizeSetByShipData()
    {
        GameObject testShipObject = new GameObject();
        IShip testShip = testShipObject.AddComponent<Ship>();
        testShip.Build(new ShipFactoryStub());

        Assert.That(testShipObject.GetComponents<Collider>().Length, Is.EqualTo(2));

        Assert.That(testShip.Collider, Is.Not.Null);
    }



    class MockShip : IShip
    {
        public HandlingProfile HandlingProfile => new HandlingProfile(TestGM.LoadFromResources().testData.AccelerationCurve, TestGM.LoadFromResources().testData.TurningCurve, TestGM.LoadFromResources().testData.TopSpeed, TestGM.LoadFromResources().testData.TurningSpeed);
        public float Mass => TestGM.LoadFromResources().testData.Mass;

        #region Not implemented
        public float Speed => throw new System.NotImplementedException();

        public float TurningSpeed => throw new System.NotImplementedException();

        public float OptimalTurnSpeed => throw new System.NotImplementedException();

        public float PropulsionMultiplier => throw new System.NotImplementedException();

        public float RudderPos => throw new System.NotImplementedException();

        public float DetectionRange => throw new System.NotImplementedException();

        public bool IsPropelling => throw new System.NotImplementedException();

        public float CompassDirection => throw new System.NotImplementedException();

        public Rigidbody Rigidbody => throw new System.NotImplementedException();

        public SphereCollider DetectionCollider => throw new System.NotImplementedException();

        public IDetectableEntity[] DetectedEntities => throw new System.NotImplementedException();

        public float TopSpeed => throw new System.NotImplementedException();

        public float Size => throw new System.NotImplementedException();

        public float TopTurningSpeed => throw new System.NotImplementedException();

        public SphereCollider Collider => throw new System.NotImplementedException();

        public void Build(IShipFactory shipFactory)
        {
            throw new System.NotImplementedException();
        }

        public bool SetPropulsion(bool state)
        {
            throw new System.NotImplementedException();
        }

        public bool SetPropulsionMultiplier(float multiplier)
        {
            throw new System.NotImplementedException();
        }

        public bool TurnRudderTo(float rudderPos)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
