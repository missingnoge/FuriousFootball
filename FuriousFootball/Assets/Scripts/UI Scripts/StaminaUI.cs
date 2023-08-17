using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaminaUI : MonoBehaviour
{
    private GameObject playerObj;
    private PlayerController playerScr;
    private TextMeshProUGUI stamVal;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("PlayerObj");

        if (playerObj != null)
        {
            playerScr = playerObj.GetComponent<PlayerController>();
        }

        stamVal = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScr != null)
        {
            stamVal.text = playerScr.stamina.ToString();
        }
    }
}
