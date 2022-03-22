using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MaktubRotationGoal : MaktubGoal
{

    public float eulerAngletolernace = 1.0f;

    protected GameObject robot;

    // Start is called before the first frame update
    void Start()
    {
        uncompleteMaterial = (Material)Resources.Load("Materials/uncompletedGoal", typeof(Material));
        completedMaterial = (Material)Resources.Load("Materials/completedGoal", typeof(Material));
        robot = GameObject.FindGameObjectWithTag("Robot");
    }

    // Update is called once per frame
    void Update()
    {

        //check the robot rotation
        if (robot != null)
        {
            //check rotation bounds
            if (robot.transform.rotation.eulerAngles.x > this.transform.rotation.eulerAngles.x - eulerAngletolernace
             && robot.transform.rotation.eulerAngles.x < this.transform.rotation.eulerAngles.x + eulerAngletolernace
             && robot.transform.rotation.eulerAngles.y > this.transform.rotation.eulerAngles.y - eulerAngletolernace
             && robot.transform.rotation.eulerAngles.y < this.transform.rotation.eulerAngles.y + eulerAngletolernace
             && robot.transform.rotation.eulerAngles.z > this.transform.rotation.eulerAngles.z - eulerAngletolernace
             && robot.transform.rotation.eulerAngles.z < this.transform.rotation.eulerAngles.z + eulerAngletolernace)
            {
                setCompleted();
            }
        }

        if (this.completed && !this.switchedMaterials)
        {
            this.GetComponent<Renderer>().sharedMaterial = this.completedMaterial;
            switchedMaterials = true;
        }
    }

}

//custom editor control panel
#if UNITY_EDITOR
[CustomEditor(typeof(MaktubCollisionGoal))]
public class MaktubRotationGoalEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("isFailureGoal"));

        var mg = target as MaktubCollisionGoal;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("isSequenced"));

        if (mg.isSequenced)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("prerequisite"));


        serializedObject.ApplyModifiedProperties();
    }
}
#endif