using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    [Space]
    [Header("States")]
    public ZoneSelectState zoneSelectState;
    public WheelSelectState wheelSelectState;
    public ReadyToSpinState readyToSpinState;
    public SpinningState spinningState;
    public CardRevealState cardRevealState;
    public EndGameState endGameState;
    

    private State currentState;

    //common variables between states
    [HideInInspector] public WheelPiece pickedPiece; //it's getting assigned in SpinningState for other states to use
    GameManager gameManager;
    
    public void SetManager(GameManager gm)
    {
        gameManager = gm;
        SetupStates();
    }

    private void SetupStates()
    {
        UIManager uiManager = gameManager.uiManager;
        WheelManager wheelManager = gameManager.wheelManager;
        AnimationManager animationManager = gameManager.animationManager;

        zoneSelectState.Setup(this, gameManager, uiManager, wheelManager,animationManager);
        wheelSelectState.Setup(this, gameManager, uiManager, wheelManager,animationManager);
        readyToSpinState.Setup(this, gameManager, uiManager, wheelManager,animationManager);
        spinningState.Setup(this, gameManager, uiManager, wheelManager,animationManager);
        cardRevealState.Setup(this, gameManager, uiManager, wheelManager,animationManager);
        endGameState.Setup(this, gameManager, uiManager, wheelManager,animationManager);
    }

    public void ChangeState(State newState)
    {
        if(currentState != null)
        {
            currentState.ExitState();
            //currentState.enabled = false;
        }

        currentState = newState;
        currentState.enabled = true;
        currentState.EnterState();
    }

    private void Update()
    {
        if(currentState != null)
        {
            currentState.UpdateState();
        }
    }
}
