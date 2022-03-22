using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class MaktubRoot : MonoBehaviour
{

    public float updatesPerSecond = 10;

    List<MaktubGoal> successGoals;
    List<MaktubGoal> failureGoals;
    ROSConnection ros;

    private bool testFinished = false;
    private float timeSinceLastUpdate = 0;
    // Start is called before the first frame update
    void Start()
    {
        //find all goals in the scene
        GameObject[] goals = GameObject.FindGameObjectsWithTag("Goal");
        successGoals = new List<MaktubGoal>();
        failureGoals = new List<MaktubGoal>();

        //split into success and failure goals
        foreach(GameObject goal in goals)
        {
            MaktubGoal mg = goal.GetComponent<MaktubGoal>();
            if(mg.isFailureGoal)
                failureGoals.Add(mg);
            else
                successGoals.Add(mg);
        }
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<StringMsg>("maktub/test_log");
    }

    // Update is called once per frame
    void Update()
    {
        //check our goals to see if we're done with the test
        //do so only X times per second as defined in the public vars
        timeSinceLastUpdate += Time.deltaTime;
        if (!testFinished && timeSinceLastUpdate > (1 / updatesPerSecond))
        {

            //check if we failed
            foreach(MaktubGoal goal in failureGoals)
            {
                if(goal.completed)
                {
                    testFinished = true;
                    ros.Publish("maktub/test_log", new StringMsg("Test failed"));
                }
            }

            //if we didnt fail, lets see if we were successful
            bool complete = true;
            foreach (MaktubGoal goal in successGoals)
            {
                if (!goal.completed)
                    complete = false;
            }

            if (complete)
            {
                testFinished = true;
                ros.Publish("maktub/test_log", new StringMsg("Test successfully completed"));
            }
            
            timeSinceLastUpdate = 0;
        }
    }
}
