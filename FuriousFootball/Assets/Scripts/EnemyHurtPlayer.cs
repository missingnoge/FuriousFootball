using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtPlayer : MonoBehaviour
{
    private EnemyMovement parentScr;

    private void Start()
    {
        parentScr = GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerScr;

        if (other.gameObject.CompareTag("PlayerHurtbox"))
        {
            playerScr = other.GetComponentInParent<PlayerController>();

            if (playerScr != null)
            {
                playerScr.hasBall = false;
                playerScr.knockbackPlayer(transform.position, 16f);
                parentScr.knockbackEnemy(other.transform.position, 8f);
            }
        }
    }
}
