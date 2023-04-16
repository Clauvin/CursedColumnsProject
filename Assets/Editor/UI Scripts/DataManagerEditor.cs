using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(DataManager))]
public class DataManagerEditor : Editor
{
    bool showGameSpaceLimits = true;
    private float showGameSpace;

    bool showSecondsToApply = true;
    private float showSeconds;

    //bool showGameSpaceLimits = true;
    //private float showGameSpace;

    public override void OnInspectorGUI()
    {
        DataManager targetDataManager = (DataManager)target;

        EditorGUILayout.Vector3IntField("Block Set Spawn Point", targetDataManager.blockSetSpawnPoint);
        EditorGUILayout.Vector2IntField("Game Space", targetDataManager.gameSpace);

        #region GAMESPACELIMITS

        showGameSpaceLimits = EditorGUILayout.ToggleLeft("Show Game Space Limits", showGameSpaceLimits);

        if (showGameSpaceLimits) { showGameSpace = 1.0f; }
        else { showGameSpace = 0.0f; }

        if (EditorGUILayout.BeginFadeGroup(showGameSpace))
        {
            EditorGUILayout.IntField("Left Field", targetDataManager.leftLimit);
            EditorGUILayout.IntField("Right Field", targetDataManager.rightLimit);
            EditorGUILayout.IntField("Lower Limit", targetDataManager.lowerLimit);
            EditorGUILayout.IntField("Upper Field", targetDataManager.upperLimit);
        }
        
        EditorGUILayout.EndFadeGroup();

        #endregion GAMESPACELIMITS

        #region SECONDSTOAPPLY

        showGameSpaceLimits = EditorGUILayout.ToggleLeft("Show Game Space Limits", showGameSpaceLimits);

        if (showGameSpaceLimits) { showGameSpace = 1.0f; }
        else { showGameSpace = 0.0f; }

        if (EditorGUILayout.BeginFadeGroup(showGameSpace))
        {
            EditorGUILayout.IntField("Left Field", targetDataManager.leftLimit);
            EditorGUILayout.IntField("Right Field", targetDataManager.rightLimit);
            EditorGUILayout.IntField("Lower Limit", targetDataManager.lowerLimit);
            EditorGUILayout.IntField("Upper Field", targetDataManager.upperLimit);
        }

        EditorGUILayout.EndFadeGroup();

        #endregion SECONDSTOAPPLY

        //serializedObject.Update();




        //serializedObject.ApplyModifiedProperties();

        DrawDefaultInspector();
    }
}
