using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private TerrainSpawner terrainSpawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private Button startButton;
    [SerializeField] private Button playAgainButton;

    private int score = 0;
    private Animator animator;
    private bool isHopping;
    private bool isGameStarted = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.W) && !isHopping)
            {
                score++;
                scoreText.text = "Score : " + score;
                float zDifference = 0;
                if ((transform.position.z % 1) != 0)
                {
                    zDifference = Mathf.Round(transform.position.z) - transform.position.z;
                }
                MoveCharacter(new Vector3(1, 0, zDifference));
            }
            else if (Input.GetKeyDown(KeyCode.A) && !isHopping)
            {
                MoveCharacter(new Vector3(0, 0, 1));
            }
            else if (Input.GetKeyDown(KeyCode.D) && !isHopping)
            {
                MoveCharacter(new Vector3(0, 0, -1));
            }
        }
        
    }

    private void MoveCharacter(Vector3 difference)
    {
        animator.SetTrigger("hop");
        isHopping = true;
        transform.Translate(difference);
        //transform.position = Vector3.Lerp(transform.position, transform.position + difference, 1f);
        terrainSpawner.SpawnTerrain(transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<MovingObject>() != null)
        {
            if (collision.collider.GetComponent<MovingObject>().isBoat)
            {
                transform.parent = collision.collider.transform;
            }
        }
        else
        {
            transform.parent = null;
        }

        if(collision.gameObject.tag == "Enemy")
        {
            this.gameObject.SetActive(false);
            playAgainButton.gameObject.SetActive(true);
        }
    }

    public void FinishHop()
    {
        isHopping = false;
    }

    public void GameStart()
    {
        isGameStarted = true;
        startButton.gameObject.SetActive(false);
    }
}
