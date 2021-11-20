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
        Assert.That(ResourceLoader.Boats.TestBoat, Is.Not.Null);
    }

}
