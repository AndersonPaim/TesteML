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

    [SerializeField] private List<EnemyToSpawn> _enemy;

    public Dictionary<ObjectsTag, EnemyToSpawn> _enemyDictionary;

    private ObjectPooler _objectPooler;

    private float _totalCurrentEnemies = 0; //TODO ajeitar essas variavel
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
        
        _enemyDictionary = new Dictionary<ObjectsTag, EnemyToSpawn>();

        for(int i = 0; i < _enemy.Count; i++)
        {
            _totalMaxEnemies += _enemy[i].maxEnemies;
        }

        foreach(EnemyToSpawn enemyToSpawn in _enemy)
        {
            _enemyDictionary.Add(enemyToSpawn.enemySpawnTag, enemyToSpawn);
        }

        StartCoroutine(SpawnEnemy());
    }

    private void SetupDelegates()
    {
        Enemy.OnEnemyDeath += EnemyDeath;
    }

    private void RemoveDelegates()
    {
        Enemy.OnEnemyDeath -= EnemyDeath;
    }

    private void EnemyDeath(ObjectsTag enemy)
    {
        _enemyDictionary[enemy].currentEnemies--;
        _totalCurrentEnemies--;
    }

    private IEnumerator SpawnEnemy()
    { 
        while(true)
        {
            yield return new WaitForSeconds(_spawnDelay);
                
            if(CanSpawn())
            {
                _enemy[_randomEnemy].currentEnemies++;
                _totalCurrentEnemies++;
                GameObject obj = _objectPooler.SpawnFromPool(_enemy[_randomEnemy].enemySpawnTag);

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
            while(!_canSpawn)
            {
                _randomEnemy = Random.Range(0, _enemy.Count);

                if(_enemy[_randomEnemy].currentEnemies < _enemy[_randomEnemy].maxEnemies)
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
