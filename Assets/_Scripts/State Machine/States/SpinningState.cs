using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningState : State
{
    public override void EnterState()
    {
        Debug.Log("Entered State: SPINNING");

        uiManager.exitButton.gameObject.SetActive(false); //deactivating the exit button, since the wheel is spinning rn
        
        uiManager.spinButton.interactable = false; //makes spin button uninteractable


        wheelManager.currentWheel.OnSpinEnd(WheelPiece => { //end of the spin

            Debug.Log(WheelPiece.reward.name + " reward picked from the wheel"); 

            gameStateMachine.pickedPiece = WheelPiece; //assign selected reward piece for future use

            gameStateMachine.ChangeState(gameStateMachine.cardRevealState); //change state to -CARD REVEAL STATE-

        });
    }

    
    public override void ExitState()
    {
        Debug.Log("Exiting State: SPINNING");
        
    } 
}
