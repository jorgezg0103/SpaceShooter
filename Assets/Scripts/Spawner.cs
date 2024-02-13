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
    [SerializeField] private float _spawnPickUpProb = 0.1f;

    [Header("Scout Squad Settings")]
    [SerializeField] private int _minSquadUnits = 3;
    [SerializeField] private int _maxSquadUnits = 6;
    [SerializeField] private Waypoints _waypointsValues;

    private float timeBtwSpawn = 2f;
    private float timer = 0;
    private bool spawnOff = false;

    private void Awake() {
        _poolManager = gameObject.GetComponent<PoolController>();
    }

    private void OnEnable() {
        GameController.OnGameStart += StartSpawn;
    }

    private void OnDisable() {
        GameController.OnGameStart -= StartSpawn;
    }

    void Start() {
        
    }

    void Update() {
        
    }

    private void StartSpawn() {
        StartCoroutine(Spawn());
    }

    private Vector2 SelectRandomLocation() {
        int random = Random.Range(0, Screen.width);
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector2(random, 0));
        return new Vector2(screenPos.x, transform.position.y);
    }

    private int GetRandomEnemy() {
        return Random.Range(0, typeof(Enemies).GetEnumValues().Length);
    }

    private void SpawnScoutSquad(int quantity) {
        int randomPathIndex = Random.Range(0, _waypointsValues.paths.Length);
        Vector2[] selectedPath = _waypointsValues.paths[randomPathIndex];
        for(int i = 0; i < quantity; i++) {
            GameObject enemy = _poolManager.GetScout();
            enemy.GetComponent<Scout>().SetScoutPath(selectedPath);
            enemy.transform.position = transform.position + new Vector3(0f, i*2);
        }
    }

    private void SpawnEnemy() {
        int enemyIndex = GetRandomEnemy();
        switch(enemyIndex) {
            case (int) Enemies.Scout:
            int units = Random.Range(_minSquadUnits, _maxSquadUnits);
            SpawnScoutSquad(units);
            break;
            default:
            SpawnScoutSquad(_minSquadUnits);
            break;
        }
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
                SpawnEntity();
                timer = 0;
            }
            yield return null;
        }
    }

}
