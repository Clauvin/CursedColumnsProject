using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    public InputActionAsset actions;

    private InputAction moveAction;
    private InputAction pauseAction;

    public Vector2 moveAmount;
    public bool pausePressed;

    void Awake()
    {
        moveAction = actions.FindActionMap("player").FindAction("move");
        pauseAction = actions.FindActionMap("player").FindAction("pause");
    }

    public void StartManager()
    {
        moveAmount = new Vector2(0, 0);
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

    public void OnEnable()
    {
        moveAction.Enable();
        pauseAction.Enable();
    }

    public void OnDisable()
    {
        moveAction.Disable();
        pauseAction.Disable();
    }

    public void Update()
    {
        moveAmount = moveAction.ReadValue<Vector2>();
        float pauseValue = pauseAction.ReadValue<float>();
        if (pauseValue > 0.7f)
        {
            pausePressed = true;
        }
        else
        {
            pausePressed = false;
        }
   
    }


}
