using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ShipMovementTest
{
    public GameObject motorShip;
    public IShip shipInterface;

    #region set up and tear down
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        motorShip = new GameObject("Test Ship");
        motorShip.AddComponent<Ship>();
        IShip shipComponent = motorShip.GetComponent<IShip>();
        shipInterface = shipComponent;
        shipComponent.Build(new MotorShipFactory());
        yield return new EnterPlayMode();
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(motorShip);
        motorShip = null;
        yield return new ExitPlayMode();
    }
    #endregion

    [UnityTest]
    public IEnumerator TurningOnEngineTurnsOnEngineAndStartsAt0Throttle()
    {
        motorShip.GetComponent<IShip>().StartPropulsion();
        Assert.That(shipInterface.IsPropelling, Is.EqualTo(true));
        Assert.That(shipInterface.PropulsionMultiplier, Is.EqualTo(0f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator WhileEngineIsOffPropulsionShouldBe0()
    {
        motorShip.GetComponent<IShip>().StopPropulsion();
        Assert.That(shipInterface.IsPropelling, Is.EqualTo(false));
        Assert.That(shipInterface.SetPropulsionMultiplier(1f), Is.EqualTo(false));
        Assert.That(shipInterface.PropulsionMultiplier, Is.EqualTo(0f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator SettingPropulsionChangesPropulsionWhenEngineIsOn()
    {
        motorShip.GetComponent<IShip>().StartPropulsion();
        Assert.That(shipInterface.IsPropelling, Is.EqualTo(true));
        Assert.That(shipInterface.SetPropulsionMultiplier(1f), Is.EqualTo(true));
        Assert.That(shipInterface.PropulsionMultiplier, Is.EqualTo(1f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator TurningCausesRudderPosToChange()
    {
        Assert.That(shipInterface.TurnRudderTo(1f), Is.True);
        Assert.That(shipInterface.RudderPos, Is.EqualTo(1f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator CannotOverturnOrUnderturnRudder()
    {
        Assert.That(shipInterface.TurnRudderTo(-1.1f), Is.False);
        Assert.That(shipInterface.TurnRudderTo(1.1f), Is.False);
        Assert.That(shipInterface.RudderPos, Is.EqualTo(0f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator PropellingCausesMovement()
    {
        shipInterface.StartPropulsion();
        shipInterface.SetPropulsionMultiplier(1f);
        Vector3 startPos = motorShip.transform.position;
        yield return new WaitForSeconds(1f);
        Vector3 endPos = motorShip.transform.position;

        Assert.That(Vector3.Distance(startPos, endPos), Is.GreaterThan(0.1f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator SpeedShouldExceed0WhenMoving()
    {
        shipInterface.SetPropulsion(true);
        shipInterface.SetPropulsionMultiplier(1f);
        yield return new WaitForSeconds(1f);
        Assert.That(shipInterface.Speed,Is.GreaterThan(0));
        yield return null;
    }

    [UnityTest]
    public IEnumerator BoatShouldHaveTurningSpeedCurve()
    {
        Assert.That(shipInterface.TurningSpeedCurve,Is.Not.Null);
        yield return null;
    }


    [UnityTest]
    public IEnumerator BoatsShouldNotExperienceGravity()
    {
        bool usingGravity = shipInterface.Rigidbody.useGravity;
        Assert.IsFalse(usingGravity);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CannotOverthrottleOrUnderthrottle()
    {
        shipInterface.StartPropulsion();
        Assert.That(shipInterface.SetPropulsionMultiplier(-1.1f), Is.False);
        Assert.That(shipInterface.SetPropulsionMultiplier(1.1f), Is.False);
        Assert.That(shipInterface.PropulsionMultiplier, Is.EqualTo(0f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator RudderNot0DoesNotTurnWhenStill()
    {
        Quaternion startRot = motorShip.transform.rotation;
        Assert.That(shipInterface.TurnRudderTo(1f), Is.True);

        shipInterface.StopPropulsion();

        yield return new WaitForSeconds(0.2f);

        Quaternion endRot = motorShip.transform.rotation;

        Assert.AreEqual(endRot, startRot);
        yield return null;
    }

    [UnityTest]
    public IEnumerator MovingAndTurningCausesRotation()
    {
        Quaternion startRot = motorShip.transform.rotation;

        shipInterface.StartPropulsion();
        shipInterface.SetPropulsionMultiplier(1f);
        shipInterface.TurnRudderTo(1f);

        yield return new WaitForSeconds(0.2f);
        Quaternion endRot = motorShip.transform.rotation;
        Assert.AreNotEqual(endRot, startRot);

        yield return null;
    }

    #region Turn tests
    static KeyValuePair<float, int>[] turnTestvalues = new KeyValuePair<float, int>[]
    {
        new KeyValuePair<float, int>(-1f, -1),
        new KeyValuePair<float, int>(-0.1f, -1),
        new KeyValuePair<float, int>(0.1f, 1),
        new KeyValuePair<float, int>(0, 0)
    };

    private static int SignWith0(float x)
    {
        return x == 1f ? 0 : (int) Mathf.Sign(x);
    }

    [UnityTest]
    public IEnumerator TurningTest([ValueSource("turnTestvalues")] KeyValuePair<float, int> values)
    {

        Quaternion startRot = motorShip.transform.rotation;
        shipInterface.StartPropulsion();
        shipInterface.TurnRudderTo(values.Key);
        shipInterface.SetPropulsionMultiplier(1f);
        int fcount = 0;
        yield return new WaitWhile(()=> { fcount++; return startRot == motorShip.transform.rotation && fcount < 500; });
        Quaternion endRot = motorShip.transform.rotation;
        Assert.That(SignWith0(1- (endRot.eulerAngles.y/180)), Is.EqualTo(values.Value));
        yield return null;
    }
    #endregion

    [UnityTest]
    public IEnumerator TurningSpeedIncreasesWithSpeed()
    {
        float turningSpeed = shipInterface.TurningSpeed;
        shipInterface.SetPropulsion(true);
        shipInterface.SetPropulsionMultiplier(1f);
        float startSpeed = turningSpeed;
        for (int i = 0; i < 200; i++)
        {
            yield return new WaitWhile(()=>turningSpeed == shipInterface.TurningSpeed);
            turningSpeed = shipInterface.TurningSpeed;
            Assert.IsTrue(turningSpeed <= shipInterface.TurningSpeed);
        }
        Assert.IsTrue(startSpeed < turningSpeed);
        yield return null;
    }

    [UnityTest]
    public IEnumerator NotPropellingCausesNoMovement()
    {
        shipInterface.StopPropulsion();
        Vector3 startPos = motorShip.transform.position;
        yield return new WaitForSeconds(5);
        Vector3 endPos = motorShip.transform.position;

        Assert.That(Vector3.Distance(startPos, endPos), Is.LessThan(0.00001f));
        yield return null;
    }

    void MakeBoatFast()
    {
        shipInterface.Rigidbody.velocity = motorShip.transform.forward * 12;

    }

    [UnityTest]
    public IEnumerator HardTurnSlowsDownShip()
    {
        MakeBoatFast();
        shipInterface.SetPropulsion(true);
        shipInterface.SetPropulsionMultiplier(1f);
        shipInterface.TurnRudderTo(1f);
        float currentSpeed = shipInterface.Speed;
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.05f);
            Assert.That(currentSpeed, Is.GreaterThan(shipInterface.Speed));
            currentSpeed = shipInterface.Speed;
        }
    }

    [UnityTest]
    public IEnumerator SlowTurnDoesNotSlowDownShip()
    {
        MakeBoatFast();
        shipInterface.Rigidbody.velocity = shipInterface.Rigidbody.velocity * 0.8f;
        shipInterface.SetPropulsion(true);
        shipInterface.SetPropulsionMultiplier(1f);
        shipInterface.TurnRudderTo(0.2f);
        float currentSpeed = shipInterface.Speed;
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Assert.That(shipInterface.Speed, Is.GreaterThan(currentSpeed));
            currentSpeed = shipInterface.Speed;
        }
    }

}
