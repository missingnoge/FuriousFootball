using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hasBall = false;

    public float baseSpeed;
    public float speed;

    // Stamina vars
    public float stamina = 2f;
    private float stamTimer = 0f;
    public float stamTimerGoal = 360f;

    // Control vars
    [SerializeField]
    private KeyCode charge = KeyCode.F;

    // Charge vars
    public bool charging = false;
    [SerializeField] private float chargeSpdMod = 4f;
    [SerializeField] private float chargeTimerGoal = 40f;
    private float chargeTimer = 0f;
    [SerializeField] private GameObject chargeHitbox;
    [SerializeField] private GameObject hurtbox;

    // Knockback vars
    public float kbForce;
    public float kbTime = 1f;
    private float kbCounter;

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
            chargeTimer += Time.deltaTime;

            if (chargeTimer >= chargeTimerGoal)
            {
                charging = false;
                chargeTimer = 0;
            }

            speed = baseSpeed * chargeSpdMod;
        }

        if (stamina != 2)
        {
            stamTimer += Time.deltaTime;

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

    public void knockbackPlayer(Vector3 enemyPos, float force)
    {
        Vector3 kbDir;
        kbCounter = kbTime;
        kbForce = force;

        kbDir = transform.position - enemyPos;
        kbDir.Normalize();
        kbDir *= kbForce;
        myRB.velocity = kbDir;
    }

    private void movePlayer()
    {
        if (kbCounter <= 0)
        {
            float xInput = Input.GetAxisRaw("Horizontal");
            float yInput = Input.GetAxisRaw("Vertical");

            myRB.velocity = new Vector3(xInput * speed, myRB.velocity.y, yInput * speed);
        }
        else
        {
            kbCounter -= Time.deltaTime;
        }
    }

    private void activateChargeBox(bool charging)
    {
        if (chargeHitbox != null && hurtbox != null && !chargeHitbox.activeSelf)
        {
            chargeHitbox.SetActive(charging);
            hurtbox.SetActive(!charging);
        }
    }
}
