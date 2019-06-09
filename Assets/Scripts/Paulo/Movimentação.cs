using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using Pathfinding;

public class Movimentação : MonoBehaviour
{
    public Animator anim;
    public GameObject normalClass, scienceClass, computerClass;
    [Space]
    public bool movementActive = true;
    public bool pRun = true;

    Camera cam;
    Rigidbody2D rb;
    Vector2 direction, mousePos;

    public float force = 200f, runFactor = 1.8f;
    float forceStop;
    bool isRunning;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void CamWalkClick()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - rb.position).normalized;
        transform.up = direction;

        if (Input.GetMouseButton(1))
        {
            Vector2 forceToApply = direction * force * Time.deltaTime * runFactor;
            isRunning = true;
            rb.AddForce(forceToApply);
            forceStop = 1;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 forceToApply = direction * force * Time.deltaTime;
            isRunning = false;
            rb.AddForce(forceToApply);
            forceStop = 1;
        }
        else
        {
            if(forceStop > 0)
                forceStop -= Time.deltaTime * 0.25f;

            rb.velocity *= forceStop;
        }
    }

    private void Update()
    {       
        if (movementActive)
        {
            CamWalkClick();
            if (rb.velocity.magnitude >= 0.3f)
            {
                if (pRun)
                {
                    if (isRunning)
                    {
                        FindObjectOfType<AudioManager>().Play("Running");
                        FindObjectOfType<AudioManager>().Stop("Walking");
                        anim.speed = runFactor - 0.4f;
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("Walking");
                        FindObjectOfType<AudioManager>().Stop("Running");
                        anim.speed = 1;
                    }
                    pRun = false;
                }
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.speed = 1;
                anim.SetBool("Walking", false);
                FindObjectOfType<AudioManager>().Stop("Running");
                FindObjectOfType<AudioManager>().Stop("Walking");
                pRun = true;
            }

            //SceneLoaders
            if (loader1 == true && Input.GetKey(KeyCode.E))
            {
                FindObjectOfType<AudioManager>().Stop("Walking");
                FindObjectOfType<AudioManager>().Stop("Running");
                FindObjectOfType<AudioManager>().Play("QuizMusic");
                FindObjectOfType<AudioManager>().Stop("Theme");
                //Debug.Log("Ativando quiz normal");
                normalClass.SetActive(true);
                movementActive = false;
                normalClass.GetComponent<QuizManager>().RestartingThings();
            }

            if (loader2 == true && Input.GetKey(KeyCode.E))
            {
                FindObjectOfType<AudioManager>().Stop("Walking");
                FindObjectOfType<AudioManager>().Stop("Running");
                FindObjectOfType<AudioManager>().Play("QuizMusic");
                FindObjectOfType<AudioManager>().Stop("Theme");
                //Debug.Log("Ativando quiz computador");
                computerClass.SetActive(true);
                movementActive = false;
                computerClass.GetComponent<QuizManager>().RestartingThings();
            }

            if (loader3 == true && Input.GetKey(KeyCode.E))
            {
                FindObjectOfType<AudioManager>().Stop("Walking");
                FindObjectOfType<AudioManager>().Stop("Running");
                FindObjectOfType<AudioManager>().Play("QuizMusic");
                FindObjectOfType<AudioManager>().Stop("Theme");
                //Debug.Log("Ativando quiz ciencias");
                scienceClass.SetActive(true);
                movementActive = false;
                scienceClass.GetComponent<QuizManager>().RestartingThings();
            }
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    //public TilemapRenderer roof;
    bool loader1 = false, loader2 = false, loader3 = false;
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("AulaPadrão"))
        {
            //if (!normalClass.activeSelf) {Debug.Log("Collider aula normal");}
            loader1 = true;
        }

        if (col.gameObject.CompareTag("AulaComputação"))
        {
            //if (!computerClass.activeSelf){Debug.Log("Collider aula computação");}
            loader2 = true;
        }

        if (col.gameObject.CompareTag("AulaCiencias"))
        {
            //if (!scienceClass.activeSelf){Debug.Log("Collider aula ciencias");}
            loader3 = true;
        }

        if (col.gameObject.CompareTag("Telhado"))
        {
            //roof.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Telhado"))
        {
            //roof.enabled = true;
        }

        if (other.gameObject.CompareTag("AulaPadrão"))
        {
            //if (!normalClass.activeSelf) {Debug.Log("Collider aula normal");}
            loader1 = false;
        }

        if (other.gameObject.CompareTag("AulaComputação"))
        {
            //if (!computerClass.activeSelf){Debug.Log("Collider aula computação");}
            loader2 = false;
        }

        if (other.gameObject.CompareTag("AulaCiencias"))
        {
            //if (!scienceClass.activeSelf){Debug.Log("Collider aula ciencias");}
            loader3 = false;
        }
    }
}
