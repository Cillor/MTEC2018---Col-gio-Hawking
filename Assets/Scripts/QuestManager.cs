using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public QuestObject[] quests;
    public bool[] middleQuestCompleted;
    public bool[] questCompleted;

    public DialogueManager theDM;

	void Start () {
        questCompleted = new bool[quests.Length];
        middleQuestCompleted = new bool[quests.Length];
    }

    public void ShowQuestText (string[] questText, GameObject npc, string nome)
    {
        //theDM.dialogLines = new string [1];
        //theDM.dialogLines[0] = questText;
        theDM.dialogLines = questText;

        theDM.currentLine = 0;
        theDM.ShowDialogue(npc, nome);
    }
}
