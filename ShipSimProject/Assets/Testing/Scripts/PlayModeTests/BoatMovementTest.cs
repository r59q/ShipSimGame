using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoatMovementTest
{
    public GameObject motorBoat;
    public IShip boatInterface;

    #region set up and tear down
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        motorBoat = new GameObject("Test Boat");
        motorBoat.AddComponent<Ship>();
        IShip boatComponent = motorBoat.GetComponent<IShip>();
        boatInterface = boatComponent;
        boatComponent.Build(new MotorBoatFactory());
        yield return new EnterPlayMode();
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(motorBoat);
        motorBoat = null;
        yield return new ExitPlayMode();
    }
    #endregion

    [UnityTest]
    public IEnumerator TurningOnEngineTurnsOnEngineAndStartsAt0Throttle()
    {
        motorBoat.GetComponent<IShip>().StartPropulsion();
        Assert.That(boatInterface.IsPropelling, Is.EqualTo(true));
        Assert.That(boatInterface.PropulsionMultiplier, Is.EqualTo(0f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator WhileEngineIsOffPropulsionShouldBe0()
    {
        motorBoat.GetComponent<IShip>().StopPropulsion();
        Assert.That(boatInterface.IsPropelling, Is.EqualTo(false));
        Assert.That(boatInterface.SetPropulsionMultiplier(1f), Is.EqualTo(false));
        Assert.That(boatInterface.PropulsionMultiplier, Is.EqualTo(0f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator SettingPropulsionChangesPropulsionWhenEngineIsOn()
    {
        motorBoat.GetComponent<IShip>().StartPropulsion();
        Assert.That(boatInterface.IsPropelling, Is.EqualTo(true));
        Assert.That(boatInterface.SetPropulsionMultiplier(1f), Is.EqualTo(true));
        Assert.That(boatInterface.PropulsionMultiplier, Is.EqualTo(1f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator TurningCausesRudderPosToChange()
    {
        Assert.That(boatInterface.TurnRudderTo(1f), Is.True);
        Assert.That(boatInterface.RudderPos, Is.EqualTo(1f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator CannotOverturnOrUnderturnRudder()
    {
        Assert.That(boatInterface.TurnRudderTo(-1.1f), Is.False);
        Assert.That(boatInterface.TurnRudderTo(1.1f), Is.False);
        Assert.That(boatInterface.RudderPos, Is.EqualTo(0f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator PropellingCausesMovement()
    {
        boatInterface.StartPropulsion();
        boatInterface.SetPropulsionMultiplier(1f);
        Vector3 startPos = motorBoat.transform.position;
        yield return new WaitForSeconds(1f);
        Vector3 endPos = motorBoat.transform.position;

        Assert.That(Vector3.Distance(startPos, endPos), Is.GreaterThan(0.1f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator SpeedShouldExceed0WhenMoving()
    {
        boatInterface.SetPropulsion(true);
        boatInterface.SetPropulsionMultiplier(1f);
        yield return new WaitForSeconds(1f);
        Assert.That(boatInterface.Speed,Is.GreaterThan(0));
        yield return null;
    }

    [UnityTest]
    public IEnumerator BoatShouldHaveTurningSpeedCurve()
    {
        Assert.That(boatInterface.TurningSpeedCurve,Is.Not.Null);
        yield return null;
    }


    [UnityTest]
    public IEnumerator BoatsShouldNotExperienceGravity()
    {
        bool usingGravity = boatInterface.Rigidbody.useGravity;
        Assert.IsFalse(usingGravity);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CannotOverthrottleOrUnderthrottle()
    {
        boatInterface.StartPropulsion();
        Assert.That(boatInterface.SetPropulsionMultiplier(-1.1f), Is.False);
        Assert.That(boatInterface.SetPropulsionMultiplier(1.1f), Is.False);
        Assert.That(boatInterface.PropulsionMultiplier, Is.EqualTo(0f));
        yield return null;
    }

    [UnityTest]
    public IEnumerator RudderNot0DoesNotTurnWhenStill()
    {
        Quaternion startRot = motorBoat.transform.rotation;
        Assert.That(boatInterface.TurnRudderTo(1f), Is.True);

        boatInterface.StopPropulsion();

        yield return new WaitForSeconds(0.2f);

        Quaternion endRot = motorBoat.transform.rotation;

        Assert.AreEqual(endRot, startRot);
        yield return null;
    }

    [UnityTest]
    public IEnumerator MovingAndTurningCausesRotation()
    {
        Quaternion startRot = motorBoat.transform.rotation;

        boatInterface.StartPropulsion();
        boatInterface.SetPropulsionMultiplier(1f);
        boatInterface.TurnRudderTo(1f);

        yield return new WaitForSeconds(0.2f);
        Quaternion endRot = motorBoat.transform.rotation;
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

        Quaternion startRot = motorBoat.transform.rotation;
        boatInterface.StartPropulsion();
        boatInterface.TurnRudderTo(values.Key);
        boatInterface.SetPropulsionMultiplier(1f);
        int fcount = 0;
        yield return new WaitWhile(()=> { fcount++; return startRot == motorBoat.transform.rotation && fcount < 500; });
        Quaternion endRot = motorBoat.transform.rotation;
        Assert.That(SignWith0(1- (endRot.eulerAngles.y/180)), Is.EqualTo(values.Value));
        yield return null;
    }
    #endregion

    [UnityTest]
    public IEnumerator TurningSpeedIncreasesWithSpeed()
    {
        float turningSpeed = boatInterface.TurningSpeed;
        boatInterface.SetPropulsion(true);
        boatInterface.SetPropulsionMultiplier(1f);
        float startSpeed = turningSpeed;
        for (int i = 0; i < 200; i++)
        {
            yield return new WaitWhile(()=>turningSpeed == boatInterface.TurningSpeed);
            turningSpeed = boatInterface.TurningSpeed;
            Assert.IsTrue(turningSpeed <= boatInterface.TurningSpeed);
        }
        Assert.IsTrue(startSpeed < turningSpeed);
        yield return null;
    }

    [UnityTest]
    public IEnumerator NotPropellingCausesNoMovement()
    {
        boatInterface.StopPropulsion();
        Vector3 startPos = motorBoat.transform.position;
        yield return new WaitForSeconds(5);
        Vector3 endPos = motorBoat.transform.position;

        Assert.That(Vector3.Distance(startPos, endPos), Is.LessThan(0.00001f));
        yield return null;
    }

    void MakeBoatFast()
    {
        boatInterface.Rigidbody.velocity = motorBoat.transform.forward * 12;

    }

    [UnityTest]
    public IEnumerator HardTurnSlowsDownBoat()
    {
        MakeBoatFast();
        boatInterface.SetPropulsion(true);
        boatInterface.SetPropulsionMultiplier(1f);
        boatInterface.TurnRudderTo(1f);
        float currentSpeed = boatInterface.Speed;
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.05f);
            Assert.That(currentSpeed, Is.GreaterThan(boatInterface.Speed));
            currentSpeed = boatInterface.Speed;
        }
    }

    [UnityTest]
    public IEnumerator SlowTurnDoesNotSlowDownBoat()
    {
        MakeBoatFast();
        boatInterface.Rigidbody.velocity = boatInterface.Rigidbody.velocity * 0.8f;
        boatInterface.SetPropulsion(true);
        boatInterface.SetPropulsionMultiplier(1f);
        boatInterface.TurnRudderTo(0.2f);
        float currentSpeed = boatInterface.Speed;
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Assert.That(boatInterface.Speed, Is.GreaterThan(currentSpeed));
            currentSpeed = boatInterface.Speed;
        }
    }

}
