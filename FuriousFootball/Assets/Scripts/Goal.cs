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
    private bool entered = false;

    private GameObject[] enemies;
    [SerializeField]  private Vector3[] enem_initialPos;

    private GameObject player;
    private Vector3 plyr_initialPos;

    private GameObject football;
    private Vector3 fb_initialPos;

    private GameObject formManager;
    private FormationManager formManScr;

    [SerializeField] private GameObject fbPrefab;
    [SerializeField] private GameObject enemyPrefab;

    private void Start()
    {
        GameObject scoreManagerObj = GameObject.Find("ScoreManager");

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.Find("PlayerObj");
        football = GameObject.Find("Football");
        formManager = GameObject.Find("FormationManager");

        enem_initialPos = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enem_initialPos[i] = enemies[i].transform.position;
        }

        if (player != null)
        {
            plyr_initialPos = player.transform.position;
        }

        if (football != null)
        {
            fb_initialPos = football.transform.position;
        }

        if (scoreManagerObj != null)
        {
            scoreScr = scoreManagerObj.GetComponent<ScoreManager>();
        }

        if (formManager != null)
        {
            formManScr = formManager.GetComponent<FormationManager>();
        }
    }

    private void Update()
    {
        if (countingDown)
        {
            resetCounter -= Time.deltaTime;

            if (resetCounter <= 0)
            {
                ResetStage();
                countingDown = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!entered)
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
                            entered = true;
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
                            entered = true;
                        }
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

    private void ResetStage()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Enemy")[i]);
        }    

        for (int o = 0; o < enem_initialPos.Length; o++)
        {
            Instantiate(enemyPrefab, enem_initialPos[o], enemyPrefab.transform.rotation);
        }

        player.transform.position = plyr_initialPos;
        Instantiate(fbPrefab, fb_initialPos, fbPrefab.transform.rotation);

        entered = false;
    }

    private bool isAChild(GameObject obj)
    {
        var me = transform;
        var t = obj.transform;

        while(t != null)
        {
            if (t == me) return true;
            t = t.parent;
        }

        return false;
    }
}
