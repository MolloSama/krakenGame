﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour {

    public static CardSelect _instance;

    public GameProp gameProp = null;

    private static float positionX = -3.29f;

    private static float positionY = 3.678f;
    
    private static float positionZ = 0.5f;

    private static float grapY = -0.714f;

    //全部出战卡牌
    public static GameProp[] fightCardsGrids = new GameProp[GlobalVariable.MAX_NUMBER_OF_FIGHT_CARDS];

    //展示的出战卡牌
    public static GameObject[] fightCardsOnShow = new GameObject[10];

    //记录出战卡牌数量
    public static int count = 0;

    public static int firstindex = 0;

    public static int pagenum=10;

    public static int onShowNum = 0;

    private static bool clickable = true;

    public static void Clear()
    {
        count = 0;
        clickable = true;
        onShowNum = 0;
        firstindex = 0;
        for (int i = 0; i < fightCardsGrids.Length; i++)
        {
            fightCardsGrids[i] = null;
        }
        for (int i = 0; i < fightCardsOnShow.Length; i++)
        {
            fightCardsOnShow[i] = null;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start () {

	}

    public void LoadCard(GameProp temp)
    {
        gameProp = temp;
    }

    private void OnMouseUpAsButton()
    {
        bool contains = false;
        if (count < GlobalVariable.MAX_NUMBER_OF_FIGHT_CARDS)
        {
            for (int i = 0; i < count; i++)
                if (fightCardsGrids[i].SerialNumber.Equals(gameProp.SerialNumber))
                {
                    contains = true;
                    break;
                }
            if (!contains && clickable && BarScript.clickable) 
            {
                gameObject.transform.Find("card-style").GetComponent<SpriteRenderer>().material = Resources.Load<Material>("materials/SpriteGray");
                gameObject.transform.Find("card-raw-img").GetComponent<SpriteRenderer>().material = Resources.Load<Material>("materials/SpriteGray");
                StartCoroutine(AlterFithtGrids(true, count));
                count++;
            }
        }
    }
    public void CallAlterFithtGrids(bool add,int index)
    {
        if(clickable&&BarScript.clickable)
            StartCoroutine(AlterFithtGrids(add, index));
    }

    public static void LoadData()
    {
        for(int i = 0; i < GlobalVariable.FightCards.Count; i++)
        {
            fightCardsGrids[i] = GlobalVariable.FightCards[i];
            count++;
        }
        Show();
    }

    public IEnumerator AlterFithtGrids(bool add,int index)
    {
        clickable = false;
        BarScript.clickable = false;
        if (add)
        {
            if (index >= firstindex && index < firstindex + pagenum)
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("cardpanel/bar2"), new Vector3(positionX, positionY + grapY * (index % pagenum), positionZ), Quaternion.identity);
                temp.transform.parent = gameObject.transform.parent;
                temp.GetComponent<BarScript>().index = index;
                onShowNum++;
                fightCardsOnShow[index - firstindex] = temp;
                fightCardsGrids[index] = gameProp;
                temp.GetComponent<BarScript>().SetGameProp(gameProp);
                temp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cardpanel/" + gameProp.SerialNumber);
                temp.transform.Find("info").GetComponent<TextMesh>().text = gameProp.Name;
                temp.transform.Find("number").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cardpanel/" + gameProp.Type.Substring(0, 2) + gameProp.EnergyConsumption);
            }
            if (index >= firstindex + pagenum)
            {
                fightCardsGrids[count] = gameProp;
            }
        }
        else
        {
            for (int i = index; i < count; i++)
            {
                fightCardsGrids[i] = (i + 1 == count) ? null : fightCardsGrids[i + 1];
            }
            count--;
            Destroy(fightCardsOnShow[index % pagenum]);
            for (int i = index % pagenum; i < onShowNum; i++)
            {
                if (i + 1 != onShowNum)
                {
                    fightCardsOnShow[i] = fightCardsOnShow[i + 1];
                    fightCardsOnShow[i].GetComponent<BarScript>().index--;
                    yield return new WaitForSeconds(0.2f);
                    fightCardsOnShow[i].transform.position = new Vector3(positionX, positionY + fightCardsOnShow[i].GetComponent<BarScript>().index % pagenum * grapY, positionZ);
                }
            }

            if (firstindex + onShowNum <= count)
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("cardpanel/bar2"), new Vector3(positionX, positionY + (onShowNum - 1) * grapY, positionZ), Quaternion.identity);
                temp.GetComponent<BarScript>().index = firstindex + onShowNum - 1;
                temp.GetComponent<BarScript>().SetGameProp(fightCardsGrids[firstindex + onShowNum - 1]);
                temp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cardpanel/" + fightCardsGrids[firstindex + onShowNum - 1].SerialNumber);
                temp.transform.Find("info").GetComponent<TextMesh>().text = fightCardsGrids[firstindex + onShowNum - 1].Name;
                temp.transform.Find("number").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cardpanel/" + fightCardsGrids[firstindex + onShowNum - 1].Type.Substring(0, 2) + fightCardsGrids[firstindex + onShowNum - 1].EnergyConsumption);
                fightCardsOnShow[onShowNum - 1] = temp;
                temp.transform.parent = gameObject.transform.parent;
            }
            else
            {
                onShowNum--;
            }
        }
        clickable = true;
        BarScript.clickable = true;
    }

    private static void Show()
    {
        foreach(GameObject t in fightCardsOnShow)
        {
            Destroy(t);
        }
        onShowNum = 0;
        for(int i = 0; i < pagenum && i + firstindex < count; i++)
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("cardpanel/bar2"), new Vector3(positionX, positionY + i * grapY, positionZ), Quaternion.identity);
            temp.GetComponent<BarScript>().index = firstindex + i;
            onShowNum++;
            temp.transform.parent = GameObject.Find("GameObject").transform.parent;
            temp.GetComponent<BarScript>().SetGameProp(fightCardsGrids[i + firstindex]);
            temp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cardpanel/" + fightCardsGrids[i + firstindex].SerialNumber);
            temp.transform.Find("info").GetComponent<TextMesh>().text = fightCardsGrids[i + firstindex].Name;
            temp.transform.Find("number").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cardpanel/" + fightCardsGrids[i + firstindex].Type.Substring(0, 2) + fightCardsGrids[i + firstindex].EnergyConsumption);
            fightCardsOnShow[i] = temp;
        }
    }

    public void NextPage()
    {
        if (firstindex + pagenum < count)
        {
            firstindex += pagenum;
            Show();
        }
    }
    public void PrevPage()
    {
        if (firstindex - pagenum >= 0) 
        {
            firstindex -= pagenum;
            Show();
        }
    }
}
