using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

[System.Serializable]
public enum PrerequisiteOperation
{
    AND,
    OR
}

[System.Serializable]
public struct Prerequisite
{
    public PrerequisiteOperation operation;
    public Object goal;
}

public class MaktubGoal : MonoBehaviour
{
    public bool isFailureGoal = false;
    public bool isSequenced = false;
    public List<Prerequisite> prerequisites;
    [HideInInspector]
    public bool completed = false;

    protected Material uncompleteMaterial;
    protected Material completedMaterial;
    protected bool switchedMaterials = false;

    void Start()
    {
        prerequisites = new List<Prerequisite>();
    }

    protected void setCompleted()
    {
        //if the goal is already completed we dont need to do any of these calculations
        if (!this.completed)
        {
            //compute whether or not we have our prerequisite goals fulfilled
            bool hasPrereqs = false;
            if (prerequisites.Count > 0)
            {
                hasPrereqs = ((GameObject)prerequisites[0].goal).GetComponent<MaktubGoal>().completed;

                for (int i = 1; i < prerequisites.Count; i++)
                {
                    if(prerequisites[i].operation == PrerequisiteOperation.AND)
                        hasPrereqs = hasPrereqs & ((GameObject)prerequisites[i].goal).GetComponent<MaktubGoal>().completed;
                    else
                        hasPrereqs = hasPrereqs | ((GameObject)prerequisites[i].goal).GetComponent<MaktubGoal>().completed;

                }
            }

            //if we dont have any prereqs or we have our prereqs fulfilled then set the goal to be completed
            if (!isSequenced || hasPrereqs)
            {
                ROSConnection.GetOrCreateInstance().Publish("maktub/test_log", new StringMsg(completionMessage()));
                this.completed = true;
            }
        }
    }

    protected string completionMessage()
    {
        return "Completed Goal: " + this.gameObject.name;
    }
}