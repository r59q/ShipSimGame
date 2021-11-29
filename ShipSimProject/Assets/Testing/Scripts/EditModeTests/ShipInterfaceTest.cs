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



    class MockShip : IShip
    {
        public HandlingProfile HandlingProfile => new HandlingProfile(null, null, 0, 0);
        public float Mass => 0;

        #region Not implemented
        public float Speed => throw new System.NotImplementedException();

        public ICurve AccelerationCurve => throw new System.NotImplementedException();

        public ICurve TurningSpeedCurve => throw new System.NotImplementedException();

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
