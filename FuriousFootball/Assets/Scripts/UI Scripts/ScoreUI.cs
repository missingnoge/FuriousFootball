using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private GameObject scoreManager;
    private ScoreManager managerScr;
    private TextMeshProUGUI scoreVal;

    public bool useEnemyScore = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager");
        scoreVal = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreManager != null)
        {
            managerScr = scoreManager.GetComponent<ScoreManager>();

            if (managerScr != null)
            {
                if (!useEnemyScore)
                {
                    scoreVal.text = managerScr.playerScore.ToString();
                }
                else
                {
                    scoreVal.text = managerScr.enemyScore.ToString();
                }
            }
        }
    }
}
