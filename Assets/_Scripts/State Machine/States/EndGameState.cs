using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameState : State
{

    void Start()
    {
        uiManager.giveUpButton.onClick.AddListener(() => { //give up
            gameManager.ReloadScene();
        });

        uiManager.reviveButton.onClick.AddListener(() => { //revive
            gameManager.Revive();
        });

    }

    public override void EnterState()
    {
        Debug.Log("Entered State: END GAME");

        uiManager.loseGamePanel.SetActive(true);

    }

    public override void UpdateState()
    {
        //audio implementation maybe
    }

    public override void ExitState()
    {
        Debug.Log("Exiting State: END GAME");
        
    }
}
