using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
public class SpeedConversionTests 
{
    [Test]
    [TestCase(1, 1 * 1.852f, 1 * 1.852f * 1000 / 60 / 60)]
    [TestCase(2, 2 * 1.852f, 2 * 1.852f * 1000 / 60 / 60)]
    [TestCase(5, 5 * 1.852f, 5 * 1.852f * 1000 / 60 / 60)]
    public void ConversionTest(float knots, float kmh, float ms)
    {
        Assert.AreEqual(kmh, Ship.KnotsToKMH(knots));
        Assert.AreEqual(ms, Ship.KnotsToMS(knots));
        Assert.AreEqual(ms, Ship.KMHtoMS(kmh));
    }

    [Test]
    [TestCase(1, 1 * 1.852f, 1 * 0.514444f)]
    [TestCase(2, 2 * 1.852f, 2 * 0.514444f)]
    [TestCase(5, 5 * 1.852f, 5 * 0.514444f)]
    [TestCase(10, 10 * 1.852f, 10 * 0.514444f)]
    [TestCase(1000, 1000 * 1.852f, 1000 * 0.514444f)]
    public void UnconversionTest(float knots, float kmh, float ms)
    {
        Assert.That(Mathf.Abs(knots - (float)System.Math.Round(Ship.KMHToKnots(kmh), 5)), Is.LessThan(0.005f));
        Assert.That(Mathf.Abs(knots - (float)System.Math.Round(Ship.MStoKnots(ms), 5)), Is.LessThan(0.005f));
        Assert.That(Mathf.Abs(kmh - (float)System.Math.Round(Ship.MStoKMH(ms), 5)), Is.LessThan(0.005f));
    }

}
