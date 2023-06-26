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
    private Transform target;

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
        if (target.gameObject != null)
        {
            dir2target = target.position - transform.position;
            dir2target.Normalize();
            dir2target *= speed;

            myRB.velocity = dir2target;
        }
    }
}
