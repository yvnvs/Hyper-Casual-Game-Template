using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField ] private GameObject StartP, InGameP, NextP, GameOverP;
    [SerializeField ] private float CountDown = 2f;

    [SerializeField] private int asynSceneIndex = 1;

    public enum GameState
    {
        Start,
        Ingame,
        Next,
        GameOver
    }

    public GameState gameState;

    public enum Panels
    {
        Startp,
        Nextp,
        GameOverp,
        InGamep
    }

    private void Start()
    {
        gameState = GameState.Start;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                GameStart();
                break;
            case GameState.Ingame:
                GameIngame();
                break;
            case GameState.Next:
                GameNext();
                break;
            case GameState.GameOver:
                GameOver();
                break;

        }
    }

    void PanelController(Panels currentPanel)
    {
        StartP.SetActive(false);
        InGameP.SetActive(false);
        NextP.SetActive(false);
        GameOverP.SetActive(false);

        switch (currentPanel)
        {
            case Panels.Startp:
                StartP.SetActive(true);
                break;
            case Panels.InGamep:
                InGameP.SetActive(true);
                break;
            case Panels.Nextp:
                NextP.SetActive(true);
                break;
            case Panels.GameOverp:
                GameOverP.SetActive(true);
                break;
        }
    }

    private void GameStart()
    {
        PanelController(Panels.Startp);
        if (Input.anyKeyDown)
        {
            gameState = GameState.Ingame;
        }

        if (SceneManager.sceneCount < 2)
        {
            SceneManager.LoadSceneAsync(asynSceneIndex, LoadSceneMode.Additive);
        }
    }

    private void GameIngame()
    {
        PanelController(Panels.InGamep);
    }

    private void GameNext()
    {
        PanelController(Panels.Nextp);
    }

    private void GameOver()
    {
        CountDown -= Time.deltaTime;
        if (CountDown < 0)
        {
            PanelController(Panels.GameOverp);
        }
    }

    public void RestartButton()
    {
        SceneManager.UnloadSceneAsync(asynSceneIndex);
        gameState = GameState.Start;
    }

    public void NextLevelButton()
    {
        if (SceneManager.sceneCountInBuildSettings == asynSceneIndex + 1)
        {
            SceneManager.UnloadSceneAsync(asynSceneIndex);
            asynSceneIndex = 1;
            SceneManager.LoadSceneAsync(asynSceneIndex, LoadSceneMode.Additive);
        }
        else
        {
            if (SceneManager.sceneCount > 1)
            {
                SceneManager.UnloadSceneAsync(asynSceneIndex);
                asynSceneIndex++;
            }

            SceneManager.LoadSceneAsync(asynSceneIndex, LoadSceneMode.Additive);
        }

        gameState = GameState.Start;
    }
}