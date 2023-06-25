using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Rigidbody myRB;
    private Vector3 dir2Player;

    public float speed = 6;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerObj");
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            dir2Player = player.transform.position - transform.position;
            dir2Player.Normalize();
            dir2Player *= speed;

            myRB.velocity = dir2Player;
        }
    }
}
