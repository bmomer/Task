#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor 
{
    private void OnValidate() {
        Debug.Log("validating ui elements");
        UIManager uiManager = (UIManager)target;
        AssignUI(uiManager); //all buttons getting assigned from here
    }

    private void AssignUI(UIManager uiManager) {
        
        uiManager.goldText = GameObject.Find("ui_panel_loseGame_gold_text")?.GetComponent<TextMeshProUGUI>();
        if (uiManager.goldText == null) Debug.LogWarning("gold text  couldn't be found");

        uiManager.safeZoneText = GameObject.Find("ui_panel_upcoming_zone_display_safeZone_image_number_text_value")?.GetComponent<TextMeshProUGUI>();
        if (uiManager.safeZoneText == null) Debug.LogWarning("safe zone text  couldn't be found");

        uiManager.superZoneText = GameObject.Find("ui_panel_upcoming_zone_display_superZone_image_number_text_value")?.GetComponent<TextMeshProUGUI>();
        if (uiManager.superZoneText == null) Debug.LogWarning("super zone text  couldn't be found");

        uiManager.spinButton = GameObject.Find("ui_wheels_spin_button")?.GetComponent<Button>();
        if (uiManager.spinButton == null) Debug.LogWarning("spin button coludn't be found");

        uiManager.exitButton = GameObject.Find("ui_panel_rewards_exit_button")?.GetComponent<Button>();
        if (uiManager.exitButton == null) Debug.LogWarning("exit button couldn't be found");

        uiManager.reviveButton = GameObject.Find("ui_panel_loseGame_revive_button")?.GetComponent<Button>();
        if (uiManager.reviveButton == null) Debug.LogWarning("revive button couldn't be found");

        uiManager.giveUpButton = GameObject.Find("ui_panel_loseGame_giveup_button")?.GetComponent<Button>();
        if (uiManager.giveUpButton == null) Debug.LogWarning("give up button couldn't be found");

        uiManager.restartButton = GameObject.Find("ui_panel_winGame_restart_button")?.GetComponent<Button>();
        if (uiManager.restartButton == null) Debug.LogWarning("restart button couldn't be found");
    }
}

#endif