using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MaktubCollisionGoal : MaktubGoal
{
    // Load assets needed for the update loop, in this case the materials for being a complete/uncomplete goal
    void Start()
    {
        uncompleteMaterial = (Material)Resources.Load("Materials/uncompletedGoal", typeof(Material));
        completedMaterial = (Material)Resources.Load("Materials/completedGoal", typeof(Material));
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Robot")
        {
            setCompleted();
            
            //change our material to visually indicate completion
            if(this.completed && !this.switchedMaterials)
            {
                this.GetComponent<Renderer>().sharedMaterial = this.completedMaterial;
                switchedMaterials = true;
            }
        }
    }
}

//custom editor control panel
#if UNITY_EDITOR
[CustomEditor(typeof(MaktubCollisionGoal))]
public class MaktubCollisionGoalEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("isFailureGoal"));

        var mg = target as MaktubCollisionGoal;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("isSequenced"));

        if (mg.isSequenced)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("prerequisites"));


        serializedObject.ApplyModifiedProperties();
    }
}
#endif