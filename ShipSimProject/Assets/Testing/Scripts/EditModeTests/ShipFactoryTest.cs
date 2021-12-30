using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactoryTest 
{

    // create mock factory
    IShipFactory mockFactory = new MockFactory();

    [Test]
    public void HasHandlingProfile()
    {
        Assert.That(mockFactory.CreateHandlingProfile(), Is.Not.Null);
        Assert.That(mockFactory.CreateHandlingProfile().AccelerationCurve.keys.Length, Is.Not.EqualTo(0));
        Assert.That(mockFactory.CreateHandlingProfile().TurningCurve.keys.Length, Is.Not.EqualTo(0));
    }

    [Test]
    public void HasMassProperty()
    {
        Assert.That(mockFactory.CreateMass(),Is.Not.Null);
    }

    [Test]
    public void HasSizeProperty()
    {
        Assert.That(mockFactory.CreateSize(),Is.Not.Null);
    }

    [Test]
    public void MotorShipFactoryHasProfile()
    {
        IShipFactory motorShipFactory = new MotorShipFactory();

        Assert.That(motorShipFactory.CreateHandlingProfile(), Is.Not.Null);
    }

    [Test]
    public void MotorShipFactoryCanAccelerateAtStandstill()
    {
        IShipFactory motorShipFactory = new MotorShipFactory();

        Assert.That(motorShipFactory.CreateHandlingProfile().GetAccelerationAt(0), Is.GreaterThan(0));
    }


    class MockFactory : IShipFactory
    {
        public HandlingProfile CreateHandlingProfile()
        {
            return ResourceLoader.Load.shipDatas.defaultData.HandlingProfile;
        }

        public float CreateMass()
        {
            return 0;
        }
        #region Not implemented

        public float CreateDetectionRange()
        {
            throw new System.NotImplementedException();
        }


        public Material CreateMaterial()
        {
            throw new System.NotImplementedException();
        }

        public Mesh CreateMesh()
        {
            throw new System.NotImplementedException();
        }

        public float CreateOptimalTurnSpeed()
        {
            throw new System.NotImplementedException();
        }

        public float CreateSize()
        {
            return 99;
        }

        #endregion

    }

}
