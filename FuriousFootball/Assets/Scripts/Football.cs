using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Football : MonoBehaviour
{
    private PlayerController playerScr;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("PlayerObj");

        if (player != null)
        {
            playerScr = player.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerScr.hasBall = true;
        }
        else if (other.CompareTag("Enemy"))
        {
            EnemyMovement enemyScr = other.gameObject.GetComponent<EnemyMovement>();

            if (enemyScr != null)
            {
                enemyScr.currentMode = EnemyMovement.EnemyStates.HasBall;
            }
        }

        Destroy(gameObject);
    }
}
