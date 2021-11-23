using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class GameManagerTests
{

    GameObject gmObject;
    GameManager gmComp;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        gmObject = new GameObject("GameManager");
        gmComp = gmObject.AddComponent<GameManager>();
        yield return null;
    }


    static KeyValuePair<IBoatFactory, Vector3>[] boatSpawnValues = new KeyValuePair<IBoatFactory, Vector3>[]
    {
        new KeyValuePair<IBoatFactory, Vector3>(new MotorBoatFactory(), new Vector3(1,1,1)),
        new KeyValuePair<IBoatFactory, Vector3>(new MotorBoatFactory(), new Vector3(100,1,100)),
        new KeyValuePair<IBoatFactory, Vector3>(new MotorBoatFactory(), new Vector3(-19,1,10)),
    };

    [UnityTest]
    public IEnumerator BoatSpawningTest([ValueSource("boatSpawnValues")] KeyValuePair<IBoatFactory, Vector3> values)
    {
        GameObject boatObject = gmComp.BuildBoat(values.Key, values.Value);
        Assert.That(boatObject, Is.Not.Null);
        Assert.AreEqual(boatObject.transform.position, values.Value);

        GameObject.Destroy(boatObject);
        yield return null;
    }

    [UnityTest]
    public IEnumerator GMStartsWithNoBoats()
    {
        Assert.AreEqual(gmComp.GetBoatCount(),0);
        yield return null;
    }


    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(gmObject);
        gmObject = null;
        yield return null;
    }

}