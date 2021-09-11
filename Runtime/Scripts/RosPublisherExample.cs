using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;

public class RosPublisherExample : MonoBehaviour
{

    ROSConnection ros;
    // The game object
    public GameObject cube;
    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 0.5f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.instance;
        ros.RegisterPublisher<PoseMsg>("unity/test");
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > publishMessageFrequency)
        {
            cube.transform.rotation = Random.rotation;
            cube.transform.position = Random.insideUnitSphere;

            PoseMsg cubePos = new PoseMsg(new PointMsg(cube.transform.position.x, cube.transform.position.y, cube.transform.position.z), 
                                            new QuaternionMsg(cube.transform.rotation.x, cube.transform.rotation.y, cube.transform.rotation.z, cube.transform.rotation.w));

            // Finally send the message to server_endpoint.py running in ROS
            ros.Send("unity/test", cubePos);

            timeElapsed = 0;
        }
    }
}
