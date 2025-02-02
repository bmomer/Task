using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public ZoneIndicatorUI zoneIndicatorUI;
    public RewardDisplayUI rewardDisplay;
    public GameObject loseGamePanel;
    public GameObject winGamePanel;
    


    public TMP_Text goldText; //these asigned via editor code
    public TMP_Text safeZoneText;
    public TMP_Text superZoneText;
    public Button spinButton;
    public Button exitButton; 
    public Button giveUpButton;
    public Button reviveButton;
    public Button restartButton;


    public void SetUI(GameManager gm)
    {
        Debug.Log("Setting UI Elements");

        zoneIndicatorUI.SetZoneIndicator(gm.zoneStartValue, gm.totalZoneAmount);
        goldText.text = gm.gameData.playerInventoryItems[3].ToString(); //since the item id of gold is 3 we update with it
    }

    public void ShowRewards()
    {
        List<RewardUI> rewList = rewardDisplay.rewardDisplayDictionary.Values.ToList();
        if(rewList.Count > 0)
        {
            SetRewardsToContainer(rewList); //move rewards to win game screen
        } 
    }

    private void SetRewardsToContainer(List<RewardUI> rewardList)
    {
        Transform winGamePanelContainer = winGamePanel.GetComponent<Transform>().GetChild(0).transform; //win game panel reward container
        foreach(RewardUI r in  rewardList)
            r.transform.SetParent(winGamePanelContainer);
    }

    public void UpdateUpcomingZoneTexts(int zone)//these calculations made for 60 levels
    {
        //no upcoming safe zone ahead
        if(zone >= 55)
            safeZoneText.text = "";

        //safe zone but there is already super zone ahead
        else if(zone == 25) 
            safeZoneText.text = (zone + 10).ToString();

        //safe zone
        else if(zone % 5 == 0)
            safeZoneText.text = (zone + 5).ToString();

        //super zone
        if(zone == 30)    
            superZoneText.text = (zone + 30).ToString();
    }

    public void UpdateZoneUI(int zone)
    {
        zoneIndicatorUI.UpdateCurrentZonePosition(zone); //updates the zone indicator at the top of the screen
    }

    public void MakeGameQuitable(int zone)
    {
        if(zone % 30 == 0 || zone % 5 == 0 || zone == 1) //forfeit the run conditions (quitting possible only at start, safe or super zone)
            exitButton.gameObject.SetActive(true);
    }

    public void MakeSpinButtonInteractable()
    {
        Debug.Log("Spin Button interactable");
        spinButton.interactable = true;
    }

    public bool IsItemAlreadyOnRewardsPanel(WheelPiece p)
    {
        return rewardDisplay.rewardDisplayDictionary.ContainsKey(p.reward.itemID);
    }
}
