using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestControl : MonoBehaviour {
    public ClockManager clockManager;
    public QuestManager questManager;
    public GameHandler gameHandler;

    public int questsIgnored;
    public GameObject[] questList;
    public Transform[] firstLocationOfEachQuest;
    public bool[] createPointer;
    public GameObject loseScreen;

	void Start () {
        createPointer = new bool[questList.Length];
        anticlock = 2;
        currentQuest = 4;
	}

    public int counter = 1, anticlock, currentQuest = 4;
	void Update () {
        #region Quest Tutorial
        if (!createPointer[0]){
            gameHandler.CreatePointer(firstLocationOfEachQuest[0], 0);
            questList[0].SetActive(true);
            createPointer[0] = true;
        } else if (questManager.questCompleted[0] && !createPointer[1])
        {
            gameHandler.CreatePointer(firstLocationOfEachQuest[1], 1);
            questList[0].SetActive(false);
            questList[1].SetActive(true);
            createPointer[1] = true;
        }
        else if (questManager.questCompleted[1] && !createPointer[2])
        {
            gameHandler.CreatePointer(firstLocationOfEachQuest[2], 2);
            questList[1].SetActive(false);
            questList[2].SetActive(true);
            createPointer[2] = true;
        }
        else if (questManager.questCompleted[2] && !createPointer[3])
        {
            gameHandler.CreatePointer(firstLocationOfEachQuest[3], 3);
            questList[2].SetActive(false);
            questList[3].SetActive(true);
            createPointer[3] = true;
        }
        #endregion
        
        if (questsIgnored >= 4)
            loseScreen.SetActive(true);

        #region Verifications
        /*Debug.Log("x :"+x);
        Debug.Log("anticlock :" + anticlock);
        Debug.Log("counter :" + counter);
        Debug.Log("Clocker :" +(clock.tday - anticlock));*/
        #endregion

        if (currentQuest < questList.Length)
        {
            if (clockManager.tday - anticlock > 0)
            {
                counter++;
                //Debug.Log("counter :" + counter);
                if (counter == 2)
                {
                    if (currentQuest - 1 > 3)
                    {
                        //Debug.Log(x+"-1 > 3");
                        questList[currentQuest - 1].SetActive(false);
                        if (questManager.questCompleted[currentQuest - 1])
                        {
                            questsIgnored = 0;
                        }
                        else if (!questManager.questCompleted[currentQuest - 1])
                        {
                            questsIgnored += 1;
                        }
                    }
                    //Debug.Log("Setting " + x + " to true");
                    questList[currentQuest].SetActive(true);
                    if (!createPointer[currentQuest])
                    {
                        gameHandler.StartPointerCreation(firstLocationOfEachQuest[currentQuest], clockManager.tday + 2);
                        createPointer[currentQuest] = true;
                    }
                    //Debug.Log("Adding x++");
                    currentQuest++;
                    //Debug.Log("counter = 0");
                    counter = 0;
                }
                anticlock++;
            }
        }
    }
}
