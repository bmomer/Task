using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class ZoneIndicatorUI : MonoBehaviour
{
    [SerializeField] private int currentZone = 1;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] RectTransform contentPanel;
    [SerializeField] RectTransform sampleListItem;
    [SerializeField] HorizontalLayoutGroup hlg;

    public void SetZoneIndicator(int zoneStartNumber, int zoneAmount) //creates zone numbers (for top scroll view)
    {
        for(int i = zoneStartNumber; i <= zoneAmount ; i++)
        {
            GameObject go = Instantiate(sampleListItem.gameObject, contentPanel); 
            TMP_Text zoneText = go.transform.GetChild(0).GetComponent<TMP_Text>(); 
            zoneText.text = i.ToString();

            if(i % 30 == 0) //super zone
            {
                zoneText.color = Color.yellow;
            }

            else if(i % 5 == 0)//safe zone
            {
                zoneText.color = Color.green;
            }

        }
    }

    public void UpdateCurrentZonePosition(int newZone) //updates the current zone position for top scroll view
    {
        currentZone = newZone;
        float targetX = 0 - ((currentZone - 1) * (sampleListItem.rect.width + hlg.spacing));
        contentPanel.DOLocalMoveX(targetX, 0.5f).SetEase(Ease.OutQuad);
    }

}
