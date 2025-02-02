#if UNITY_EDITOR

/// <summary>
/// This is for:
/// -assign rewards for wheels
/// -change rewards individual select probability
/// 
/// </summary>

using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;


[CustomEditor(typeof(WheelManager))]
public class WheelManagerEditor : Editor 
{
    private bool showBronzeWheelConfig = false;
    private bool showSilverWheelConfig = false;
    private bool showGoldWheelConfig = false;

    private List<WheelPiece> bronzeList;
    private List<WheelPiece> silverList;
    private List<WheelPiece> goldList;

    private void OnEnable() 
    {
        WheelManager wheelManager =  (WheelManager)target;
        
        bronzeList = wheelManager.bronzeWheelPieces.ToList<WheelPiece>();
        silverList = wheelManager.silverWheelPieces.ToList<WheelPiece>();
        goldList = wheelManager.goldWheelPieces.ToList<WheelPiece>();
  
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Spin Wheel Configurations", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        //dropdown configuration tables

        showBronzeWheelConfig = EditorGUILayout.Foldout(showBronzeWheelConfig, "Bronze Wheel Configuration", true, new GUIStyle(EditorStyles.foldout) { fixedWidth = 0, wordWrap = true }); 
        if (showBronzeWheelConfig) DrawConfiguration(bronzeList);
        EditorGUILayout.Space(30);

        showSilverWheelConfig = EditorGUILayout.Foldout(showSilverWheelConfig, "Silver Wheel Configuration", true, new GUIStyle(EditorStyles.foldout) { fixedWidth = 0, wordWrap = true });
        if (showSilverWheelConfig) DrawConfiguration(silverList);
        EditorGUILayout.Space(30);

        showGoldWheelConfig = EditorGUILayout.Foldout(showGoldWheelConfig, "Gold Wheel Configuration", true, new GUIStyle(EditorStyles.foldout) { fixedWidth = 0, wordWrap = true });
        if (showGoldWheelConfig) DrawConfiguration(goldList);
        EditorGUILayout.Space(30);
        
        
        if (GUI.changed)//save
        {
            EditorUtility.SetDirty(target);
        }
    }
    
    private void DrawConfiguration(List<WheelPiece> wplist)//generating config table
    {
        
        int accumulatedWeight = 0;

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Pieces Weights", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

       
        foreach (var piece in wplist)
        {
            float c = EditorGUILayout.Slider(piece.reward.rewardName + " " + piece.reward.amount + " Weight", piece.chance, 0f, 100f);
            piece.chance = (int)c;
            accumulatedWeight = CalculateWeights(wplist);
        }

        EditorGUILayout.Space();

        foreach (var piece in wplist)
        {
            float percentage = piece.chance / (float)accumulatedWeight * 100;

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField(piece.reward.rewardName + " " + piece.reward.amount + " -> %" + (int)percentage);
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndVertical();
    }

    private int CalculateWeights(List<WheelPiece> wplist) //we sum up all wheel piece chances to calculate a weight
    {
        int weight = 0;
        
        foreach(WheelPiece p in wplist)
            weight += p.chance;
            
        return weight;
    }
}

#endif