using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 100;
    [SerializeField] private float speed = 8;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Vector3 currentPos;

    private Boolean isJumping;
    private Text score;
    private Text highScore;
    private int countScore;
    private bool isOnLog;
    public static int trackScore;
    private Vector3 DefaultScale;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentPos = transform.position;
        DefaultScale = transform.localScale;

        score = GameObject.Find("Score").GetComponent<Text>();
        highScore = GameObject.Find("HighScore").GetComponent<Text>();

        highScore.text = PlayerPrefs.GetString("HighScoreOwner") + PlayerPrefs.GetInt("HighestScore").ToString();

        countScore = 0;
        trackScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != new Vector3(currentPos.x, transform.position.y, (float)Math.Round(currentPos.z)) + moveDirection && !isOnLog)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentPos.x,
                                        transform.position.y, (float) Math.Round(currentPos.z)) + moveDirection, speed * Time.deltaTime);
        }
        else
        {
            moveDirection = Vector3.zero;
            currentPos = transform.position;

            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && !isJumping)
            {
                moveDirection = new Vector3(1, 0, 0);
                transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
                jump();
                incrementScore();
                isOnLog = false;
            }
            else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !isJumping && currentPos.z < 5)
            {
                moveDirection = new Vector3(0, 0, 1);
                transform.rotation = Quaternion.AngleAxis(-180, Vector3.up);
                jump();
                isOnLog = false;
            }
            else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !isJumping && currentPos.z > -6)
            {
                moveDirection = new Vector3(0, 0, -1);
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                jump();
                isOnLog = false;
            }
            else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !isJumping && trackScore > 1 && countScore - trackScore > -2)
            {
                moveDirection = new Vector3(-1, 0, 0);
                transform.rotation = Quaternion.AngleAxis(-270, Vector3.up);
                jump();
                countScore--;
                isOnLog = false;
            }
        }

        if (transform.position.z > 5.5 || transform.position.z < -6.5)
        {
            DestroyPlayer();
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.35f))
        {
            if (hit.collider.transform.tag != "Log" && hit.collider.transform.tag == "Finish")
            {
                DestroyPlayer();
            }
            else if (hit.collider.transform.tag == "Map")
            {
                isJumping = false;
                isOnLog = false;
            }
        }
    }

    void jump()
    {
        rb.AddForce(0, jumpForce, 0);
        isJumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Map")
        {
            isJumping = false;
            isOnLog = false;
        }
        else if (collision.gameObject.tag == "Tree1")
        {
            moveDirection = new Vector3(0, 0, 0);
            isJumping = false;
        }
        else if (collision.collider.GetComponent<Log>() != null)
        {
            if (collision.collider.GetComponent<Log>().isLog)
            {
                transform.parent = collision.collider.transform;
                isOnLog = true;
                isJumping = false;
            }
            else
            {
                isJumping = false;
                isOnLog = false;
                transform.parent = null;
            }
        }
        else if (collision.gameObject.tag == "Star")
        {
            countScore += 9;
            trackScore += 9;
            incrementScore();
            isJumping = false;
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Log")
        {
            isJumping = false;
            isOnLog = false;
            transform.parent = null;
            transform.localScale = DefaultScale;
        }
    }

    void incrementScore()
    {
        countScore++;
        if (countScore == trackScore + 1)
        {
            trackScore = countScore;
            score.text = "Score: " + trackScore.ToString();
        }
    }

    void DestroyPlayer()
    {
        Destroy(gameObject);
        KillPlayer.alive = false;
        MapGenerator.restartButton.SetActive(true);
        if (trackScore >= PlayerPrefs.GetInt("HighestScore"))
        {
            MapGenerator.inputName.SetActive(true);
            MapGenerator.keepHighScore = trackScore;
            MapGenerator.isHighScore = true;
        }
    }
}


