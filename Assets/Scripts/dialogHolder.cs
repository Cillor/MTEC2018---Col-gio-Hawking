using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogHolder : MonoBehaviour {

    private NPCMovement npcM;
    private DialogueManager dMan;
    private bool colliding = false;
    [Space]
    [TextArea]
    public string nome;
    [TextArea]
    public string[] dialogueLines;

    private void Start()
    {
        dMan = FindObjectOfType<DialogueManager>();
        npcM = GetComponent<NPCMovement>();
    }

    private void Update()
    {
        if (dMan.restartWalk)
        {
            npcM.staticMovement = 1;
        }
        if(Input.GetKeyUp(KeyCode.E) && colliding)
        {
            if (!dMan.dialogActive)
            {
                npcM.staticMovement = 0;
                dMan.dialogLines = dialogueLines;
                dMan.currentLine = 0;
                dMan.nameText.text = nome;
                dMan.ShowDialogue(null, null);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dMan.CloseDialogue(null);
        colliding = false;
    }
}
