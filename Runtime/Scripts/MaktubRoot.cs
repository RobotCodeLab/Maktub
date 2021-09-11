using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaktubRoot : MonoBehaviour
{

    GameObject[] goals;

    // Start is called before the first frame update
    void Start()
    {
        //find all goals in the scene
        goals = GameObject.FindGameObjectsWithTag("goal");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
