using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

    public static PanelManager _instance;

    public List<SkillSelect> skills = new List<SkillSelect>();

    private void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start () {

        Init();



        GlobalVariable.ExistingLeadSkills.Add(new GameProp("100001", "气灸术", 0, "a2c2", "在回合结束时回复10 % 生命值，持续5回合", 0.1f, 5, 0, 0, "0", "hp_add"));
        GlobalVariable.ExistingLeadSkills.Add(new GameProp("100002", "气灸术", 0, "a2c2", "在回合结束时回复10 % 生命值，持续5回合", 0.1f, 5, 0, 0, "0", "hp_add"));
        GlobalVariable.ExistingLeadSkills.Add(new GameProp("100003", "气灸术", 0, "a2c2", "在回合结束时回复10 % 生命值，持续5回合", 0.1f, 5, 0, 0, "0", "hp_add"));
        GlobalVariable.ExistingLeadSkills.Add(new GameProp("100001", "气灸术", 0, "a2c2", "在回合结束时回复10 % 生命值，持续5回合", 0.1f, 5, 0, 0, "0", "hp_add"));
        GlobalVariable.ExistingLeadSkills.Add(new GameProp("100001", "气灸术", 0, "a2c2", "在回合结束时回复10 % 生命值，持续5回合", 0.1f, 5, 0, 0, "0", "hp_add"));
        GlobalVariable.ExistingLeadSkills.Add(new GameProp("100002", "气灸术", 0, "a2c2", "在回合结束时回复10 % 生命值，持续5回合", 0.1f, 5, 0, 0, "0", "hp_add"));
        GlobalVariable.ExistingLeadSkills.Add(new GameProp("100003", "气灸术", 0, "a2c2", "在回合结束时回复10 % 生命值，持续5回合", 0.1f, 5, 0, 0, "0", "hp_add"));
        GlobalVariable.ExistingLeadSkills.Add(new GameProp("100001", "气灸术", 0, "a2c2", "在回合结束时回复10 % 生命值，持续5回合", 0.1f, 5, 0, 0, "0", "hp_add"));
        GlobalVariable.ExistingLeadSkills.Add(new GameProp("100001", "气灸术", 0, "a2c2", "在回合结束时回复10 % 生命值，持续5回合", 0.1f, 5, 0, 0, "0", "hp_add"));
        GlobalVariable.ExistingLeadSkills.Add(new GameProp("100002", "气灸术", 0, "a2c2", "在回合结束时回复10 % 生命值，持续5回合", 0.1f, 5, 0, 0, "0", "hp_add"));
        GlobalVariable.ExistingLeadSkills.Add(new GameProp("100003", "气灸术", 0, "a2c2", "在回合结束时回复10 % 生命值，持续5回合", 0.1f, 5, 0, 0, "0", "hp_add"));
        GlobalVariable.ExistingLeadSkills.Add(new GameProp("100001", "气灸术", 0, "a2c2", "在回合结束时回复10 % 生命值，持续5回合", 0.1f, 5, 0, 0, "0", "hp_add"));
        ShowSkill();
    }
	// Update is called once per frame
	void Update () {
		
	}

    private void ShowSkill()
    {
        foreach(SkillSelect t in skills)
        {
            t.SetSkill(null);
        }

        for (int i = 0; i < skills.Count && i < GlobalVariable.ExistingLeadSkills.Count; i++) 
        {
            skills[i].SetSkill(GlobalVariable.ExistingLeadSkills[i]);
        }
    }

    private void OnMouseUpAsButton()
    {
        if(SkillSelect.currentSkillGrid!=null)
            GameObject.Find(SkillSelect.currentSkillGrid).transform.Find("frame").GetComponent<SpriteRenderer>().material = Resources.Load<Material>("materials/Default");
        SkillSelect.currentSkillGrid = null;
    }
    private void Init()
    {
        for(int i = 0; i < GlobalVariable.FightSkills.Length; i++)
        {
            if (GlobalVariable.FightSkills[i] != null)
            {
                GameObject.Find("skillgrid" + (i + 1).ToString()).transform.Find("skill").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("skill/" + GlobalVariable.FightSkills[i].SerialNumber);
            }
        }
    }
}
