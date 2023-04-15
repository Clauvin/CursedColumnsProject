using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
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
    }

    public static void Pause()
    {
        pauseText.enabled = true;
    }

    public static void Unpause()
    {
        pauseText.enabled = false;
    }

    public static void UpdateScoreUI()
    {
        scoreValueText.text = DataManager.currentScore.ToString();
    }

    public static void UpdateDifficultyLevelUI()
    {
        diffValueText.text = DataManager.currentDifficultyLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
