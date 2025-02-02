using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class ReadyToSpinState : State
{
    void Start()
    {
        uiManager.spinButton.onClick.AddListener(() => {
            
            wheelManager.currentWheel.OnSpinStart(() => { 
                gameStateMachine.ChangeState(gameStateMachine.spinningState); //change the state if spin button clicked, change state to -SPINNING STATE-
            });

            wheelManager.currentWheel.Spin(animationManager);
        });

        uiManager.exitButton.onClick.AddListener(() => { 
            
            gameManager.EndGameWin(); //if you decide to quit game while on ready to spin 
        });

        uiManager.restartButton.onClick.AddListener(() => { 
            
            gameManager.ReloadScene(); //if you win already, and want to play again
        });
    }

    public override void EnterState()
    {
        Debug.Log("Entered State: READY TO SPIN");

        uiManager.MakeGameQuitable(gameManager.currentZone); //check exit conditions
        
        uiManager.MakeSpinButtonInteractable(); //makes spin button interactable 

        animationManager.StartIdleRotationAnimation(wheelManager.currentWheel.wheelCircle); //idle animation for wheel
    }

    public override void ExitState()
    {
        Debug.Log("Exiting State: READY TO SPIN");  

        animationManager.StopIdleRotationAnimation(); //stops the idle anim
    }
}
