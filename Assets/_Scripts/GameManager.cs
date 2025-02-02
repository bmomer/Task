using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public UIManager uiManager;
    public GameStateMachine gameStateMachine;
    public WheelManager wheelManager;
    public AnimationManager animationManager;
    public GameDataSO gameData;
    
    [Space]
    [Header("Game Configuration")]
    public int targetFrameRate = 60;
    public int zoneStartValue = 1;
    public int totalZoneAmount = 60;
    [HideInInspector] public int currentZone;

 
    private void Awake()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        Debug.Log("Initializing Fortune Wheel Mini-game");

        gameStateMachine.SetManager(this); //send reference to state machine, state machine will setup the State instances
        uiManager.SetUI(this); //setup the ui
        gameStateMachine.ChangeState(gameStateMachine.zoneSelectState);//initial state that starts the game
        gameData.Load();//load player data

        Application.targetFrameRate = targetFrameRate;
    }

    public void EndGameWin()
    {
        RewardsToInventory(); //take rewards to play inventory, increase 
        uiManager.winGamePanel.SetActive(true); //open win game panel
        uiManager.ShowRewards(); //show the acquired rewards
    }

    public void IncrementZone()
    {
        currentZone++;

        if(currentZone == 61) EndGameWin(); //finished the whole game 
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Revive()
    {
        int goldAmount = gameData.playerInventoryItems[3]; //gold from player inventory (gold item id is 3)
        if(goldAmount >= 1000)
        {
            uiManager.loseGamePanel.SetActive(false); //close the lose game panel
            gameData.playerInventoryItems[3] -= 1000; // update the player inventory gold amount
            uiManager.goldText.text = gameData.playerInventoryItems[3].ToString(); //update the gold text    
            gameData.Save();
            gameStateMachine.ChangeState(gameStateMachine.zoneSelectState); //CHANGE STATE TO -ZONE SELECT-
        }
    }

    private void RewardsToInventory()
    {
        List<RewardUI> rewList = uiManager.rewardDisplay.rewardDisplayDictionary.Values.ToList(); //gained rewards list

        foreach(RewardUI r in rewList)
            gameData.playerInventoryItems[r.itemID_UI] += r.currentAmount;  //move rewards to player inventory with item id's

        gameData.Save();    
    }   
}
