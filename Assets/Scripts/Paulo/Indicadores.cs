using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Indicadores : MonoBehaviour
{
    private float averageGrade = 60;

    public GameObject loseByHungry, loseByHealth, loseByDepression, loseByGrades, loseByObesity, loseByExcessOfPhysicalExercises, loseByExcessOfFriends;
    [Space]
    public Image healthFiller;
    public Image gradesFiller, foodFiller, socialFiller;
    public TextMeshProUGUI knowledgeText;
    [HideInInspector]
    public float healthFillAmount, foodFillAmount, socialFillAmount;
    [HideInInspector]
    public float knowledgeAmount = 1800, gradeAmount = 0;
    private float foodAmount = 0, healthAmount = 1.3f, socialAmount = 1.3f;
    private float foodSubtractionModifier, healthSubtractionModifier, socialSubtractionModifier;
    private QuestControl questController;

    [HideInInspector]
    public bool isDead = true;

    private void Start()
    {
        questController = FindObjectOfType<QuestControl>();
        isDead = false;
        StartCoroutine("xAxisSubtractionByTime");
    }

    void Update()
    {
        if (questController.currentQuest <= questController.questList.Length)
        {
            //Perdendo o jogo
            if (healthAmount < -Mathf.PI)
            {
                Die();
                loseByHealth.SetActive(true);
            }
            if (foodAmount < -Mathf.PI)
            {
                Die();
                loseByHungry.SetActive(true);
            }
            if (socialAmount < -Mathf.PI)
            {
                Die();
                loseByDepression.SetActive(true);
            }
            if (healthAmount > Mathf.PI)
            {
                Die();
                loseByExcessOfPhysicalExercises.SetActive(true);
            }
            if (foodAmount > Mathf.PI)
            {
                Die();
                loseByObesity.SetActive(true);
            }
            if (socialAmount > Mathf.PI)
            {
                Die();
                loseByExcessOfFriends.SetActive(true);
            }
            if ((questController.currentQuest == questController.questList.Length - 1) && gradeAmount < averageGrade)
            {
                Die();
                loseByGrades.SetActive(true);
            }
        }

        FillAllStatusBar();
        SubtractionModifier();
    }

    void FillAllStatusBar()
    {
        healthFillAmount = (Mathf.Sin(healthAmount * 0.5f) + 1) / 2; //Função de seno
        healthFiller.fillAmount = healthFillAmount; // linha para preencher a barra indicadora

        socialFillAmount = (Mathf.Sin(socialAmount * 0.5f) + 1) / 2; //Função de seno
        socialFiller.fillAmount = socialFillAmount; // linha para preencher a barra indicadora

        foodFillAmount = (Mathf.Sin(foodAmount * 0.5f) + 1) / 2; //Função de seno
        foodFiller.fillAmount = foodFillAmount; // linha para preencher a barra indicadora

        gradesFiller.fillAmount = gradeAmount / 100; // linha para preencher a barra indicadora

        knowledgeText.SetText(knowledgeAmount.ToString()); //linha para escrever sua quantidade de conhecimento
    }

    void SubtractionModifier()
    {
        foodSubtractionModifier = (16 * (foodFillAmount * foodFillAmount)) - (16 * foodFillAmount) + 5f; //Função quadratica
        healthSubtractionModifier = (10 * (healthFillAmount * healthFillAmount)) - (16 * healthFillAmount) + 7.4f; //Função quadratica
        socialSubtractionModifier = (10 * (socialFillAmount * socialFillAmount)) - (16 * socialFillAmount) + 7.4f; //Função quadratica
    }

    void Die()
    {
        Time.timeScale = 0f;
        if (!isDead)
        {
            FindObjectOfType<AudioManager>().Stop("Walking");
            FindObjectOfType<AudioManager>().Stop("Theme");
            FindObjectOfType<AudioManager>().Play("DeathMusic");
            isDead = true;
        }
    }

    IEnumerator xAxisSubtractionByTime()
    {
        do
        {
            if (-Mathf.PI < healthAmount)
            {
                healthAmount -= 0.003f * foodSubtractionModifier * socialSubtractionModifier; //Função linear
            }
            if (-Mathf.PI < socialAmount)
            {
                socialAmount -= 0.002f * healthSubtractionModifier * foodSubtractionModifier; //Função linear
            }
            if (-Mathf.PI < foodAmount)
            {
                foodAmount -= 0.003f * healthSubtractionModifier * socialSubtractionModifier; //Função linear
            }
            yield return new WaitForSeconds(1f);
        } while (true);
    }

    #region variables for farm control
    bool quadra = false, cantina = false, biblio = false;
    #endregion
    void OnTriggerStay2D(Collider2D farm)
    {
        if (farm.gameObject.CompareTag("Quadra"))
        {
            quadra = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<AudioManager>().Play("BallKick");
                if (-Mathf.PI < foodAmount && foodAmount < Mathf.PI)
                {
                    foodAmount -= 0.005f * healthSubtractionModifier;
                }
                if (-Mathf.PI < healthAmount && healthAmount < Mathf.PI)
                {
                    healthAmount += 0.01f;
                }
            }
        }
        else
        {
            quadra = false;
        }

        if (farm.gameObject.CompareTag("Cantina"))
        {
            cantina = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<AudioManager>().Play("BagRip");
                if (-Mathf.PI < healthAmount && healthAmount < Mathf.PI)
                {
                    healthAmount -= 0.005f * foodSubtractionModifier;
                }
                if (-Mathf.PI < foodAmount && foodAmount < Mathf.PI)
                {
                    foodAmount += 0.01f;
                }
            }
        }
        else
        {
            cantina = false;
        }

        if (farm.gameObject.CompareTag("Biblioteca"))
        {
            biblio = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<AudioManager>().Play("OpeningBook");
                if (-Mathf.PI < socialAmount && socialAmount < Mathf.PI)
                {
                    socialAmount -= 0.005f * socialSubtractionModifier;
                }
                if (-Mathf.PI < foodAmount && foodAmount < Mathf.PI)
                {
                    foodAmount -= 0.005f * healthSubtractionModifier;
                }
                if (-Mathf.PI < healthAmount && healthAmount < Mathf.PI)
                {
                    healthAmount -= 0.005f * foodSubtractionModifier;
                }
                knowledgeAmount += 1f;
            }
        }
        else
        {
            biblio = false;
        }

        if (farm.gameObject.CompareTag("NPC"))
        {
            if (!quadra && !cantina && !biblio)
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (-Mathf.PI < socialAmount && socialAmount < Mathf.PI)
                    {
                        socialAmount += 0.002f;
                    }
                }
        }
    }
}
