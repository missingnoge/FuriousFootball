using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyStates
    {
        GetBall,        // Run towards the ball if it exists
        HasBall,        // This enemy has the ball and will beeline for your side to touchdown
        Cheer           // Enemy has scored and they are now cheering to taunt the player
    }

    public EnemyStates currentMode = EnemyStates.GetBall;

    public float health = 1f;
    public bool stunned = false;

    [SerializeField]
    private GameObject player;
    private PlayerController playerScr;

    private Rigidbody myRB;
    private Vector3 dir2target;
    [SerializeField] private Transform target;
    private float distance2ball;
    private float distance2Player;
    [SerializeField] private GameObject enemyGoal;

    private GameObject football;
    [SerializeField] private GameObject fbPrefab;

    private float hitStunCounter;
    private float hitStunTime = 0.3f;

    public float speed = 6;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerObj");
        enemyGoal = GameObject.Find("EnemyGoal");
        myRB = GetComponent<Rigidbody>();

        if (player != null)
        {
            playerScr = player.GetComponent<PlayerController>();
        }

        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (currentMode == EnemyStates.HasBall)
            {
                Instantiate(fbPrefab, transform);
            }

            playerScr.chargeTimer -= 0.05f;

            Destroy(gameObject);
        }

        if (hitStunCounter <= 0)
        {
            if (FindFootball() && football != null)
            {
                distance2ball = Vector3.Distance(transform.position, football.transform.position);
            }

            if (player != null)
            {
                distance2Player = Vector3.Distance(transform.position, player.transform.position);
            }

            if (currentMode != EnemyStates.HasBall)
            {
                if (distance2ball > distance2Player) // Player is closer to the enemy
                {
                    target = player.transform;
                }
                else // Football is closer to the enemy
                {
                    if (football != null)
                    {
                        target = football.transform;
                    }
                    else if (football == null)
                    {
                        target = player.transform;
                    }
                }
            }
            else
            {
                if (enemyGoal != null)
                {
                    target = enemyGoal.transform;
                }
                else
                {
                    Debug.Log("I can't find the goal!");
                }
            }

            if (target.gameObject != null)
            {
                dir2target = target.position - transform.position;
                dir2target.Normalize();
                dir2target *= speed;

                myRB.velocity = dir2target;
            }
        }
        else
        {
            hitStunCounter -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCharge"))
        {
            if (hitStunCounter <= 0)
            {
                health--;
            }

            if (health > 0)
            {
                playerScr.charging = false;
                knockbackEnemy(other.transform.position, 10f);
                playerScr.knockbackPlayer(transform.position, 8f);
            }
        }
    }

    private bool FindFootball()
    {
        football = GameObject.Find("Football");

        if (football != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void knockbackEnemy(Vector3 otherPos, float force)
    {
        Vector3 kbDir;
        hitStunCounter = hitStunTime;

        kbDir = transform.position - otherPos;
        kbDir.Normalize();
        kbDir *= force;
        myRB.velocity = kbDir;
    }
}
