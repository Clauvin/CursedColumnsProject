using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationAtScenes : MonoBehaviour
{
    public static void ToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public static void ToOptionsScene()
    {
        Debug.Log("Options Scene Haven't Been Added Yet");
    }

    public static void CloseGame()
    {
        #if UNITY_STANDALONE
        Application.Quit();
        #endif

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
