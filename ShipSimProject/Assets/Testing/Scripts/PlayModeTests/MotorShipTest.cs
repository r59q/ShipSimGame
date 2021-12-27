using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MotorShipTest
{
    GameManager gm;

    GameObject shipObject;
    IShip shipInterface;

    #region set up and tear down
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        gm = new GameObject("GM").AddComponent<GameManager>();
        shipObject = gm.BuildShip(new MotorShipFactory(), Vector3.zero);
        shipInterface = shipObject.GetComponent<IShip>();
        yield return null;
    
    }
    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(gm);
        GameObject.Destroy(shipObject);
        shipObject = null;
        gm = null;
        yield return null;
    }
    #endregion

    #region Speed value tests
    static KeyValuePair<float, float>[] shipSpeedValues = new KeyValuePair<float, float>[]
    {
        new KeyValuePair<float, float>(0,0),
        new KeyValuePair<float, float>(1,13.88f),
        new KeyValuePair<float, float>(30/2,416.6666f/2f)
    };

    [UnityTest]
    public IEnumerator MovementSpeedTest([ValueSource("shipSpeedValues")] KeyValuePair<float, float> values)
    {
        Assert.IsTrue(shipInterface.SetPropulsion(true));
        Assert.IsTrue(shipInterface.SetPropulsionMultiplier(1f));
        Vector3 startPos = shipObject.transform.position;
        float time = values.Key;
        float maxDist = values.Value;

        yield return new WaitForSeconds(time);
        Vector3 endPos = shipObject.transform.position;

        float dist = Vector3.Distance(startPos, endPos);
        Assert.That(dist, Is.LessThanOrEqualTo(maxDist+0.001f));

        yield return null;
    }
    #endregion

    [UnityTest]
    public IEnumerator SpeedShouldNotExceedExpectedMaximumSpeed()
    {
        shipInterface.SetPropulsion(true);
        shipInterface.SetPropulsionMultiplier(1f);
        yield return new WaitForSeconds(20);
        Assert.That(shipInterface.Speed, Is.LessThanOrEqualTo(Ship.MStoKnots(13.353f)));
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestAccelerationCurve()
    {
        shipInterface.SetPropulsion(true);
        shipInterface.SetPropulsionMultiplier(1f);


        float speedInMS = shipInterface.Speed;

        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Assert.That(shipInterface.Speed, Is.GreaterThan(speedInMS));
            speedInMS = shipInterface.Speed;
        }


        yield return null;


    }

    [UnityTest]
    public IEnumerator TheoreticalMaxSpeedShouldBe50KMH()
    {
        Assert.That(Mathf.Abs(13.353f - Ship.KnotsToMS(Ship.MStoKnots(13.353f))),Is.LessThan(0.005f));
        yield return null;
    }

    
}
