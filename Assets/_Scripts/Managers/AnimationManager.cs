using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationManager : MonoBehaviour
{
    [Header ("Wheel Spin Animation Parameters :")]
    [Range(1, 20)] public int spinDuration = 8;

    [Header("Idle Animation Parameters")]
    [SerializeField] private float idleRotationSpeed = 1f;

    [Space]
    [Header("Card Reveal Animation Parameters")]
    [SerializeField] private float duration = 1.5f;    
    [SerializeField] private Vector3 fullScale = new Vector3(1, 1, 1); // Final size
    [SerializeField] private Vector3 rotationAmount = new Vector3(0, 0, 360); 

    [Space]
    [Header("Card Icon Animation Parameters")]
    [SerializeField] Transform tempTargetForIcon;
    [SerializeField] private float moveDuration = 1f;

    [Space]
    [Header("Card Disappear Animation Parameters")]
    [SerializeField] Transform disappearTarget;
    [SerializeField] private float disappearDuration = 1f;

    //Wheel Animations

    private Tween tweenTemp;
    public void StartIdleRotationAnimation(Transform wheelCircle)
    {
        Debug.Log("starting idle rotation anim for current wheel");

        tweenTemp = wheelCircle.DORotate(new Vector3(0, 0, -360), 36f / idleRotationSpeed, RotateMode.FastBeyond360)
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Restart); 
    }

    public void StopIdleRotationAnimation() 
    {
        if (tweenTemp != null)
        {
            Debug.Log("stoping idle rotation anim for current wheel");

            tweenTemp.Kill(); 
        }
    }

    public void SpinWheelAnimation(Wheel wheel, float targetRotation, WheelPiece piece, float pieceAngle, Action onComplete = null) 
    {
        Vector3 targetRotVec = Vector3.back * targetRotation;
        float prevAngle = wheel.wheelCircle.eulerAngles.z;
        float currentAngle = prevAngle;
        bool isIndicatorOnTheLine = false;
        
        wheel.wheelCircle.DORotate(targetRotVec, spinDuration, RotateMode.FastBeyond360)
        .SetEase(Ease.InOutQuart)
        .OnUpdate(() => {
        float diff = Mathf.Abs(prevAngle - currentAngle);
        if (diff >= pieceAngle / 2f)
        {
            if (isIndicatorOnTheLine) 
            {
                //tick sound hereee
            }
            prevAngle = currentAngle;
            isIndicatorOnTheLine = !isIndicatorOnTheLine;
        }
        currentAngle = wheel.wheelCircle.eulerAngles.z;

        })

        .OnComplete(() => {
            onComplete?.Invoke();
        });
    }

    //Card animations
    public void CardRevealAnimation(CardUI cu, Action onComplete = null) //revealing the card with scaling-up and rotating at the same time
    {
        Transform cardTransform = cu.transform;
        cardTransform.localScale = Vector3.zero; 
        cardTransform.localEulerAngles = Vector3.zero; 

        Sequence cardSequence = DOTween.Sequence(); 

        cardSequence.Append(cardTransform.DOScale(fullScale, duration).SetEase(Ease.OutBack));
        cardSequence.Join(cardTransform.DORotate(rotationAmount, duration, RotateMode.FastBeyond360).SetEase(Ease.OutQuad));

        cardSequence.OnComplete(() => { //do this when anim finished
            Debug.Log("card reveal animation finishd");
            
            onComplete?.Invoke();
        }); 
    }

    public void MoveRewardIconToPanelAnimation(CardUI cu, Transform target, Action onComplete = null) //moves the revealed card's icon to the rewards panel on the left
    {
        if(target == null)
            target = tempTargetForIcon;
        RectTransform rt = cu.icon.rectTransform;

        Sequence iconSequence = DOTween.Sequence();

        iconSequence.Append(rt.DOMove(target.position, moveDuration).SetEase(Ease.InOutQuad)); 
        iconSequence.Join(rt.DOScale(1.2f, moveDuration / 2).SetEase(Ease.OutQuad))  
                    .Append(rt.DOScale(Vector3.zero, moveDuration / 2).SetEase(Ease.InOutQuad)); 

        
        iconSequence.OnComplete(() => { //do this when anim finished

            Debug.Log("icon move animation finished");
            onComplete?.Invoke();

        });
    }

    public void HideCardAnimation(CardUI cu, Action onComplete = null) //scale down the card for disappear effect
    {
        Transform cardTransform = cu.transform;
        Sequence disappearSequence = DOTween.Sequence();

        
        disappearSequence.Append(cardTransform.DOScale(Vector3.zero, disappearDuration).SetEase(Ease.InBack));
        disappearSequence.Join(cardTransform.DOMove(disappearTarget.position, disappearDuration).SetEase(Ease.InOutQuad));

        
        disappearSequence.OnComplete(() => {
            onComplete?.Invoke();
        });
    }

    private void OnDestroy()
    {
        if(tweenTemp != null) //idle spin anim, we kill this anim for clean scene reload
            tweenTemp.Kill();  
    }
}
