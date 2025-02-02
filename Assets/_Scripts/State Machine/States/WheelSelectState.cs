using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSelectState : State
{
    

    public override void EnterState()
    {
        Debug.Log("Entered State: WHEEL SELECT");

        wheelManager.CloseCurrentWheel(); //if safe zone or super zone wheel is changing

        wheelManager.ActivateCurrentWheel(gameManager.currentZone); //activate the wheel appropriate wheel
        
        Debug.Log(wheelManager.currentWheel + " is selected");

        wheelManager.OpenCurrentWheel(); //activate the selected wheel object on the scene

        gameStateMachine.ChangeState(gameStateMachine.readyToSpinState);//change state to -READY TO SPIN STATE-

        
    }

    public override void ExitState()
    {
        Debug.Log("Exiting State: WHEEL SELECT");
    }
}
