using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtPlayer : MonoBehaviour
{
    private EnemyMovement parentScr;
    private float fbDropRange = 5f;

    [SerializeField]
    private GameObject fbPrefab;

    private void Start()
    {
        parentScr = GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerScr;
        GameObject playerObj = other.gameObject;

        if (other.gameObject.CompareTag("PlayerHurtbox"))
        {
            playerScr = other.GetComponentInParent<PlayerController>();

            if (playerScr != null)
            {
                playerScr.knockbackPlayer(transform.position, 16f);
                parentScr.knockbackEnemy(other.transform.position, 8f);

                if (playerScr.hasBall)
                {
                    GameObject fb;

                    playerScr.hasBall = false;

                    fb = Instantiate(fbPrefab, new Vector3(playerObj.transform.position.x + Random.Range(-fbDropRange, fbDropRange), 0.64f,
                                    playerObj.transform.position.z + Random.Range(-fbDropRange, fbDropRange)), fbPrefab.transform.rotation);
                    fb.name = "Football";
                }
            }
        }
    }
}
