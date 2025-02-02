using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//pieces for reward panel on the left of the screen

public class RewardUI : MonoBehaviour
{
    public int itemID_UI;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountText;
    public int currentAmount;

    public void SetReward(WheelRewardSO newReward)
    {
        if (itemID_UI != newReward.itemID)
        {
            itemID_UI = newReward.itemID;
            icon.sprite = newReward.icon;
            currentAmount = newReward.amount;
        }
        else
        {
            // Increase the current amount if it's the same reward.
            currentAmount += newReward.amount;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        amountText.text = "x" + currentAmount.ToString();
        
    }
    
}
