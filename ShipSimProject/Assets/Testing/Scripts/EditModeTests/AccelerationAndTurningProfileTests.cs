using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationAndTurningProfileTests
{

    [Test]
    public void CanCreateAccelerationProfile()
    {
        Assert.That(new HandlingProfile(new AnimationCurve(), 0), Is.Not.Null);
    }

    [Test]
    public void HasCurveAndSpeedProperties()
    {
        AnimationCurve curve = new AnimationCurve();

        curve.AddKey(0, 128);

        HandlingProfile profile = new HandlingProfile(curve, 1337f);

        Assert.That(profile.ProfileCurve.keys[0].value, Is.EqualTo(128));
        Assert.That(profile.TopSpeed, Is.EqualTo(1337f));
    }

}
