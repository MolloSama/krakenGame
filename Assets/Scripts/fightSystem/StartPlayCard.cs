using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlayCard : MonoBehaviour {

    public GameControll gameControll;
	// Use this for initialization
	void Start () {
		
	}

    private void OnMouseDown()
    {
        if (gameControll.PlayCard())
        {
            gameControll.ClickSelectedMonster();
            RenderMonster.needClickMonster = false;
            CardAction.currentIndex = -1;
        }
    }
}
