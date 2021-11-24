using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour, IShip
{

    private Rigidbody rb;

    List<IDetectableEntity> detectedEntitiesList;

    /// <summary>
    /// Builds a boat on the GameObject this script is bound to.
    /// </summary>
    /// <param name="boatFactory">The BoatFactory object to build the boat from</param>
    public void Build(IBoatFactory boatFactory)
    {
        // Create and render mesh
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
        mf.mesh = boatFactory.CreateMesh();

        // Add rigid body.
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false; // Avoid the boat falling through the 'ground'

        // Create detected entities list
        detectedEntitiesList = new List<IDetectableEntity>();

        // Build properties from boat factory.
        AccelerationCurve = boatFactory.CreateAccelerationCurve(); 
        TurningSpeedCurve = boatFactory.CreateTurningSpeedCurve();
        OptimalTurnSpeed = boatFactory.CreateOptimalTurnSpeed();
        mr.sharedMaterial = boatFactory.CreateMaterial();
        
        // Add the 'visual' sphere of the boat. I.e the detection collider
        SphereCollider detectCol = gameObject.AddComponent<SphereCollider>();
        detectCol.isTrigger = true;
        detectCol.radius = boatFactory.CreateDetectionRange();
    }

   

    /// <summary>
    /// Sets the propulsion state. Decide whether or not the boat should have propulsion.
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
                if (Mathf.Abs( RudderPos)> OptimalTurnSpeed)
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
        get; private set;
    }

    public IDetectableEntity[] DetectedEntities
    {
        get { return detectedEntitiesList.ToArray(); }
    }

    public ICurve AccelerationCurve { get; private set; }

    public bool IsPropelling { get; protected set; }

    public float Speed => rb.velocity.magnitude;

    public float Acceleration => AccelerationCurve.F((Speed));

    public ICurve TurningSpeedCurve
    {
        get;private set;
    }

    public float TurningSpeed => TurningSpeedCurve.F(Speed);
    
    public float DetectionRange
    {
        get { return DetectionCollider.radius; }
    }

    public SphereCollider DetectionCollider
    {
        get { return GetComponent<SphereCollider>(); }
    }

        
    public float CompassDirection { get { return transform.rotation.eulerAngles.y % 360; } }

    #region OnTrigger events
    private void OnTriggerEnter(Collider other)
    {
        IDetectableEntity entity = other.GetComponent<IDetectableEntity>();
        if (entity != null)
        {
            // Object is detectable
            detectedEntitiesList.Add(entity);
        }
        else
        {
            // object was not detectable
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDetectableEntity entity = other.GetComponent<IDetectableEntity>();
        if (entity != null)
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
