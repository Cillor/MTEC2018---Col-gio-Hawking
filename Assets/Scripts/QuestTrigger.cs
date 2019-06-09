using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour {

    private QuestManager theQM;
    public int questNumber;
    public GameObject startLocation;
    public SpriteRenderer startCross;
    public SpriteRenderer endCross;
    public GameObject endLocation;

    [Space]
    public GameObject npcStart;
    public string npcSnome;
    public GameObject npcFinal;
    public string npcFnome;

    bool col;
    public bool isStartingQuest;

	void Start () {
        theQM = FindObjectOfType<QuestManager>();        
    }
	
	void Update () {
        TargetPosition();

        if (col && Input.GetKeyDown(KeyCode.E))
        {
            if (!theQM.questCompleted[questNumber])
            {
                FindObjectOfType<AudioManager>().Stop("Walking");
                FindObjectOfType<AudioManager>().Stop("Running");
                if (isStartingQuest && !theQM.quests[questNumber].gameObject.activeSelf && Input.GetKeyDown(KeyCode.E))
                {
                    theQM.quests[questNumber].gameObject.SetActive(true);
                    theQM.quests[questNumber].StartQuest(npcStart, npcSnome);
                    startCross.enabled = false;
                    endCross.enabled = true;
                    theQM.quests[questNumber].CreatePointer(endLocation.transform);
                }
                if (!isStartingQuest && theQM.quests[questNumber].gameObject.activeSelf && Input.GetKeyDown(KeyCode.E))
                {
                    theQM.quests[questNumber].EndQuest(npcFinal, npcFnome);
                    endCross.enabled = false;
                }
            }
        }
	}

    void TargetPosition()
    {
        if (npcStart != null)
        {
            npcStart.transform.position = startLocation.transform.position;
            npcStart.GetComponent<NPCMovement>().staticMovement = 0;
        }

        npcFinal.transform.position = endLocation.transform.position;
        npcFinal.GetComponent<NPCMovement>().staticMovement = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            col = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        col = false;
    }
}
