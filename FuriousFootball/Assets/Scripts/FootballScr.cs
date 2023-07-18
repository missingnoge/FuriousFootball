using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballScr : MonoBehaviour
{
    [SerializeField] private bool canGrab = false;

    private float grabTimer = 0f;
    private float grabTimerInit = 0.75f;

    private void Start()
    {
       if (!canGrab)
       {
            grabTimer = grabTimerInit;
       }
    }

    private void Update()
    {
        if (!canGrab)
        {
            grabTimer -= Time.deltaTime;
            
            if (grabTimer <= 0)
            {
                canGrab = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (canGrab)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerHurtbox"))
            {
                PlayerController playerScr = other.gameObject.GetComponentInParent<PlayerController>();

                if (playerScr != null)
                {
                    playerScr.hasBall = true;
                }
            }
            else if (other.gameObject.CompareTag("Enemy"))
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
