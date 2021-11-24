using UnityEditor;
using UnityEngine;
using System.Collections;



[CreateAssetMenu(fileName = "LoadableAssets", menuName = "LoadableAssets")]
public class LoadableAssets : ScriptableObject
{
    public BoatMesh boatMeshes = new();
    public Materials materials = new();

    [System.Serializable]
    public class BoatMesh
    {
        public Mesh testBoat;
    }

    [System.Serializable]
    public class Materials
    {
        public Material testMat;
    }

}