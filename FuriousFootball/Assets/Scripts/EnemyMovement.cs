using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyStates
    {
        GetBall,        // Run towards the ball if it exists
        HasBall,        // This enemy has the ball and will beeline for your side to touchdown
    }

    public EnemyStates currentMode = EnemyStates.GetBall;

    public float health = 1f;

    [SerializeField]
    private GameObject player;

    private Rigidbody myRB;
    private Vector3 dir2target;
    [SerializeField] private Transform target;
    private float distance2ball;
    private float distance2Player;
    [SerializeField] private GameObject enemyGoal;

    private GameObject football;
    [SerializeField] private GameObject fbPrefab;

    public float speed = 6;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerObj");
        enemyGoal = GameObject.Find("EnemyGoal");
        myRB = GetComponent<Rigidbody>();

        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (currentMode == EnemyStates.HasBall)
            {
                Instantiate(fbPrefab);
            }

            Destroy(gameObject);
        }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCharge"))
        {
            health--;
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
}
