using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoringManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI instructText;

    private float score = 0f;
    private bool started = false;

    private float lastXPosition = -5f;

    void Start()
    {
        scoreText.enabled = false;
        titleText.enabled = true;
        instructText.enabled = true;

        if (scoreText != null)
        {
            scoreText.text = "Score: 0";
        }

        lastXPosition = player.position.x;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            started = true;

            if (started)
            {
                titleText.enabled = false;
                instructText.enabled = false;
                scoreText.enabled = true;

                started = false;
            }
        }

        float currentXPosition = player.position.x;

        if (currentXPosition > lastXPosition)
        {
            score += (currentXPosition - lastXPosition) * 10f;
            lastXPosition = currentXPosition;

            if (scoreText != null)
            {
                scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
            }
        }
    }
}
