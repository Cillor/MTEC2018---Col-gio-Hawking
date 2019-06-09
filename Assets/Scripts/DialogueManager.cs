using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public bool restartWalk, dialogueEnded;
    private Movimentação movP;
    public GameObject dBox;
    [Space]
    public Text nameText;
    public Text dText;
    [Space]
    public bool dialogActive;
    [Space]

    public GameObject npcs;
    public string[] dialogLines;
    public int currentLine;

    private void Start()
    {
        movP = FindObjectOfType<Movimentação>();
    }
    void Update () {
		if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
        }

        if (currentLine >= dialogLines.Length) {
            dialogueEnded = true;
            CloseDialogue(npcs);
        }

        dText.text = dialogLines[currentLine];
    }

    public void CloseDialogue(GameObject npc)
    {
        dBox.SetActive(false);
        dialogActive = false;
        currentLine = 0;
        movP.movementActive = true;
        restartWalk = true;
        dialogueEnded = false;
        npc.GetComponent<NPCMovement>().staticMovement = 1;
    }
    public void ShowDialogue(GameObject npc, string nome)
    {
        nameText.text = nome; 
        npcs = npc;
        movP.movementActive = false;
        restartWalk = false;
        dBox.SetActive(true);
        dialogActive = true; 
    }
}
