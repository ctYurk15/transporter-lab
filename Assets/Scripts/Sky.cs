using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public Material[] levelMaterials;

    public void setMaterial(int level)
    {
        GetComponent<MeshRenderer>().material = levelMaterials[level];
    }
}
