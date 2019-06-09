using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockManager : MonoBehaviour {

    public TextMeshProUGUI mhr;
    public TextMeshProUGUI dmy;


    public float timer = 1;

    public int minute = 30, hour = 7, day = 1, month = 2, year = 2018, tday = 1;

    IEnumerator Clock()    {
        int a = 0;
        do {
            minute += 1;
            yield return new WaitForSeconds(timer);
            if (minute >= 60)
            {
                minute = 0;
                hour += 1;
                if (hour >= 24)
                {
                    hour = 0;
                    day += 1;
                    tday += 1;
                    if (day >= 30)
                    {
                        day = 1;
                        month += 1;
                        if (month >= 5)
                        {
                            month = 1;
                            year += 1;
                        }
                    }
                }
            }
        } while (a == 0);
    }
	void Start () {
        StartCoroutine("Clock");
	}

    private void Update()
    {
        mhr.SetText (hour + " : " + minute);
        dmy.SetText ("    " + day + " / " + month + " / " + year);
    }
}


