using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class BoatInstrumentTest
{

    IBoat boatInterface;
    GameObject boat;

    # region SetUp and TearDown
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        boat = new GameObject("Boat");
        boat.AddComponent<Boat>().Build(new MotorBoatFactory());
        boatInterface = boat.GetComponent<IBoat>();
        yield return new EnterPlayMode();
    }
    [UnityTearDown]
    public IEnumerator TearDown()
    {

        GameObject.Destroy(boat);
        boat = null;
        boatInterface = null;

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (var ob in allObjects)
        {
            if (ob.GetComponent<Boat>() != null)
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
        boat.transform.rotation = targetRotation;

        float compassReading = boatInterface.CompassDirection;
        int degrees = Mathf.FloorToInt(compassReading);
        Assert.That(degrees, Is.EqualTo(val));

        yield return null;
    }
    #endregion

    [UnityTest]
    public IEnumerator ShouldHaveCompass()
    {
        Assert.That(boatInterface.CompassDirection, Is.Not.Null);

        yield return null;
    }

    #region Boat detection test
    static int[] boatDetectionTestValues = new int[]
    {
        0, 1, 2, 10, 15
    };
    [UnityTest]
    public IEnumerator BoatsInVisualFieldTest([ValueSource("boatDetectionTestValues")] int val)
    {
        for (int i = 0; i < val; i++)
        {
            GameObject gboat = new GameObject("Added boat - " + i);
            gboat.AddComponent<Boat>().Build(new MotorBoatFactory());
            IBoat iboat = gboat.GetComponent<IBoat>();
        }
        yield return new WaitForSeconds(0.5f);
        Assert.That(boatInterface.DetectedEntities.Length, Is.EqualTo(val));
        yield return null;
    }
    #endregion
}
