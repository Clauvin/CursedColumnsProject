using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartManager()
    {
        
    }

    private bool MoveBlockLaterally()
    {
        return false;
    }

    private void FastBlockDrop()
    {

    }

    private void InstantBlockDrop()
    {

    }

    public void OnPause()
    {
        if (DataManager.isPaused)
        {
            DataManager.Unpause();
        }
        else
        {
            DataManager.Pause();
        }
    }

    private void ExitGame()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }


}
