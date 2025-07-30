using UnityEngine;
namespace _ElementalDefense
{
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
        [field: SerializeField] private BattleController battleController;
        [SerializeField] private PlayerModel _playerModel;
        public PlayerModel playerModel => _playerModel;

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
            battleController.StartBattle();
        }


    }

}