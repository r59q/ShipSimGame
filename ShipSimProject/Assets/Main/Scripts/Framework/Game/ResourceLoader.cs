using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceLoader
{

    private static LoadableAssets storedAssets;

    public static LoadableAssets Load
    {
        get { 
            if (storedAssets == null)
            {
                storedAssets = Resources.Load("LoadableAssets") as LoadableAssets;
                if (storedAssets == null)
                {
                    throw new System.NullReferenceException("WARNING: A LoadableAssets object must be present in a resources folder.");
                }
            }
            return storedAssets;
        }
    }

}
