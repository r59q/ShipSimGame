using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDataTest
{
    LoadableShipData loadableShipData;

    [SetUp]
    public void SetUp()
    {
        loadableShipData = ScriptableObject.CreateInstance<LoadableShipData>();
    }
    [Test]
    public void HasProperties()
    {
        Assert.That(loadableShipData.TopSpeed, Is.Not.Null);
        Assert.That(loadableShipData.TurningSpeed, Is.Not.Null);
        Assert.That(loadableShipData.AccelerationCurve, Is.Null);
        Assert.That(loadableShipData.TurningCurve, Is.Null);

        Assert.That(loadableShipData.Mass, Is.Not.Null);
        Assert.That(loadableShipData.Size, Is.Not.Null);

        HandlingProfile profile = loadableShipData.HandlingProfile;
        Assert.That(profile, Is.Not.Null);
    }

    [Test]
    public void CheckTestValues()
    {
        loadableShipData = TestGM.LoadFromResources().testData;
        Assert.That(loadableShipData.TopSpeed, Is.EqualTo(5));
        Assert.That(loadableShipData.TurningSpeed, Is.EqualTo(6));
        Assert.That(loadableShipData.AccelerationCurve.keys.Length, Is.EqualTo(3));
        Assert.That(loadableShipData.TurningCurve.keys.Length, Is.EqualTo(2));

        Assert.That(loadableShipData.Mass, Is.EqualTo(10));
        Assert.That(loadableShipData.Size, Is.EqualTo(10));

        HandlingProfile profile = loadableShipData.HandlingProfile;
        Assert.That(profile.AccelerationCurve, Is.EqualTo(loadableShipData.AccelerationCurve));
    }

    [Test]
    public void HasDefaultData()
    {
        Assert.That(ResourceLoader.Load.shipDatas.defaultData,Is.Not.Null);
    }
}
