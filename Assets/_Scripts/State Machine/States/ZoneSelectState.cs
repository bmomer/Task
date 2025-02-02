using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSelectState : State
{
    public override void EnterState()
    {
        Debug.Log("Entered State: ZONE SELECT");

        gameManager.IncrementZone(); //incerementing current zone

        gameStateMachine.ChangeState(gameStateMachine.wheelSelectState);//change state to -WHEEL SELECT STATE-
    }

    public override void ExitState()
    {
        Debug.Log("Exiting State: ZONE SELECT");

        uiManager.UpdateZoneUI(gameManager.currentZone); //updates the zone panel

        uiManager.UpdateUpcomingZoneTexts(gameManager.currentZone); 

    }
}
