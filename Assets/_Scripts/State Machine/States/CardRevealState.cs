using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardRevealState : State
{
    [Header("Prefabs")]
    [SerializeField] GameObject bombCardPrefab;
    [SerializeField] GameObject goldCardPrefab;
    [SerializeField] GameObject specialCardPrefab;
    [SerializeField] GameObject commonCardPrefab;

    [SerializeField] Transform cardInstantiatePoint;
    private WheelPiece piece;

    public override void EnterState()
    {
        Debug.Log("Entered State: CARD REVEAL");
        piece = gameStateMachine.pickedPiece;

        CardUI cardUI = InstantiateCard();

        Transform rewardOnPanelTransform = null;

        if(uiManager.IsItemAlreadyOnRewardsPanel(piece)) //check if the acquired reward is already on the rewards panel, needed for the animation destination
            rewardOnPanelTransform = uiManager.rewardDisplay.rewardDisplayDictionary[piece.reward.itemID].transform;

        cardUI.SetCard(piece); //set card variables

        animationManager.CardRevealAnimation(cardUI, () => { //animation sequence
            
            if(!piece.reward.isBomb) //moving to reward pannel (only for reward cards)
            {
                animationManager.MoveRewardIconToPanelAnimation(cardUI, rewardOnPanelTransform, () => { 
                    animationManager.HideCardAnimation(cardUI, () => {

                        Destroy(cardUI.gameObject); //destroy card after all animations
                        gameStateMachine.ChangeState(gameStateMachine.zoneSelectState); //change state to -ZONE SELECT STATE- (restarts the game flow in a way)
                        
                    });
                });
            }

            else //if bomb is picked
            {
                Destroy(cardUI.gameObject);//destroy bomb card from screen
                gameStateMachine.ChangeState(gameStateMachine.endGameState); //change state to -ENDGAME STATE-
                //explosion anim here maybe
            }
            
        });
    }


    public override void ExitState()
    {
        Debug.Log("Exiting State: CARD REVEAL");
        if(!piece.reward.isBomb)
            uiManager.rewardDisplay.AddReward(piece.reward);
    }

    //

    
    private CardUI InstantiateCard() 
    {
        GameObject go = Instantiate(GetCardPrefab(), cardInstantiatePoint);
        CardUI card = go.GetComponent<CardUI>();
        return card;
    }

    private GameObject GetCardPrefab()
    {
        if(piece.reward.isBomb) return bombCardPrefab;
        else if(piece.reward.isGold) return goldCardPrefab;
        else if(piece.reward.isSpecialReward)return specialCardPrefab;
        return commonCardPrefab;
    }
}
