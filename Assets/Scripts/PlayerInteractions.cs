using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    public InputActionAsset actions;

    private InputAction moveAction;
    private InputAction pauseAction;
    private InputAction closeGameAction;

    public Vector2 moveAmount;

    public bool cycleButtonIsCurrentlyPressedAfterApplyingItsEffect;

    public bool moveButtonIsCurrentlyPressed;

    public bool pauseIsCurrentlyPressed;
    public bool pauseJustPressed;
    public bool pausePressReleaseHappened;

    public bool closeJustPressed;

    void Awake()
    {
        moveAction = actions.FindActionMap("player").FindAction("move");
        pauseAction = actions.FindActionMap("player").FindAction("pause");
        closeGameAction = actions.FindActionMap("player").FindAction("close");

        cycleButtonIsCurrentlyPressedAfterApplyingItsEffect = false;

        moveButtonIsCurrentlyPressed = false;

        pauseIsCurrentlyPressed = false;
        pauseJustPressed = false;
        pausePressReleaseHappened = true;

        closeGameAction.Enable();
        closeJustPressed = false;
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
            GameSceneUIManager.Unpause();
        }
        else
        {
            DataManager.Pause();
            GameSceneUIManager.Pause();
        }
    }

    public void OnCloseGame()
    {
        #if UNITY_STANDALONE
        ExitGame();
        #endif

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void ExitGame()
    {
        Application.Quit();
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
        if (pauseValue > 0.7f && pausePressReleaseHappened)
        {
            pauseIsCurrentlyPressed = true;
            pauseJustPressed = true;
            pausePressReleaseHappened = false;
        }
        else if (pauseValue < 0.7 && !pausePressReleaseHappened)
        {
            pauseIsCurrentlyPressed = false;
            pauseJustPressed = false;
            pausePressReleaseHappened = true;
        }
        else
        {
            pauseJustPressed = false;
        }

        float closeValue = closeGameAction.ReadValue<float>();
        if (closeGameAction.ReadValue<float>() > 0.7f)
        {
            closeJustPressed = true;
        }
    }
}
