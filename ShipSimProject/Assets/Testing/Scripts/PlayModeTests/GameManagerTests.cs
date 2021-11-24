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


    static KeyValuePair<IShipFactory, Vector3>[] shipSpawnValues = new KeyValuePair<IShipFactory, Vector3>[]
    {
        new KeyValuePair<IShipFactory, Vector3>(new MotorShipFactory(), new Vector3(1,1,1)),
        new KeyValuePair<IShipFactory, Vector3>(new MotorShipFactory(), new Vector3(100,1,100)),
        new KeyValuePair<IShipFactory, Vector3>(new MotorShipFactory(), new Vector3(-19,1,10)),
    };

    [UnityTest]
    public IEnumerator ShipSpawningTest([ValueSource("shipSpawnValues")] KeyValuePair<IShipFactory, Vector3> values)
    {
        GameObject shipObject = gmComp.BuildShip(values.Key, values.Value);
        Assert.That(shipObject, Is.Not.Null);
        Assert.AreEqual(shipObject.transform.position, values.Value);

        GameObject.Destroy(shipObject);
        yield return null;
    }

    [UnityTest]
    public IEnumerator GMStartsWithNoBoats()
    {
        Assert.AreEqual(gmComp.GetShipCount(),0);
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
