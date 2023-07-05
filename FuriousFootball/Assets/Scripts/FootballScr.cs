using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballScr : MonoBehaviour
{
    private bool canGrab = false;
    private PlayerController playerScr;

    private float grabTimer = 0f;
    private float grabTimerGoal = 60f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("PlayerObj");

        if (player != null)
        {
            playerScr = player.GetComponent<PlayerController>();
        }
    }

    private void Update()
    {
        if (!canGrab)
        {
            grabTimer++;
            
            if (grabTimer >= grabTimerGoal)
            {
                grabTimer = 0f;
                canGrab = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canGrab)
        {
            if (other.CompareTag("Player"))
            {
                playerScr.hasBall = true;
            }
            else if (other.CompareTag("Enemy"))
            {
                EnemyMovement enemyScr = other.gameObject.GetComponentInParent<EnemyMovement>();

                if (enemyScr != null)
                {
                    enemyScr.currentMode = EnemyMovement.EnemyStates.HasBall;
                }
            }

            Destroy(gameObject);
        }
    }
}
