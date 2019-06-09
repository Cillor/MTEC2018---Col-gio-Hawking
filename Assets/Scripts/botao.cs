using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class botao : MonoBehaviour {
    public GameObject loading;
    public Slider loadingbar;
    public Text progresstext;

    Indicadores ind;

    void Start()
    {
        ind = FindObjectOfType<Indicadores>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            FindObjectOfType<AudioManager>().Play("MusicMenu");
            FindObjectOfType<AudioManager>().Stop("DeathMusic");
            FindObjectOfType<AudioManager>().Stop("QuizMusic");
            FindObjectOfType<AudioManager>().Stop("Theme");
        }
    }

    public void PlayThis(string _audio)
    {
        FindObjectOfType<AudioManager>().Play(_audio);
    }

    public void PlayButton(int sceneIndex)
    {
        Time.timeScale = 1f;
        
        FindObjectOfType<AudioManager>().Stop("MusicMenu");
        FindObjectOfType<AudioManager>().Stop("DeathMusic");
        FindObjectOfType<AudioManager>().Stop("QuizMusic");
        FindObjectOfType<AudioManager>().Play("Theme");
        loading.SetActive(true);
        StartCoroutine(load(sceneIndex));
    }

    IEnumerator load(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingbar.value = progress;
            progresstext.text = progress * 100 + "%";
            yield return null;
        }
    }
    public void MenuButton()
    {
        ind.isDead = false;
        FindObjectOfType<AudioManager>().Play("MusicMenu");
        FindObjectOfType<AudioManager>().Stop("QuizMusic");
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Stop("DeathMusic");
        SceneManager.LoadScene("Start");
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}

 