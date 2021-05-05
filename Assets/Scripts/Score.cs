using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public GameObject scoreText;
    private int score;

    void Start()
    {
        score = 0;
    }

    void Update()
    {
        scoreText.GetComponent<TMP_Text>().SetText(score.ToString());
    }

    public void IncreaseScore()
    {
        score++;
    }
}
