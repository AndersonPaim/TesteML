using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyToSpawn
    {
        public ObjectsTag enemySpawnTag;

        public int maxEnemies;
        public int currentEnemies;
    }
    [SerializeField] private float _spawnDelay;

    [SerializeField] private Transform[] _spawnPos;

    [SerializeField] private List<EnemyToSpawn> _enemies;

    public Dictionary<ObjectsTag, EnemyToSpawn> _enemiesDictionary;

    private ObjectPooler _objectPooler;

    private float _totalCurrentEnemies = 0;
    private float _totalMaxEnemies;

    private int _randomEnemy;

    private bool _canSpawn;

    private void Start()
    {
        Initialize();
        SetupDelegates();
    }

    private void OnDestroy()
    {
        RemoveDelegates();
    }

    private void Initialize(){
        
        _objectPooler = GameManager.sInstance.ObjectPooler;
        
        _enemiesDictionary = new Dictionary<ObjectsTag, EnemyToSpawn>();

        for(int i = 0; i < _enemies.Count; i++) //calculate max number of enemies with the max of each enemy 
        {
            _totalMaxEnemies += _enemies[i].maxEnemies;
        }

        foreach(EnemyToSpawn enemyToSpawn in _enemies)
        {
            _enemiesDictionary.Add(enemyToSpawn.enemySpawnTag, enemyToSpawn);
        }

        StartCoroutine(SpawnEnemy());
    }

    private void SetupDelegates()
    {
        Enemy.OnDeath += EnemyDeath;
    }

    private void RemoveDelegates()
    {
        Enemy.OnDeath -= EnemyDeath;
    }

    private void EnemyDeath(ObjectsTag enemy)
    {
        _enemiesDictionary[enemy].currentEnemies--;
        _totalCurrentEnemies--;
    }

    private IEnumerator SpawnEnemy()
    { 
        while(true)
        {
            yield return new WaitForSeconds(_spawnDelay);
                
            if(CanSpawn())
            {
                _enemies[_randomEnemy].currentEnemies++;
                _totalCurrentEnemies++;
                
                GameObject obj = _objectPooler.SpawnFromPool(_enemies[_randomEnemy].enemySpawnTag);
                int randomPos = Random.Range(0, _spawnPos.Length);
                obj.transform.position = _spawnPos[randomPos].position;

                _canSpawn = false;
            }
        }
    }

    private bool CanSpawn()
    {
        if(_totalCurrentEnemies < _totalMaxEnemies)
        {
            while(!_canSpawn) //randomize an enemy until finds one that is not at max number
            {
                _randomEnemy = Random.Range(0, _enemies.Count);

                if(_enemies[_randomEnemy].currentEnemies < _enemies[_randomEnemy].maxEnemies)
                {
                    _canSpawn = true;
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
