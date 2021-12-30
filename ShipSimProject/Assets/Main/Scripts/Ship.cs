using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IShip, IDetectableEntity
{

    private Rigidbody rb;

    List<IDetectableEntity> detectedEntitiesList;

    /// <summary>
    /// Builds a ship on the GameObject this script is bound to.
    /// </summary>
    /// <param name="shipFactory">The ShipFactory object to build the ship from</param>
    public void Build(IShipFactory shipFactory)
    {
        // Create and render mesh
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
        mf.mesh = shipFactory.CreateMesh();

        // Add rigid body.
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false; // Avoid the ship falling through the 'ground'
        rb.mass = shipFactory.CreateMass();

        // Create detected entities list
        detectedEntitiesList = new List<IDetectableEntity>();

        // Build properties from ship factory.
        HandlingProfile = shipFactory.CreateHandlingProfile();
        mr.sharedMaterial = shipFactory.CreateMaterial();

        // Add the 'visual' sphere of the boat. I.e the detection collider
        SphereCollider detectCol = gameObject.AddComponent<SphereCollider>();
        detectCol.isTrigger = true;
        detectCol.radius = shipFactory.CreateDetectionRange();
        DetectionCollider = detectCol;

        SphereCollider collisionCol = gameObject.AddComponent<SphereCollider>();
        collisionCol.radius = shipFactory.CreateSize();
        Collider = collisionCol;
    }



    /// <summary>
    /// Sets the propulsion state. Decide whether or not the ship should have propulsion.
    /// Use this to turn off engine or lower the sails.
    /// </summary>
    /// <param name="state">Propulsion state.</param>
    /// <returns>If the requested state was granted.</returns>
    public bool SetPropulsion(bool state)
    {
        IsPropelling = state;
        return true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Move
        if (IsPropelling)
            rb.velocity = transform.forward * (Speed + (Acceleration * Time.deltaTime));

        // rotate
        if (IsPropelling)
        {
            if (RudderPos != 0f)
            {
                transform.RotateAround(transform.position, Vector3.up, TurningSpeed * RudderPos * Time.deltaTime);
                if (Speed > OptimalTurnSpeed)
                    rb.velocity -= rb.velocity * 0.1f * Time.deltaTime;
            }
        }
    }


    public bool SetPropulsionMultiplier(float multiplier)
    {
        if (multiplier < -1 || multiplier > 1) return false;
        PropulsionMultiplier = IsPropelling ? multiplier : 0f;

        return IsPropelling;
    }


    // 1 is right -1 is left
    public bool TurnRudderTo(float rudderPos)
    {
        if (rudderPos < -1f || rudderPos > 1f) return false;

        RudderPos = rudderPos;

        return true;
    }

    public float RudderPos { get; private set; }

    public Rigidbody Rigidbody => rb;

    public float PropulsionMultiplier
    {
        get; private set;
    }

    public float OptimalTurnSpeed
    {
        get {
            Keyframe highestFrame = new Keyframe(0,-1);
            for (int i = 0; i < HandlingProfile.TurningCurve.length; i++)
            {
                Keyframe currentKeyFrame = HandlingProfile.TurningCurve.keys[i];
                if (currentKeyFrame.value > highestFrame.value)
                {
                    highestFrame = currentKeyFrame;
                }
            }
            return highestFrame.time * HandlingProfile.TopSpeed;
        } 
    }

    public IDetectableEntity[] DetectedEntities
    {
        get { return detectedEntitiesList.ToArray(); }
    }

    public bool IsPropelling { get; protected set; }

    public float Speed => rb.velocity.magnitude;

    public float Acceleration => HandlingProfile.GetAccelerationAt(Speed);

    public float TurningSpeed => HandlingProfile.GetTurningAt(Speed);

    public float DetectionRange
    {
        get { return DetectionCollider.radius; }
    }

    public SphereCollider DetectionCollider
    {
        get;private set;
    }


    public float CompassDirection { get { return transform.rotation.eulerAngles.y % 360; } }

    public HandlingProfile HandlingProfile { get; private set; }

    public float Mass => rb.mass;

    public float TopSpeed => HandlingProfile.TopSpeed;

    public float Size => Collider.radius;

    public float TopTurningSpeed => HandlingProfile.TurningSpeed;

    public SphereCollider Collider { get; private set; }

    #region OnTrigger events
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            IDetectableEntity entity = other.GetComponent<IDetectableEntity>();
            if (entity != null && entity != gameObject.GetComponent<IDetectableEntity>())
            {
                // Object is detectable and not self
                detectedEntitiesList.Add(entity);
            }
            else
            {
                // object was not detectable
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        IDetectableEntity entity = other.GetComponent<IDetectableEntity>();
        if (entity != null && entity != gameObject.GetComponent<IDetectableEntity>())
        {
            // Object is detectable
            detectedEntitiesList.Remove(entity);
        }
        else
        {
            // object was not detectable
        }
    }
    #endregion


    // ---- Static functions ----

    #region Speed Conversion Functions

    public static float MStoKMH(float ms)
    {
        return ms * 3.6f;
    }

    public static float MStoKnots(float ms)
    {
        return ms * 1.943844f;
    }

    public static float KMHToKnots(float kmh)
    {
        return kmh / 1.85200f;
    }

    public static float KnotsToKMH(float knots)
    {
        return knots * 1.85200f;
    }

    public static float KMHtoMS(float kmh)
    {
        return kmh * 1000 / 60 / 60;
    }

    public static float KnotsToMS(float knots)
    {
        return KnotsToKMH(knots) * 1000 / 60 / 60;
    }

    #endregion


}
