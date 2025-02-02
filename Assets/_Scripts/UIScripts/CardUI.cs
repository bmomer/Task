using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//cards that appear on screen with animation

public class CardUI : MonoBehaviour
{
    public Image icon; //public because needed for animation with dotween
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI amountText;

    

    public void SetCard(WheelPiece wheelPiece)//setting the card variables
    {
        if(!wheelPiece.reward.isBomb)
        {
            icon.sprite = wheelPiece.reward.icon;
            titleText.text = wheelPiece.reward.rewardName;
            amountText.text = "x" + wheelPiece.reward.amount.ToString();
        }
        
    }
}
