using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypingMotorShip : Ship
{

    public float speed;

    private void Start()
    {
        
    }
    protected override void Update()
    {
        base.Update();
        speed = Rigidbody.velocity.magnitude;
    }
}
