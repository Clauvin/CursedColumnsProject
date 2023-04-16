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

    bool showTimeLimits = true;
    private float showTime;

    bool showDifficultyLevelAndScoreValues = true;
    private float showDifficultyLevelAndScore;

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

        showSecondsToApply = EditorGUILayout.ToggleLeft("Show Seconds To Apply", showSecondsToApply);

        if (showSecondsToApply) { showSeconds = 1.0f; }
        else { showSeconds = 0.0f; }

        if (EditorGUILayout.BeginFadeGroup(showSeconds))
        {
            EditorGUILayout.FloatField("Seconds To Apply Block Gravity", targetDataManager.secondsToApplyBlockGravity);
            EditorGUILayout.FloatField("Seconds To Check Player Input", targetDataManager.secondsToCheckPlayerInput);
            EditorGUILayout.FloatField("Seconds Of Delay After Match Checks", targetDataManager.secondsOfDelayAfterAllMatchChecks);
            
        }

        EditorGUILayout.EndFadeGroup();

        #endregion SECONDSTOAPPLY

        #region TIMES

        showTimeLimits = EditorGUILayout.ToggleLeft("Show Time Limits", showTimeLimits);

        if (showTimeLimits) { showTime = 1.0f; }
        else { showTime = 0.0f; }

        if (EditorGUILayout.BeginFadeGroup(showTime))
        {
            EditorGUILayout.FloatField("Time Without Input Check", targetDataManager.timeWithoutInputCheck);
            EditorGUILayout.FloatField("Time For Delay After All Match Checks", targetDataManager.timeForDelayAfterAllMatchChecks);
            EditorGUILayout.Toggle("Is Delaying After All Match Checks", targetDataManager.isDelayingAfterAllMatchChecks);
            EditorGUILayout.FloatField("Time Without PBlock Gravity", targetDataManager.timeWithoutPBlockGravityBeingApplied);
        }

        EditorGUILayout.EndFadeGroup();

        #endregion TIMES

        #region DIFFANDSCORE

        showDifficultyLevelAndScoreValues = EditorGUILayout.ToggleLeft("Show Difficulty And Score Limits", showDifficultyLevelAndScoreValues);

        if (showDifficultyLevelAndScoreValues) { showDifficultyLevelAndScore = 1.0f; }
        else { showDifficultyLevelAndScore = 0.0f; }

        if (EditorGUILayout.BeginFadeGroup(showDifficultyLevelAndScore))
        {
            EditorGUILayout.IntField("Current Difficulty Level", targetDataManager.currentDifficultyLevel);
            EditorGUILayout.IntField("Current Score", targetDataManager.currentScore);
        }

        EditorGUILayout.EndFadeGroup();

        #endregion DIFFANDSCORE

        SerializedProperty spDiffScoreValues = serializedObject.FindProperty("difficultyScoreValues");
        EditorGUILayout.PropertyField(spDiffScoreValues);

        SerializedProperty spBlockGravityDiffValues = serializedObject.FindProperty("blockGravityDiffValues");
        EditorGUILayout.PropertyField(spBlockGravityDiffValues);

        SerializedProperty spPlayerInputTimeDiffValues = serializedObject.FindProperty("playerInputTimeDiffValues");
        EditorGUILayout.PropertyField(spPlayerInputTimeDiffValues);

        SerializedProperty spDelayAfterMatchDiffValues = serializedObject.FindProperty("delayAfterMatchDiffValues");
        EditorGUILayout.PropertyField(spDelayAfterMatchDiffValues);
    }
}
