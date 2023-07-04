using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyStates
    {
        GetBall,        // Run towards the ball if it exists
        HasBall,        // This enemy has the ball and will beeline for your side to touchdown
        AttackPlayer    // This enemy does not have the ball and will instead attempt to attack the player
    }

    public EnemyStates currentMode;

    [SerializeField]
    private GameObject player;

    private Rigidbody myRB;
    private Vector3 dir2target;
    [SerializeField] private Transform target;
    private float distance2ball;
    private float distance2Player;

    private GameObject football;

    public float speed = 6;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerObj");
        myRB = GetComponent<Rigidbody>();

        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindFootball())
        {
            distance2ball = Vector3.Distance(transform.position, football.transform.position);
        }

        if (player != null)
        {
            distance2Player = Vector3.Distance(transform.position, player.transform.position);
        }

        if (distance2ball > distance2Player) // Player is closer to the enemy
        {
            target = player.transform;
        }
        else // Football is closer to the enemy
        {
            target = football.transform;
        }

        if (target.gameObject != null)
        {
            dir2target = target.position - transform.position;
            dir2target.Normalize();
            dir2target *= speed;

            myRB.velocity = dir2target;
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
