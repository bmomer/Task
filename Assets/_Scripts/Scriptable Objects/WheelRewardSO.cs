using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Reward", menuName = "Add Reward/Reward", order = 1)]
public class WheelRewardSO : ScriptableObject {

    public int itemID;
    public string rewardName;        
    public Sprite icon;              
    public int amount;         

    public bool isSpecialReward;   //these bools are important because there is different type of card prefabs  
    public bool isBomb;
    public bool isGold;
    
}
