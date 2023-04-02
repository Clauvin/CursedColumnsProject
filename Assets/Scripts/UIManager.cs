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
    }

    public static void Pause()
    {
        pauseText.enabled = true;
    }

    public static void Unpause()
    {
        pauseText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
