using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class AccelerationCurveTests
{
    [Test]
    [TestCase(0f,0.3f)]
    [TestCase(2f,0.866f)]
    [TestCase(2.991f,1.847f)]
    [TestCase(4.5f, 0.528f)]
    [TestCase(13.353f,0f)]
    public void TestMotorShipCurve(float input, float expected)
    {
        ICurve curve = new MotorShipFactory().CreateAccelerationCurve();
        UpAndDownAccelerationCurve curv = (UpAndDownAccelerationCurve)curve;
        Assert.That(Mathf.Abs(curve.F(input)/curv.AccelerationMultiplier - expected), Is.LessThan(0.005f));
    }


}
