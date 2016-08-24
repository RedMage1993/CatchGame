using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    public Text scoreText;
    public int ballValue;

    private int score;

	// Use this for initialization
	void Start () {
        score = 0;
        UpdateScoreText();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        score += ballValue;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score:\n" + score;
    }
}
