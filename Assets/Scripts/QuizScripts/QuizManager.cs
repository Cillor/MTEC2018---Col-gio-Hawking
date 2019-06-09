using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour {
    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Text factText, isCorretText;

    [SerializeField]
    private float timeBetweenQuestions = 1f;

    public Indicadores status;
    public Movimentação mov;

    [Space] [SerializeField] private GameObject isso;

    public void RestartingThings()
    {
        unansweredQuestions = questions.ToList<Question>();

        SetCurrentQuestion();
    }

    public void Return()
    {
        FindObjectOfType<AudioManager>().Stop("QuizMusic");
        FindObjectOfType<AudioManager>().Play("Theme");
        isso.SetActive(false);
        mov.movementActive = true;
    }
    void Start()
    {
    if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
    }
    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);
        animator.SetBool("Go", true);
        yield return new WaitForSeconds(timeBetweenQuestions);
        animator.SetBool("Go", false);

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
    }

    public void UserSelectTrue()
    {
        if (currentQuestion.isTrue)
        {
            status.gradeAmount += 1f * ((status.knowledgeAmount/100000)+1);
            isCorretText.text = ("Você acertou!");
        }
        else
        {
            status.gradeAmount -= 0.2f * ((status.knowledgeAmount / 100000) + 1);
            isCorretText.text = ("Você errou!");
        }
        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        if (!currentQuestion.isTrue)
        {
            status.gradeAmount += 1f * ((status.knowledgeAmount / 100000) + 1);
            isCorretText.text = ("Você acertou!");
        }
        else
        {
            status.gradeAmount -= 0.2f *((status.knowledgeAmount / 100000) + 1);
            isCorretText.text = ("Você errou!");
        }
        StartCoroutine(TransitionToNextQuestion());
    }
}
