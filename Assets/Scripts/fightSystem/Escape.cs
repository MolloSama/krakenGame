﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour {

    bool isEscaped = false;
    public int demarcationline;
    // Use this for initialization
    void Start () {
        demarcationline = 50;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    private void OnMouseDown()
    {
        if (!isEscaped)
        {
            if (GlobalVariable.Chance(demarcationline))
            {
                SceneManager.LoadScene("settlement");
            }
            else
            {
                Debug.Log("逃跑失败");
            }
        }
    }
}
