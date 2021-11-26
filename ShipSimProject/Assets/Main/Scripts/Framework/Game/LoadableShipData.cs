using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship data", menuName = "Loadable Ship Data")]
public class LoadableShipData : ScriptableObject
{
    [SerializeField]
    private float topSpeed;

    public float TopSpeed => topSpeed;
    
}
