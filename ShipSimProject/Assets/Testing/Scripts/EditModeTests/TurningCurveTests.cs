using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class TurningCurveTests
{
    [Test]
    [TestCase(0,0)]
    [TestCase(0.5f, 16.4481041f)]
    [TestCase(2,19.69728f)]
    [TestCase(20, 26.570908f)]
    public void MotorBoatTurningSpeedTest(float input, float expected)
    {
        ICurve curve = new MotorShipFactory().CreateTurningSpeedCurve();

        float val = curve.F(input);
        float result = Mathf.Abs(expected - val);
        Assert.That(result, Is.LessThan(0.005f));
    }
}
