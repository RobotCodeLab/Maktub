using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaktubGoal : MonoBehaviour
{

    [HideInInspector]
    public bool completed = false;
    [HideInInspector]
    public bool isSequenced = false;
    [HideInInspector]
    public Object prerequisite = null;

    protected Material uncompleteMaterial;
    protected Material completedMaterial;
    protected bool switchedMaterials = false;
}