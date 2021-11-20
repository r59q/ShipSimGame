using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceLoader
{
    public static class Boats
    {
        public static Mesh TestBoat => (Resources.Load("Mesh/TestBoat") as GameObject).GetComponent<MeshFilter>().sharedMesh;
    }
    public static class Materials
    {
        public static Material TestMat => (Resources.Load("Material/TestMat") as Material);
    }

}
