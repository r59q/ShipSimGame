using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypingMotorBoat : Boat
{

    public float sped;

    private void Start()
    {
        
    }
    protected override void Update()
    {
        base.Update();
        sped = Rigidbody.velocity.magnitude;
    }
}
