using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControlsManager : GameManager
{
    GameObject prototypingShip;

    private void Start()
    {
        prototypingShip = new GameObject("ProtoShip");
        prototypingShip.AddComponent<PrototypingMotorShip>().Build(new MotorShipFactory());
        prototypingShip.GetComponent<PrototypingMotorShip>().SetPropulsion(true);
        prototypingShip.GetComponent<PrototypingMotorShip>().SetPropulsionMultiplier(0.1f);
        prototypingShip.GetComponent<PrototypingMotorShip>().TurnRudderTo(-1f);
    }

    private void Update()
    {
        
    }

}
