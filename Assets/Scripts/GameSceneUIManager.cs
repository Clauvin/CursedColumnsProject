using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : MonoBehaviour
{
    public static Text pauseText;
    public Text pauseTextToLoad;

    public static Text scoreText;
    public Text scoreTextToLoad;

    public static Text scoreValueText;
    public Text scoreValueTextToLoad;

    public static Text diffText;
    public Text diffTextToLoad;

    public static Text diffValueText;
    public Text diffValueTextToLoad;

    public static Text gameOverText;
    public Text gameOverTextToLoad;

    public static GameObject resetGameSceneButtonGameObject;
    public GameObject resetGameSceneButtonGameObjectToLoad;

    public static GameObject backToMainScreenButtonGameObject;
    public GameObject backToMainScreenButtonGameObjectToLoad;

    // Start is called before the first frame update
    void Start()
    {
        pauseText = pauseTextToLoad;
        if (pauseText.enabled)
        {
            pauseText.enabled = false;
        }

        scoreText = scoreTextToLoad;
        scoreValueText = scoreValueTextToLoad;

        diffText = diffTextToLoad;
        diffValueText = diffValueTextToLoad;

        gameOverText = gameOverTextToLoad;

        resetGameSceneButtonGameObject = resetGameSceneButtonGameObjectToLoad;
        backToMainScreenButtonGameObject = backToMainScreenButtonGameObjectToLoad;
}

    public static void Pause()
    {
        pauseText.enabled = true;
    }

    public static void Unpause()
    {
        pauseText.enabled = false;
    }

    public static void UpdateScoreUI(int value)
    {
        scoreValueText.text = value.ToString();
    }

    public static void UpdateDifficultyLevelUI(int value)
    {
        diffValueText.text = value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
