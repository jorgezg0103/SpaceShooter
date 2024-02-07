using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour {

    private const int _defaultPoolSize = 5;
    [SerializeField] private GameObject _scoutPrefab;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _healthPrefab;

    private List<GameObject> _scoutPool = new List<GameObject>();
    private List<GameObject> _bulletsPool = new List<GameObject>();
    private List<GameObject> _healthPool = new List<GameObject>();

    void Start() {
        GameObject enemies = new GameObject("Enemies");
        GameObject pickUps = new GameObject("PickUps");
        GameObject bullets = new GameObject("Bullets");
        GeneratePool(_scoutPrefab, _scoutPool, enemies.transform);
        GeneratePool(_bulletPrefab, _bulletsPool, bullets.transform);
        GeneratePool(_healthPrefab, _healthPool, pickUps.transform, 3);
    }

    private void GeneratePool(GameObject prefab, List<GameObject> pool, Transform parent, int amount = _defaultPoolSize) {
        for(int i = 0; i < amount; i++) {
            GameObject newObject = Instantiate(prefab, parent);
            newObject.SetActive(false);
            pool.Add(newObject);
        }
    }

    public GameObject GetScout() {
        GameObject newEnemy;
        newEnemy = GetObjectFromPool(_scoutPool, _scoutPrefab);
        newEnemy.GetComponent<BoxCollider2D>().enabled = true;
        return newEnemy;
    }

    public GameObject GetBullet() {
        return GetObjectFromPool(_bulletsPool, _bulletPrefab);
    }

    public GameObject GetHealth() {
        return GetObjectFromPool(_healthPool, _healthPrefab);
    }

    private GameObject GetObjectFromPool(List<GameObject> pool, GameObject prefab) {
        GameObject obj = null;
        foreach(GameObject objInPool in pool) {
            if(!objInPool.activeInHierarchy) {
                obj = objInPool;
            }
        }
        if(obj == null) {
            GameObject newObj = Instantiate(prefab);
            obj = newObj;
            pool.Add(newObj);
        }
        obj.SetActive(true);
        return obj;
    }

}
