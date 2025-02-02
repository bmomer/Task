using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections.Generic;
using TMPro;


public class Wheel : MonoBehaviour 
{
    public  Transform wheelCircle;
    
    [Header ("Wheel Spin Settings :")]
    [Range(1, 20)] public int spinDuration = 8;
    [SerializeField] [Range(.2f, 2f)] private float wheelSize = 1f;

    [Space]
    [Header("Wheel Parts")]
    [SerializeField] private Transform PickerWheelTransform;
    [SerializeField] private GameObject wheelPiecePrefab;
    [SerializeField] private Transform wheelPiecesParent;
    [SerializeField] private Transform[] piecePositions;

    
    
    private WheelPiece[] wheelPieces; //comes from Wheel Manager
   
    private UnityAction onSpinStartEvent;
    private UnityAction<WheelPiece> onSpinEndEvent;

    private bool _isSpinning = false;
    public bool IsSpinning => _isSpinning; //public getter for spinning status if needed

    private float pieceAngle;
    private float halfPieceAngleWithPaddings;
    private double accumulatedWeight;
    private System.Random rand = new System.Random(); //random generator for weighted selection
    private List<int> nonZeroChancesIndices = new List<int>();

    
    public void SetWheel(WheelPiece[] wp) 
    {
        wheelPieces = wp;//setting the array of rewards from Wheel Manager

        //calculate the angle for pieces
        pieceAngle = 360 / wheelPieces.Length;
        halfPieceAngleWithPaddings = (pieceAngle / 2f) - (pieceAngle / 4f);

        GenerateWheel(); //place pieces on the wheel
        CalculateWeights(); //calculating the weights for random selection
    }

    private void GenerateWheel() 
    { 
        for (int i = 0; i < wheelPieces.Length; i++) 
            DrawPiece(i);
         
    }

    private void DrawPiece(int index) //creates and assigns the rewards for the wheel
    {
        if (index >= piecePositions.Length) 
        {
            Debug.LogError("Missing piece position reference for index");
            return;
        }

        WheelPiece piece = wheelPieces[index];
        Transform pieceTrns = InstantiatePiece(index).transform.GetChild(0);
        pieceTrns.GetChild(0).GetComponent<Image>().sprite = piece.reward.icon;

        if (piece.reward.amount != 0)//not necessary if all reward scriptable objects have amount more than 0
            pieceTrns.GetChild(2).GetComponent<TextMeshProUGUI>().text = "x" + piece.reward.amount.ToString();

        pieceTrns.position = piecePositions[index].position;
        pieceTrns.rotation = piecePositions[index].rotation;
    }

    private GameObject InstantiatePiece(int index)
    {
        return Instantiate(wheelPiecePrefab, piecePositions[index]);
    }

    public void Spin(AnimationManager animationManager) 
    {
        if (_isSpinning) return;

        _isSpinning = true;
        onSpinStartEvent?.Invoke();

        int index = GetRandomPieceIndex();
        WheelPiece piece = wheelPieces[index];

        if (piece.chance == 0 && nonZeroChancesIndices.Count != 0) 
        {
            index = nonZeroChancesIndices[Random.Range(0, nonZeroChancesIndices.Count)];
            piece = wheelPieces[index];
        }

        float targetAngle = CalculateTargetRotation(index);
        StartSpin(animationManager, targetAngle, piece);
    }

    private void StartSpin(AnimationManager animationManager ,float targetRotation, WheelPiece piece) 
    {
        animationManager.SpinWheelAnimation(this, targetRotation, piece, spinDuration, () => {//delegate for resetting the events after animation
            
            _isSpinning = false;
            onSpinEndEvent?.Invoke(piece);
            onSpinStartEvent = null;
            onSpinEndEvent = null;
        });
    }

    private float CalculateTargetRotation(int index) //calculates the wheel's stop point
    {
        float angle = -(pieceAngle * index);
        float leftOffset = (angle + halfPieceAngleWithPaddings) % 360;
        float rightOffset = (angle - halfPieceAngleWithPaddings) % 360;
        return Random.Range(leftOffset, rightOffset) + 2 * 360 * spinDuration; //adding more full rotations since it is not affecting the result
    }

    public void OnSpinStart(UnityAction action)
    {
        onSpinStartEvent = action;
    }

    public void OnSpinEnd(UnityAction<WheelPiece> action)
    {
        onSpinEndEvent = action;
    }

    private int GetRandomPieceIndex() 
    {
        double r = rand.NextDouble() * accumulatedWeight; 
        for (int i = 0; i < wheelPieces.Length; i++)
            if (wheelPieces[i]._weight >= r)
                return i;

        return 0;
    }

    private void CalculateWeights() //we sum up all wheel piece chances to calculate a weight
    {
        accumulatedWeight = 0;
        nonZeroChancesIndices.Clear();

        for (int i = 0; i < wheelPieces.Length; i++) 
        {
            WheelPiece piece = wheelPieces[i];
            accumulatedWeight += piece.chance;
            piece._weight = accumulatedWeight;
            piece.index = i;
            if (piece.chance > 0) nonZeroChancesIndices.Add(i);// selectable pieces for wheel, adding only pieces that have a chance of bigger than 0
        }
    }

    private void OnValidate() 
    {
        if (PickerWheelTransform != null)
        PickerWheelTransform.localScale = new Vector3(wheelSize, wheelSize, 1f);
    }
}

