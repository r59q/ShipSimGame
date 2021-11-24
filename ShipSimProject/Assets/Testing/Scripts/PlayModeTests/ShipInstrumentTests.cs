using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class ShipInstrumentTests
{

    IShip shipInterface;
    GameObject ship;

    # region SetUp and TearDown
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        ship = new GameObject("Ship");
        ship.AddComponent<Ship>().Build(new MotorShipFactory());
        shipInterface = ship.GetComponent<IShip>();
        yield return new EnterPlayMode();
    }
    [UnityTearDown]
    public IEnumerator TearDown()
    {

        GameObject.Destroy(ship);
        ship = null;
        shipInterface = null;

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (var ob in allObjects)
        {
            if (ob.GetComponent<Ship>() != null)
            {
                GameObject.Destroy(ob);
            }
        }
        yield return new ExitPlayMode();
    }
    #endregion 

    #region Compass value tests
    static int[] boatCompassValues = new int[]
    {
        0, 90, 180, 270
    };
    [UnityTest]
    public IEnumerator CompassValueTests([ValueSource("boatCompassValues")] int val)
    {
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, val, 0));
        ship.transform.rotation = targetRotation;

        float compassReading = shipInterface.CompassDirection;
        int degrees = Mathf.FloorToInt(compassReading);
        Assert.That(degrees, Is.EqualTo(val));

        yield return null;
    }
    #endregion

    [UnityTest]
    public IEnumerator ShouldHaveCompass()
    {
        Assert.That(shipInterface.CompassDirection, Is.Not.Null);

        yield return null;
    }

    #region Ship detection test
    static int[] shipDetectionTestValues = new int[]
    {
        0, 1, 2, 10, 15
    };
    [UnityTest]
    public IEnumerator BoatsInVisualFieldTest([ValueSource("shipDetectionTestValues")] int val)
    {
        for (int i = 0; i < val; i++)
        {
            GameObject gShip = new GameObject("Added Ship - " + i);
            gShip.AddComponent<Ship>().Build(new MotorShipFactory());
        }
        yield return new WaitForSeconds(0.5f);
        Assert.That(shipInterface.DetectedEntities.Length, Is.EqualTo(val));
        yield return null;
    }
    #endregion
}
