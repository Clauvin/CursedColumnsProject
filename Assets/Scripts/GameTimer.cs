using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private double timer;

    public double GetTimer()
    {
        return timer;
    }

    // Start is called before the first frame update
    public void StartManager()
    {
        timer = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        if (!DataManager.isPaused)
        {
            timer += Time.deltaTime;
        }
    }
}
