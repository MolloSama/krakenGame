using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondarySelect : MonoBehaviour {

    private float baseScaleX;

    private float baseScaleY;

    private float baseScaleZ = 1.0f;

    private float changeScaleX;

    private float changeScaleY;

    private float changeScaleZ = 1.00f;

    private static string sceneName;

    public static bool setOver = false;

    // Use this for initialization
    void Start () {
        setOver = false;
        baseScaleX = gameObject.transform.localScale.x;
        baseScaleY = gameObject.transform.localScale.y;
        baseScaleZ = gameObject.transform.localScale.z;
        changeScaleX = baseScaleX * 1.2f;
        changeScaleY = baseScaleY * 1.2f;
        changeScaleZ = baseScaleZ;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void SetScene(string temp)
    {
        sceneName = temp;
        setOver = true;
    }

    public void OnMouseUpAsButton()
    {
        if (sceneName == null)
        {
            sceneName = GlobalVariable.preMap.Split('-')[0];
        }
        TertiaryMapSelect.SetScene(sceneName + '-' + gameObject.name);
        while (true)
        {
            if (!TertiaryMapSelect.setOver)
                continue;
            else
            {
                SceneManager.LoadScene("tertiaryMap");
                break;
            }
        }
    }
    public void OnMouseEnter()
    {
        transform.localScale = new Vector3(changeScaleX, changeScaleY, changeScaleZ);
    }
    public void OnMouseExit()
    {
        transform.localScale = new Vector3(baseScaleX, baseScaleY, baseScaleZ);
    }
}
