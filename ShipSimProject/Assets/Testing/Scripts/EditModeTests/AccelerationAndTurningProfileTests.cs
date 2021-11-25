using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationAndTurningProfileTests
{
    private AnimationCurve accelerationCurve, turningCurve;
    private float topSpeed, turningSpeed;

    private HandlingProfile handlingProfile;

    [SetUp]
    public void SetUp()
    {
        accelerationCurve = new AnimationCurve();
        turningCurve = new AnimationCurve();
        topSpeed = 321;
        turningSpeed = 123;
        handlingProfile = new HandlingProfile(accelerationCurve, turningCurve, topSpeed, turningSpeed);
    }

    [TearDown]
    public void TearDown()
    {
        // nothing to do.
    }

    [Test]
    public void CanCreateAccelerationProfile()
    {
        Assert.That(new HandlingProfile(new AnimationCurve(), new AnimationCurve(), 0, 0), Is.Not.Null);
    }

    [Test]
    public void HasProperties()
    {
        AnimationCurve turnCurve = new AnimationCurve();
        AnimationCurve accelerationCurve = new AnimationCurve();

        accelerationCurve.AddKey(0, 128);
        turnCurve.AddKey(0, 821);

        HandlingProfile profile = new HandlingProfile(accelerationCurve, turnCurve, 7331f, 1337f);

        Assert.That(profile.AccelerationCurve.keys[0].value, Is.EqualTo(128));
        Assert.That(profile.TurningCurve.keys[0].value, Is.EqualTo(821));
        Assert.That(profile.TopSpeed, Is.EqualTo(7331f));
        Assert.That(profile.TurningSpeed, Is.EqualTo(1337f));
    }

    [Test]
    public void ScalesBySpeedWithCoeffecient1()
    {
        handlingProfile.AccelerationCurve.AddKey(0, 1);
        handlingProfile.AccelerationCurve.AddKey(1, 2);

        for (int i = 6; i < topSpeed; i++)
        {
            float evaluationAtStart = handlingProfile.GetAccelerationAt(i-5);
            float evaluationAtEnd = handlingProfile.GetAccelerationAt(i);

            float coeffecient = (evaluationAtEnd - evaluationAtStart) / (float)(i-(i-5));

            Assert.That(Mathf.Abs(coeffecient - 1f), Is.LessThan(0.001)); // floating point calculations are difficult to test
        }
    }

    [Test]
    public void ScalesTurningBySpeed()
    {
        handlingProfile.TurningCurve.AddKey(0, 0);
        handlingProfile.TurningCurve.AddKey(1, 1);
        float turnSpeed = handlingProfile.GetTurningAt(topSpeed);
        Assert.That(turnSpeed, Is.EqualTo(turningSpeed));
    }

    [Test]
    public void ScalesTurningBySpeedWhenTurningBestAtMidSpeed()
    {
        handlingProfile.TurningCurve.AddKey(0, 0);
        handlingProfile.TurningCurve.AddKey(0.5f, 1);
        handlingProfile.TurningCurve.AddKey(1, 0);
        float turnSpeed = handlingProfile.GetTurningAt(topSpeed);
        Assert.That(turnSpeed, Is.EqualTo(0));

        turnSpeed = handlingProfile.GetTurningAt(topSpeed * 0.5f);
        Assert.That(turnSpeed, Is.EqualTo(turningSpeed));
    }

}
