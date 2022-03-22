using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class MaktubRoot : MonoBehaviour
{

    public float updatesPerSecond = 10;

    GameObject[] goals;
    ROSConnection ros;

    private bool testFinished = false;
    private float timeSinceLastUpdate = 0;
    // Start is called before the first frame update
    void Start()
    {
        //find all goals in the scene
        goals = GameObject.FindGameObjectsWithTag("Goal");
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<StringMsg>("maktub/test_log");
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;
        if (!testFinished && timeSinceLastUpdate > (1 / updatesPerSecond))
        {
            bool complete = true;
            foreach (GameObject goal in goals)
            {
                if (!goal.GetComponent<MaktubGoal>().completed)
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
