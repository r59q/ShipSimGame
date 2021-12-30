using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorShipTests
{

    IShipFactory motorShipFactory;

    IShip motorShip;

    [SetUp]
    public void SetUp()
    {
        GameObject motorShipObject = new GameObject();
        motorShip = motorShipObject.AddComponent<Ship>();
        motorShipFactory = new MotorShipFactory();
        motorShip.Build(motorShipFactory);
    }

    [Test]
    public void HasSizeSetByFactory()
    {
        Assert.That(motorShip.Size, Is.EqualTo(motorShipFactory.CreateSize()));

    }

}
