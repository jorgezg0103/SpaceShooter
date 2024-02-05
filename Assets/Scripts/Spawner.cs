using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private enum PickUps {
        Health
    }

    private enum Enemies {
        Scout
    }

    private PoolController _poolManager;

    [Header("Spawn Settings")]
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float _minSpawnTime = 2f;
    [SerializeField] private float _maxSpawmTime = 3f;
    [SerializeField] private float _minSpawnTimeLimit = 1f;
    [SerializeField] private float _maxSpawmTimeLimit = 2f;
    [SerializeField] private float _decraseTimePerIteration = 0.001f;
    [SerializeField] private float _spawnPickUpProb = 0.1f;

    private float timeBtwSpawn = 2f;
    private float timer = 0;
    private bool spawnOff = false;

    private void Awake() {
        _poolManager = gameObject.GetComponent<PoolController>();
    }

    private void OnEnable() {

    }

    private void OnDisable() {

    }

    void Start() {
        StartCoroutine(spawn());
    }

    void Update() {
        
    }

    private GameObject getRandomEnemy() {
        int random = Random.Range(0, typeof(Enemies).GetEnumValues().Length);
        GameObject enemy;
        switch(random) {
            case (int) Enemies.Scout:
            enemy = _poolManager.getEnemy();
            break;
            default:
            enemy = _poolManager.getEnemy();
            break;
        }
        return enemy;
    }

    private void spawnEnemies() {
        int quantity = Random.Range(1, spawnPoints.Count);
        List<Transform> spawnPointsToSelect = new List<Transform>(spawnPoints);

        for(int i = 0; i < quantity; i++) {
            int index = Random.Range(0, spawnPointsToSelect.Count);
            GameObject enemy = getRandomEnemy();
            enemy.transform.position = spawnPointsToSelect[index].position;
            spawnPointsToSelect.RemoveAt(index);
        }
    }

    private void spawnPickUp() {
        int random = Random.Range(0, typeof(PickUps).GetEnumValues().Length);
        GameObject pickUp;
        switch(random) {
            case (int) PickUps.Health:
            pickUp = _poolManager.getHealth();
            break;
            default:
            pickUp = _poolManager.getHealth();
            break;
        }
        int index = Random.Range(0, spawnPoints.Count);
        pickUp.transform.position = spawnPoints[index].position;
    }

    private void spawnEntity() {
        float random = Random.Range(0f, 1f);
        if(random > _spawnPickUpProb) {
            spawnEnemies();
        }
        else {
            spawnPickUp();
        }
    }

    private void stopSpawner() {
        spawnOff = true;
    }

    private IEnumerator spawn() {
        while(!spawnOff) {
            timer += Time.deltaTime;
            if(timer >= timeBtwSpawn) {
                timeBtwSpawn = Random.Range(_minSpawnTime, _maxSpawmTime);
                if(_minSpawnTime > _minSpawnTimeLimit) _minSpawnTime -= _decraseTimePerIteration;
                if(_maxSpawmTime > _maxSpawmTimeLimit) _maxSpawmTime -= _decraseTimePerIteration;
                spawnEntity();
                timer = 0;
            }
            yield return null;
        }
    }

}
