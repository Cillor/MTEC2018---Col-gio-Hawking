using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour {

    public QuestManager qMan;
    public GameHandler gHan;

    public int questNumber;

    [TextArea]
    public string[] startText, endText;
	void Start () {
        //qMan = FindObjectOfType<QuestManager>();
	}

    public void CreatePointer(Transform location)
    {
        gHan.CreatePointer(location, questNumber);
    }

    public void StartQuest(GameObject npc, string nome)
    {
        qMan.ShowQuestText(startText, npc, nome);
        qMan.middleQuestCompleted[questNumber] = true;
    }

    public void EndQuest(GameObject npc, string nome)
    {
        qMan.ShowQuestText(endText, npc, nome);
        qMan.questCompleted[questNumber] = true;
        gameObject.SetActive(false);
    }
}
