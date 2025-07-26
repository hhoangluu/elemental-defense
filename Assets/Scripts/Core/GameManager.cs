using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int time = 1;

    public enum GameState
    {
        Prepare,
        Playing,
        GameOver,
        GameWin
    }

    public GameState CurrentGameState { get; private set; } = GameState.Prepare;
    [SerializeField] private GameObject startButton;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        //Todo: Load scene playing
        CurrentGameState = GameState.Playing;
        startButton.SetActive(false);
    }


}

public class ClassB
{
    public void b()
    {
        
        var a = GameManager.Instance.CurrentGameState;
        if (a == GameManager.GameState.Prepare)
        {
            // Do something when the game is in the Prepare state
        }
        else if (a == GameManager.GameState.Playing)
        {
            // Do something when the game is Playing
        }
        else if (a == GameManager.GameState.GameOver)
        {
            // Do something when the game is Over
        }
        else if (a == GameManager.GameState.GameWin)
        {
            // Do something when the game is Won
        }
    }
}
