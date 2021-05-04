using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelHandler : MonoBehaviour
{
    public void StartNextLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
