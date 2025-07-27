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



        public void SpawnEnemy()
        {
            var newEnemy = Instantiate(enemyPrefab);
            newEnemy.Init(currentMap.mapNavigation.navPoints);
        }

        public void StartBattle()
        {
            spawnedEnemies.Clear();
            StartCoroutine(SpawnEnemyCoroutine());
        }

        IEnumerator SpawnEnemyCoroutine()
        {
            while (GameManager.Instance.CurrentGameState == GameManager.GameState.Playing)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(Random.Range(1, 2f));
            }
        }
    }
}