using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private List<GameObject> shipObjects;

    protected virtual void Awake()
    {
        shipObjects = new List<GameObject>();
    }

    public GameObject BuildShip(IShipFactory factory, Vector3 pos)
    {
        GameObject shipObject = new GameObject("Boat");
        IShip ship = shipObject.AddComponent<Ship>();
        shipObject.transform.position = pos;
        ship.Build(factory);

        shipObjects.Add(shipObject);
        return shipObject;
    }

    public int GetShipCount()
    {
        return shipObjects.Count;
    }
}
