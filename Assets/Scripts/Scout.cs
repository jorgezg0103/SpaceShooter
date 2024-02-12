using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : MonoBehaviour
{
    private Animator _scoutAnimator;
    private float _deathAnimDuration = 0f;

    [SerializeField] private PoolController _poolController;
    private Vector2[] _path;
    private int _selectedPath = 0;
    private int _currentWayPoint = 0;

    private float _speed = 3f;
    private int _health = 1;

    private float _bulletOffset = -0.6f;

    float _timer = 0f;
    float _timeBtwShoot = 1f;
    float _minShootInterval = 1f;
    float _maxShootInterval = 3f;

    private void Awake() {
        InitializeAnimParams();
    }

    private void Start() {
        _poolController = GameObject.Find("Spawner").GetComponent<PoolController>();
    }

    private void OnEnable() {
        StartCoroutine(Shoot());
    }

    private void Update() {
        if(_currentWayPoint < _path.Length) {
            MoveToWaypoint(_path[_currentWayPoint]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet") {
            _health--;
            if(_health <= 0) {
                StartCoroutine(Death());
            }
        }
        if(collision.gameObject.name == "TriggerLimit") {
            gameObject.SetActive(false);
            _currentWayPoint = 0;
        }
    }

    public void SetScoutPath(Vector2[] path) {
        _path = path;
    }

    private void InitializeAnimParams() {
        _scoutAnimator = transform.GetComponent<Animator>();
        AnimationClip[] clips = _scoutAnimator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips) {
            if(clip.name == "Destruction") {
                _deathAnimDuration = clip.length;
            }
        }
    }
    private IEnumerator Death() {
        _scoutAnimator.SetBool("isDead", true);
        foreach(Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(_deathAnimDuration);
        gameObject.SetActive(false);
        _currentWayPoint = 0;
        _scoutAnimator.SetBool("isDead", false);
    }

    private void MoveToWaypoint(Vector2 waypoint) {
        transform.position = Vector2.MoveTowards(transform.position, waypoint, _speed * Time.deltaTime);
        if(transform.position.x == waypoint.x && transform.position.y == waypoint.y) {
            _currentWayPoint++;
        }
    }

    private IEnumerator Shoot() {
        while(gameObject.activeSelf) {
            _timer += Time.deltaTime;
            if(_timer >= _timeBtwShoot) {
                _timeBtwShoot = Random.Range(_minShootInterval, _maxShootInterval);
                GameObject newBullet = _poolController.GetBullet();
                newBullet.GetComponent<Proyectile>().SetEnemyBullet();
                newBullet.transform.position = transform.position + new Vector3(0, _bulletOffset, 0);
                _timer = 0;
            }
            yield return null;
        }
    }

}
