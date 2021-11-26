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
    }
}
