using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MotorBoatTests
{
    GameManager gm;

    GameObject boatObject;
    IBoat boatInterface;

    #region set up and tear down
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        gm = new GameObject("GM").AddComponent<GameManager>();
        boatObject = gm.BuildBoat(new MotorBoatFactory(), Vector3.zero);
        boatInterface = boatObject.GetComponent<IBoat>();
        yield return null;
    
    }
    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(gm);
        GameObject.Destroy(boatObject);
        boatObject = null;
        gm = null;
        yield return null;
    }
    #endregion

    #region Speed value tests
    static KeyValuePair<float, float>[] boatSpeedValues = new KeyValuePair<float, float>[]
    {
        new KeyValuePair<float, float>(0,0),
        new KeyValuePair<float, float>(1,13.88f),
        new KeyValuePair<float, float>(30/2,416.6666f/2f)
    };

    [UnityTest]
    public IEnumerator MovementSpeedTest([ValueSource("boatSpeedValues")] KeyValuePair<float, float> values)
    {
        Assert.IsTrue(boatInterface.SetPropulsion(true));
        Assert.IsTrue(boatInterface.SetPropulsionMultiplier(1f));
        Vector3 startPos = boatObject.transform.position;
        float time = values.Key;
        float maxDist = values.Value;

        yield return new WaitForSeconds(time);
        Vector3 endPos = boatObject.transform.position;

        float dist = Vector3.Distance(startPos, endPos);
        Assert.That(dist, Is.LessThanOrEqualTo(maxDist+0.001f));

        yield return null;
    }
    #endregion

    [UnityTest]
    public IEnumerator SpeedShouldNotExceedExpectedMaximumSpeed()
    {
        boatInterface.SetPropulsion(true);
        boatInterface.SetPropulsionMultiplier(1f);
        yield return new WaitForSeconds(20);
        Assert.That(boatInterface.Speed, Is.LessThanOrEqualTo(Boat.MStoKnots(13.353f)));
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldHaveSameOptimalTurnSpeed()
    {
        float optimalTurnSpeed = boatInterface.OptimalTurnSpeed;
        float factoryTurnSpeed = new MotorBoatFactory().CreateOptimalTurnSpeed();

        Assert.AreEqual(factoryTurnSpeed, optimalTurnSpeed);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestAccelerationCurve()
    {
        boatInterface.SetPropulsion(true);
        boatInterface.SetPropulsionMultiplier(1f);


        float speedInMS = boatInterface.Speed;

        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Assert.That(boatInterface.Speed, Is.GreaterThan(speedInMS));
            speedInMS = boatInterface.Speed;
        }


        yield return null;


    }

    [UnityTest]
    public IEnumerator TheoreticalMaxSpeedShouldBe50KMH()
    {
        Assert.That(Mathf.Abs(13.353f - Boat.KnotsToMS(Boat.MStoKnots(13.353f))),Is.LessThan(0.005f));
        yield return null;
    }

    
}
