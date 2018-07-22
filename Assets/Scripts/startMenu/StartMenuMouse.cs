using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuMouse : MonoBehaviour {
    private float baseScaleX = 0.4f;
    private float baseScaleY = 0.3f;
    private float baseScaleZ = 1.0f;
    private float changeScaleX = 0.44f;
    private float changeScaleY = 0.33f;
    private float changeScaleZ = 1.00f;

    // Use this for initialization
    void Start () {
        baseScaleX = GameObject.Find("button1").transform.localScale.x;
        baseScaleY = GameObject.Find("button1").transform.localScale.y;
        baseScaleZ = GameObject.Find("button1").transform.localScale.z;
        changeScaleX = baseScaleX * 1.2f;
        changeScaleY = baseScaleY * 1.2f;
        changeScaleZ = baseScaleZ * 1.2f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Mouse click
    public void OnMouseUpAsButton()
    {
        if (name.Equals("button1"))
        {
            LoadConversation.SetConversation("0-0-1", 0, "conversation");
            SceneManager.LoadScene("conversation");
        }
        if (name.Equals("button2"))
        {
            ReturnPre.preScene = "startMenu";
            SceneManager.LoadScene("accessProgress");
        }
        if (name.Equals("button3"))
        {

        }
    }

    //Mouse enter
    public void OnMouseEnter()
    {
        transform.localScale = new Vector3(changeScaleX, changeScaleY, changeScaleZ);
    }

    //Mouse exit
    public void OnMouseExit()
    {
        transform.localScale = new Vector3(baseScaleX, baseScaleY, baseScaleZ);
    }
}
