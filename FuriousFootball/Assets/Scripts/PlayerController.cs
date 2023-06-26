using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool hasBall = false;

    public float baseSpeed;
    public float speed;
    private Vector2 move;

    public bool charging = false;
    private float chargeSpdMod = 1.50f;
    private float chargeTimer = 0f;
    private float chargeTimerGoal = 30f;

    public void onMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!charging)
        {
            chargeTimer = 0;
            speed = baseSpeed;

            movePlayer();
        }
        else
        {
            chargeTimer += 1;

            if (chargeTimer >= chargeTimerGoal)
            {
                charging = false;
            }

            speed = baseSpeed * chargeSpdMod;
        }
    }

    public void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
