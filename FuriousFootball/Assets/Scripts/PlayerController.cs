using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hasBall = false;

    public float baseSpeed;
    public float speed;

    public float stamina = 2f;
    private float stamTimer = 0f;
    public float stamTimerGoal = 360f;

    [SerializeField]
    private KeyCode charge = KeyCode.F;

    public bool charging = false;
    [SerializeField] private float chargeSpdMod = 4f;
    [SerializeField] private float chargeTimerGoal = 40f;
    private float chargeTimer = 0f;
    [SerializeField] private GameObject chargeHitbox;

    private Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        activateChargeBox(charging);

        if (!charging)
        {
            chargeTimer = 0;
            speed = baseSpeed;

            if (Input.GetKeyDown(charge) && stamina > 0)
            {
                charging = true;
                stamina--;
            }
        }
        else
        {
            chargeTimer++;

            if (chargeTimer >= chargeTimerGoal)
            {
                charging = false;
            }

            speed = baseSpeed * chargeSpdMod;
        }

        if (stamina != 2)
        {
            stamTimer++;

            if (stamTimer >= stamTimerGoal)
            {
                stamina++;
                stamTimer = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        myRB.velocity = new Vector3(xInput * speed, myRB.velocity.y, yInput * speed);
    }

    private void activateChargeBox(bool charging)
    {
        if (chargeHitbox != null)
        {
            chargeHitbox.SetActive(charging);
        }
    }
}
