using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private float distance = 600f;

    public ClockManager clock;

    [SerializeField] private Window_QuestPointer windowQuestPointer;
    public QuestManager theQM;

    public void CreatePointer(Transform targets, int questNumber)
    {
        Window_QuestPointer.QuestPointer questPointers = windowQuestPointer.CreatePointer(new Vector3(targets.position.x, targets.position.y, targets.position.z));
        FunctionUpdater.Create(() =>
        {
            #region
            float xDistancia = Camera.main.transform.position.x - targets.position.x;
            float yDistancia = Camera.main.transform.position.y - targets.position.y;
            #endregion
            float distancia = Mathf.Sqrt(xDistancia * xDistancia + yDistancia * yDistancia);
            #region
            /*Debug.Log(distancia);
            Debug.Log(distance);
            Debug.Log("Cam Distance: " + (distancia < distance));
            Debug.Log("middleQuest: " + (theQM.middleQuestCompleted[questNumber]));
            Debug.Log("Quest: " + (theQM.questCompleted[questNumber]));
            Debug.Log("Everything: " + (Input.GetKeyDown(KeyCode.E) && (distancia < distance)));*/
            #endregion
            if (Input.GetKeyDown(KeyCode.E) && (distancia < distance))
            {
                windowQuestPointer.DestroyPointer(questPointers);
                return true;
            }
            else
            {
                return false;
            }
        });
    }

    public void StartPointerCreation(Transform targets, int day)
    {
        Window_QuestPointer.QuestPointer questPointers = windowQuestPointer.CreatePointer(new Vector3(targets.position.x, targets.position.y, targets.position.z));
        FunctionUpdater.Create(() =>
        {
            #region
            float xDistancia = Camera.main.transform.position.x - targets.position.x;
            float yDistancia = Camera.main.transform.position.y - targets.position.y;
            #endregion
            float distancia = Mathf.Sqrt(xDistancia * xDistancia + yDistancia * yDistancia);
            if (clock.tday == day || (Input.GetKeyDown(KeyCode.E) && (distancia < distance)))
            {
                windowQuestPointer.DestroyPointer(questPointers);
                return true;
            }
            //Debug.Log("Non E pressed " + (Vector3.Distance(Camera.main.transform.position, new Vector3(targets.position.x, targets.position.y, targets.position.z)) < distance));
            //Debug.Log("Pressing E too " + (Input.GetKeyDown(KeyCode.E) && (Vector3.Distance(Camera.main.transform.position, new Vector3(targets.position.x, targets.position.y, targets.position.z)) < distance)));
            else
            {
                return false;
            }
        });
    }
}
