using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMapMouse : MonoBehaviour {
    private float baseScaleX;
    private float baseScaleY;
    private float baseScaleZ = 1.0f;
    private float changeScaleX;
    private float changeScaleY;
    private float changeScaleZ = 1.00f;
    // Use this for initialization
    void Start () {
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

    public void OnMouseUpAsButton()
    {
        SecondarySelect.SetScene(gameObject.name);
        while (true)
        {
            if (!SecondarySelect.setOver)
                continue;
            else
            {
                SceneManager.LoadScene("secondaryMap");
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
public class MountainInformation
{
    public string serialnumber;
    public string name;
    public float x;
    public float y;
    public bool status;
    public int index;
    public MountainInformation(string a, string b, float c, float d, bool e, int f)
    {
        serialnumber = a;
        name = b;
        x = c;
        y = d;
        status = e;
        index = f;
    }
}
