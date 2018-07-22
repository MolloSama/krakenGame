using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFile : MonoBehaviour {
    public SaveControl saveControl;
	// Use this for initialization
	void Start () {
		
	}

    private void OnMouseDown()
    {
        LoadGameData(saveControl.saveNumberReflect[MoveBorder.currentIndex]);
        string[] sceneNumber = GlobalVariable.currentScene.Split('-');
        TertiaryMapSelect.SetScene(sceneNumber[0] + "-" + sceneNumber[1]);
        SceneManager.LoadScene("tertiaryMap");
    }

    void LoadGameData(SaveModel save)
    {
        GlobalVariable.ExistingCards = save.ExistCards;
        GlobalVariable.FightCards = save.FightCards;
        GlobalVariable.ExistingLeadSkills = save.ExistSkill;
        GlobalVariable.FightSkills = save.FightSkill;
        GlobalVariable.BattleItems = save.BattleItems;
        GlobalVariable.PlotItems = save.PlotItems;
        GlobalVariable.kraKen = save.Kraken;
        GlobalVariable.HasFightAreaBoss = save.HasFightAreaBoss;
        GlobalVariable.HasFightBossScenes = save.HasFightBossScenes;
        GlobalVariable.HasFightScenes = save.HasFightScenes;
        GlobalVariable.currentScene = save.CurrentScenes;
    }
}
