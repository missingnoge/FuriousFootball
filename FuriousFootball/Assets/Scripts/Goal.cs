using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private enum GoalMode
    {
        enemyGoal,
        playerGoal
    }

    [SerializeField] private GoalMode mode = GoalMode.enemyGoal;
    private ScoreManager scoreScr;

    private bool countingDown = false;
    private float resetCounter = 0f;
    private float resetTime = 1.5f;

    private void Start()
    {
        GameObject scoreManagerObj = GameObject.Find("ScoreManager");

        if (scoreManagerObj != null)
        {
            scoreScr = scoreManagerObj.GetComponent<ScoreManager>();
        }
    }

    private void Update()
    {
        if (countingDown)
        {
            resetCounter -= Time.deltaTime;

            if (resetCounter <= 0)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (mode == GoalMode.playerGoal)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerHurtbox"))
            {
                PlayerController playerScr = other.gameObject.GetComponentInParent<PlayerController>();

                if (playerScr != null)
                {
                    if (playerScr.hasBall)
                    {
                        scoreScr.playerScore++;
                        startResetCount();
                        Debug.Log("Player has scored!!!");
                    }
                }
            }
        }
        else if (mode == GoalMode.enemyGoal)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyMovement enemyScr = other.gameObject.GetComponent<EnemyMovement>();

                if (enemyScr != null)
                {
                    if (enemyScr.currentMode == EnemyMovement.EnemyStates.HasBall)
                    {
                        scoreScr.enemyScore++;
                        startResetCount();
                        Debug.Log("Enemy has scored!!!");
                    }
                }
            }
        }
    }

    private void startResetCount()
    {
        countingDown = true;
        resetCounter = resetTime;
    }
}
