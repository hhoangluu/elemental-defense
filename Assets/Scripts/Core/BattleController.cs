using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _ElementalDefense
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] EnemyController _enemyPrefab;
        private EnemyController enemyPrefab => _enemyPrefab;
        [SerializeField] private Map _currentMap;
        private Map currentMap => _currentMap;


        [SerializeField] private List<EnemyController> _spawnedEnemies;
        private List<EnemyController> spawnedEnemies => _spawnedEnemies;
        private BattleSimulationSystem simulation;

        void Awake()
        {
            simulation = new BattleSimulationSystem();
        }
        public void SpawnEnemy()
        {
            var newEnemy = Instantiate(enemyPrefab);
            int id = simulation.AddEnemyModel(newEnemy.ToModel(), newEnemy);
            newEnemy.SetId(id);
        }

        public void StartBattle()
        {
            spawnedEnemies.Clear();
            simulation.SetMap(currentMap);
            StartCoroutine(SpawnEnemyCoroutine());
        }

        void Update()
        {
            if (GameManager.Instance.CurrentGameState == GameManager.GameState.Playing)
            {
                //SpawnEnemy();
                simulation.Simulate(Time.deltaTime);
                simulation.SyncBackToControllers();
            }
        }

        IEnumerator SpawnEnemyCoroutine()
        {
            while (GameManager.Instance.CurrentGameState == GameManager.GameState.Playing)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(Random.Range(0.01f, 0.02f));
                // yield return null;
            }
        }
    }
}