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
        StartCoroutine(Spawn());
    }

    void Update() {
        
    }

    private Vector2 SelectRandomLocation() {
        int random = Random.Range(0, Screen.width);
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector2(random, 0));
        return new Vector2(screenPos.x, transform.position.y);
    }

    private GameObject GetRandomEnemy() {
        int random = Random.Range(0, typeof(Enemies).GetEnumValues().Length);
        GameObject enemy;
        switch(random) {
            case (int) Enemies.Scout:
            enemy = _poolManager.GetEnemy();
            break;
            default:
            enemy = _poolManager.GetEnemy();
            break;
        }
        return enemy;
    }

    private void SpawnEnemy() {
        GameObject enemy = GetRandomEnemy();
        enemy.transform.position = SelectRandomLocation();
    }

    private void SpawnPickUp() {
        int random = Random.Range(0, typeof(PickUps).GetEnumValues().Length);
        GameObject pickUp;
        switch(random) {
            case (int) PickUps.Health:
            pickUp = _poolManager.GetHealth();
            break;
            default:
            pickUp = _poolManager.GetHealth();
            break;
        }
        pickUp.transform.position = SelectRandomLocation();
    }

    private void SpawnEntity() {
        float random = Random.Range(0f, 1f);
        if(random > _spawnPickUpProb) {
            SpawnEnemy();
        }
        else {
            SpawnPickUp();
        }
    }

    private void StopSpawner() {
        spawnOff = true;
    }

    private IEnumerator Spawn() {
        while(!spawnOff) {
            timer += Time.deltaTime;
            if(timer >= timeBtwSpawn) {
                timeBtwSpawn = Random.Range(_minSpawnTime, _maxSpawmTime);
                if(_minSpawnTime > _minSpawnTimeLimit) _minSpawnTime -= _decraseTimePerIteration;
                if(_maxSpawmTime > _maxSpawmTimeLimit) _maxSpawmTime -= _decraseTimePerIteration;
                SpawnEntity();
                timer = 0;
            }
            yield return null;
        }
    }

}
