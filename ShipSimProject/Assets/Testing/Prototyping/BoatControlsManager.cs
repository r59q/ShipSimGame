using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControlsManager : GameManager
{
    GameObject prototypingBoat;

    private void Start()
    {
        prototypingBoat = new GameObject("ProtoBoat");
        prototypingBoat.AddComponent<PrototypingMotorBoat>().Build(new MotorShipFactory());
        prototypingBoat.GetComponent<PrototypingMotorBoat>().SetPropulsion(true);
        prototypingBoat.GetComponent<PrototypingMotorBoat>().SetPropulsionMultiplier(0.1f);
        prototypingBoat.GetComponent<PrototypingMotorBoat>().TurnRudderTo(-1f);
    }

    private void Update()
    {
        
    }

}
