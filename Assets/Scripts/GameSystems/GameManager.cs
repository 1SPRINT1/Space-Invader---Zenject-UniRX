using UnityEngine;
public class GameManager : Pause
{
    [SerializeField] private GameObject _pausePanel;
    public void PauseButton()
    {
        PauseGame(_pausePanel);
    }

    public void ResumeButton()
    {
        ResumeGame(_pausePanel);
    }

    public void RestartButton()
    {
        RestartGame();
    }
}

