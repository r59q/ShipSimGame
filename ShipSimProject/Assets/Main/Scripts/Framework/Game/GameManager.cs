using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private List<GameObject> boatObjects;

    protected virtual void Awake()
    {
        boatObjects = new List<GameObject>();
    }

    public GameObject BuildBoat(IShipFactory factory, Vector3 pos)
    {
        GameObject boatObject = new GameObject("Boat");
        IShip boat = boatObject.AddComponent<Ship>();
        boatObject.transform.position = pos;
        boat.Build(factory);

        boatObjects.Add(boatObject);
        return boatObject;
    }

    public int GetBoatCount()
    {
        return boatObjects.Count;
    }
}
