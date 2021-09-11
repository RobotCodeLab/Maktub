using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MaktubCollisionGoal : MaktubGoal
{
    // Start is called before the first frame update
    void Start()
    {
        uncompleteMaterial = (Material)Resources.Load("Materials/uncompletedGoal", typeof(Material));
        completedMaterial = (Material)Resources.Load("Materials/completedGoal", typeof(Material));
    }

    // Update is called once per frame
    void Update()
    {

        if (this.completed && !this.switchedMaterials)
        {
            this.GetComponent<Renderer>().sharedMaterial = this.completedMaterial;
            switchedMaterials = true;
        }
    }


    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Robot")
        {
            setCompleted();
        }
    }
}

[CustomEditor(typeof(MaktubCollisionGoal))]
public class MaktubCollisionGoalEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var mg = target as MaktubCollisionGoal;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("isSequenced"));

        if (mg.isSequenced)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("prerequisite"));


        serializedObject.ApplyModifiedProperties();
    }
}
