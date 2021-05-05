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
        Debug.Log(scoreText.transform.localPosition.y + 10);
        LeanTween.moveLocalY(scoreText, scoreText.transform.localPosition.y + 5, 0.15f).setLoopPingPong(1);
        LeanTween.scale(scoreText, Vector3.one * 1.2f, 0.15f).setLoopPingPong(1);
    }
}
