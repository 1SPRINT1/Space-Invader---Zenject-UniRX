using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Pause : MonoBehaviour
{
    public void PauseGame(GameObject gameObject)
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void ResumeGame(GameObject gameObject)
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
