using UnityEditor;
using UnityEngine;
using System.Collections;



[CreateAssetMenu(fileName = "LoadableAssets", menuName = "LoadableAssets")]
public class LoadableAssets : ScriptableObject
{
    public ShipMeshes shipMeshes = new();
    public Materials materials = new();
    public ShipDatas shipDatas = new();

    [System.Serializable]
    public class ShipMeshes
    {
        public Mesh defaultShip;
    }

    [System.Serializable]
    public class Materials
    {
        public Material defaultMat;
    }

    [System.Serializable]
    public class ShipDatas
    {
        public LoadableShipData defaultData;
    }

}

