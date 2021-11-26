using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGM : GameManager
{
    public LoadableShipData testData;

    public static TestGM LoadFromResources()
    {
        return (Resources.Load("TestGM") as GameObject).GetComponent<TestGM>();
    }

}
