﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadConversation : MonoBehaviour {

    private static string conversationSerialNumber;

    private static string nextScene;

    private List<string> conversation = new List<string>();

    private int index = 0;

    private bool isConversationOver = false;

    private bool isClickToDisplayText = false;

	// Use this for initialization
	void Start () {
        string conversationText = Resources.Load(string.Format("conversation/texts/{0}", 
            conversationSerialNumber)).ToString();
        using (System.IO.StringReader reader = new System.IO.StringReader(conversationText))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                conversation.Add(line);
            }
        }
        StartCoroutine(Load());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void SetConversation(string scene, int part, string nextscene)
    {
        conversationSerialNumber = string.Format("{0}-{1}", scene, part.ToString());
        nextScene = nextscene;
        SceneManager.LoadScene("conversation");
    }

    private IEnumerator Load()
    {
        string[] data = conversation[index].Split('=');
        GameObject panel = GameObject.Find("white");
        if(data.Length==3)
        {
            if (data[0].Equals("叶明卿"))
            {
                panel.transform.Find("leadName").transform.GetComponent<TextMesh>().text = data[0];
                panel.transform.Find("npcName").transform.GetComponent<TextMesh>().text = "";
            }
            else if(data[0].Equals("scene"))
            {
                panel.transform.Find("leadName").transform.GetComponent<TextMesh>().text = "";
                panel.transform.Find("npcName").transform.GetComponent<TextMesh>().text = "";
            }
            else
            {
                panel.transform.Find("npcName").transform.GetComponent<TextMesh>().text = data[0];
                panel.transform.Find("leadName").transform.GetComponent<TextMesh>().text = "";
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(string.Format("conversation/scenes/{0}", data[1]));
            foreach (Transform t in panel.transform)
            {
                switch (t.name)
                {
                    case "character":
                        if (data[0].Equals("叶明卿"))
                        {
                            t.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("conversation/characters/叶明卿");
                        }
                        else
                        {
                            t.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("conversation/characters/base");
                        }
                        break;
                    case "npc":
                        if (!data[0].Equals("叶明卿") && !data[0].Equals("scene"))
                        {
                            t.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(string.Format("conversatio/characters/{0}", data[0]));
                        }
                        else
                        {
                            t.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("conversation/characters/base");
                        }
                        break;
                    case "conversation":
                        string text = Regex.Replace(data[2], @"\S{25}", "$0\r\n");
                        for(int i = 1; i < text.Length+1; ++i)
                        {
                            if (isClickToDisplayText)
                            {
                                t.gameObject.GetComponent<TextMesh>().text = text;
                                isClickToDisplayText = false;
                                break;
                            }
                            t.gameObject.GetComponent<TextMesh>().text = text.Substring(0, i);
                            yield return new WaitForSeconds(0.1f);
                        }
                        isConversationOver = true;
                        break;
                    default:
                        break;
                }
            }
        }
        if (data.Length == 2)
        {
            isConversationOver = true;
            switch(data[0])
            {
                case "get_skill":
                    GameProp temp1;
                    if(GlobalVariable.AllLeadSkills.TryGetValue(data[1], out temp1))
                    {
                        GlobalVariable.ExistingLeadSkills.Add(temp1);
                        if (conversationSerialNumber.Split('-')[0].Equals("0"))
                        {
                            int count = 0;
                            foreach(GameProp skill in GlobalVariable.FightSkills)
                            {
                                if(skill != null)
                                {
                                    ++count;
                                }
                            }
                            GlobalVariable.FightSkills[count] = temp1;
                        }
                    }             
                    break;
                case "get_item":
                    GameProp temp2;
                    if (GlobalVariable.AllGameItems.TryGetValue(data[1], out temp2))
                    {
                        if (temp2.Type.Equals("a4e17"))
                        {
                            GlobalVariable.PlotItems.Add(temp2);
                        }
                        else GlobalVariable.BattleItems.Add(temp2);
                    }
                    break;
                case "get_card":
                    GlobalVariable.ExistingCards.Add(GlobalVariable.AllCards[data[1]]);
                    GlobalVariable.FightCards.Add(GlobalVariable.AllCards[data[1]]);
                    break;
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        if (!isConversationOver)
        {
            isClickToDisplayText = true;
            return;
        }
        if (index + 1 < conversation.Count && isConversationOver)
        {
            ++index;
            isConversationOver = false;
            StartCoroutine(Load());
        }
        else if(index + 1 >= conversation.Count)
        {
            index = 0;
            switch (nextScene)
            {
                case "fight":
                    SceneManager.LoadScene("fighting");
                    break;
                case "conversation":
                    string[] serialNumber = conversationSerialNumber.Split('-');
                    int number = int.Parse(serialNumber[2]);
                    if (serialNumber[1].Equals("0"))
                    {
                        if (number + 1 <= 5)
                        {
                            SetConversation(serialNumber[0] + "-" + serialNumber[1] + "-" + (number + 1), 0, nextScene);
                        }
                        else
                        {
                            GlobalVariable.currentScene = "0-1-1";
                            for(int i = 0; i < 5; ++i)
                            {
                                GlobalVariable.ExistingCards.Add(GlobalVariable.AllCards["001"]);
                                GlobalVariable.FightCards.Add(GlobalVariable.AllCards["001"]);
                            }                         
                            SetConversation("0-1-1", 0, "fight");
                        }
                    }
                    else
                    {
                        if (number <= 2)
                        {
                            GlobalVariable.currentScene = serialNumber[0] + "-" + serialNumber[1] + "-" + (number+1);
                            SetConversation(GlobalVariable.currentScene, 0, "fight");
                        }
                    }                
                    break;
                default:
                    SceneManager.LoadScene(nextScene);
                    break;
            }
        }
    }
}
