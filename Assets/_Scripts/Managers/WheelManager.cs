using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelManager : MonoBehaviour
{
    [HideInInspector] public Wheel currentWheel;

    //these all set from editor code (Wheel Manager)
    public Wheel bronzeWheel;
    public Wheel silverWheel;
    public Wheel goldWheel;

    public WheelPiece[] bronzeWheelPieces; 
    public WheelPiece[] silverWheelPieces; 
    public WheelPiece[] goldWheelPieces; 

    //
    
    void Start() //setting rewards for wheels
    {
        bronzeWheel.SetWheel(bronzeWheelPieces);
        silverWheel.SetWheel(silverWheelPieces);
        goldWheel.SetWheel(goldWheelPieces);
    }

    public void ActivateCurrentWheel(int zone) 
    {
        Wheel wheel;
        if(zone % 30 == 0) wheel = goldWheel;
        else if(zone % 5 == 0) wheel = silverWheel;
        else wheel = bronzeWheel;

        if(currentWheel != null && currentWheel != wheel)
             wheel.wheelCircle.localRotation = currentWheel.wheelCircle.localRotation; //inherit the last wheel rotation for preventing bad looking wheel transitions 

        currentWheel = wheel;
    }

    public void CloseCurrentWheel()
    {
        if(currentWheel != null)
            currentWheel.gameObject.SetActive(false);
    }

    public void OpenCurrentWheel()
    {
        currentWheel.gameObject.SetActive(true);
    }    
}
