using System;
using UnityEngine ;


[System.Serializable]
public class WheelPiece
{   
   public WheelRewardSO reward;  // ScriptableObject for reward data
   [HideInInspector][Range(0, 100)] public int chance;  // Probability weight
   [HideInInspector] public double _weight;
   [HideInInspector] public int index;

   
}

