using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneSFXManager : MonoBehaviour
{
    #region Variables

    public AudioSource moveBlockLeftWithoutCollisionSound;
    public AudioSource moveBlockRightWithoutCollisionSound;
    public AudioSource moveBlockLeftWithCollisionSound;
    public AudioSource moveBlockRightWithCollisionSound;
    public AudioSource dropDownBlockSound;
    public AudioSource fallNaturallyBlockSound;
    public AudioSource matchThreeChainZeroSound;
    public AudioSource matchThreeChainOneSound;
    public AudioSource matchThreeChainTwoSound;
    public AudioSource matchThreeChainThreeSound;
    public AudioSource matchThreeChainFourSound;
    public AudioSource matchThreeChainFiveSound;
    public AudioSource matchThreeChainSixSound;
    public AudioSource reachGameOverSound;

    #endregion

    #region Play Sound Functions

    public void PlayMoveBlockLeftWithoutCollisionSound() {  moveBlockLeftWithoutCollisionSound.Play(); }
    public void PlayMoveBlockRightWithoutCollisionSound() {  moveBlockRightWithoutCollisionSound.Play(); }
    public void PlayMoveBlockLeftWithCollisionSound() { moveBlockLeftWithCollisionSound.Play(); }
    public void PlayMoveBlockRightWithCollisionSound() {  moveBlockRightWithCollisionSound.Play(); }
    public void PlayDropDownBlockSound() { dropDownBlockSound.Play(); }
    public void PlayFallNaturallyBlockSound() { fallNaturallyBlockSound.Play(); }
    public void PlayMatchThreeChainZeroSound() { matchThreeChainZeroSound.Play(); }
    public void PlayMatchThreeChainOneSound() { matchThreeChainOneSound.Play(); }
    public void PlayMatchThreeChainTwoSound() { matchThreeChainTwoSound.Play(); }
    public void PlayMatchThreeChainThreeSound() { matchThreeChainThreeSound.Play(); }
    public void PlayMatchThreeChainFourSound() { matchThreeChainFourSound.Play(); }
    public void PlayMatchThreeChainFiveSound() { matchThreeChainFiveSound.Play(); }
    public void PlayMatchThreeChainSixSound() { matchThreeChainSixSound.Play(); }
    public void PlayReachGameOverSound() { reachGameOverSound.Play(); }

    #endregion Play Sound Functions

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
