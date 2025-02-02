using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected GameStateMachine gameStateMachine;
    protected GameManager gameManager;
    protected UIManager uiManager;
    protected WheelManager wheelManager;
    protected AnimationManager animationManager;

    public void Setup(GameStateMachine _gameStateMachine, GameManager _gameManager, UIManager _uiManager, WheelManager _wheelManager, AnimationManager _animationManager)
    {
        gameStateMachine = _gameStateMachine;
        gameManager = _gameManager;
        uiManager = _uiManager;
        wheelManager = _wheelManager;
        animationManager = _animationManager;
    }

    public virtual void EnterState(){}
    public virtual void UpdateState(){}
    public virtual void ExitState(){}
}
