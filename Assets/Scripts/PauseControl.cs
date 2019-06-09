using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour {

   [SerializeField] public bool isPaused = false;

    public GameObject PauseMenu;
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
	}

    public void Resume()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        isPaused = false;
    }

    void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Menu(int sceneIndex)
    {
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Play("MusicMenu");
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
