using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardDisplayUI : MonoBehaviour
{
    [SerializeField] private Transform rewardsPanel;   
    [SerializeField] private GameObject rewardUIPrefab; 
    
    public Dictionary<int, RewardUI> rewardDisplayDictionary = new Dictionary<int, RewardUI>(); //via item id's we send values of the items with this dictionary (amount values)

    public void AddReward(WheelRewardSO reward)
    {
        //if reward already exists on the reward panel only update it's amount
        if(rewardDisplayDictionary.ContainsKey(reward.itemID)) 
            rewardDisplayDictionary[reward.itemID].SetReward(reward);
        
        else
        {
            //create new reward for panel, add to the dictionary since all items have unique id's
            GameObject go = Instantiate(rewardUIPrefab, rewardsPanel);
            RewardUI rewardUI = go.GetComponent<RewardUI>();
            rewardUI.SetReward(reward);
            rewardDisplayDictionary.Add(reward.itemID, rewardUI);
        }
    }
}
