using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ResourceLoadingTests
{

    [Test]
    public void ResourceLoadingAccessibleByStatic()
    {
        Assert.That(ResourceLoader.Load.shipMeshes.testShip, Is.Not.Null);
    }

    [Test]
    public void HasTestMaterial()
    {
        Assert.That(ResourceLoader.Load.materials.testMat, Is.Not.Null);
    }

    [Test]
    public void ResourceObjectHasTestShipAttribute()
    {
        LoadableAssets assets = ScriptableObject.CreateInstance<LoadableAssets>();
        Assert.That(assets, Is.Not.Null);
        Assert.That(assets.shipMeshes.testShip, Is.Null);
    }

}
