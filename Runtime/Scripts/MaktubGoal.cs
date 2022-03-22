using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class MaktubGoal : MonoBehaviour
{

    public bool isSequenced = false;
    public Object prerequisite = null;
    [HideInInspector]
    public bool completed = false;

    protected Material uncompleteMaterial;
    protected Material completedMaterial;
    protected bool switchedMaterials = false;


    protected void setCompleted()
    {
        if (isSequenced)
        {
            if (prerequisite != null && ((GameObject)prerequisite).GetComponent<MaktubGoal>().completed)
            {
                this.completed = true;
            }
        }
        else
        {
            this.completed = true;
        }
        ROSConnection.GetOrCreateInstance().Publish("maktub/test_log", new StringMsg(completionMessage()));
    }

    protected string completionMessage()
    {
        return "Completed Goal: " + this.gameObject.name;
    }
}