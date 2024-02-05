using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : MonoBehaviour
{
    private Animator _scoutAnimator;
    private float _deathAnimDuration = 0f;

    [SerializeField] private Waypoints _waypointsValues;
    private Vector2[] _path;
    private int _currentWayPoint = 0;

    private float _speed = 3f;
    private int _health = 1;

    private float _minShootInterval = 1f;
    private float _maxShootInterval = 3f;

    [SerializeField] private GameObject _bullet;
    private float _bulletOffset = -0.6f;

    private void Awake() {
        InitializeAnimParams();
    }

    private void Start() {
        _path = _waypointsValues.paths[2];
        InvokeRepeating("Shoot", 0f, Random.Range(_minShootInterval, _maxShootInterval));
    }

    private void Update() {
        if(_currentWayPoint < _path.Length) {
            MoveToWaypoint(_path[_currentWayPoint]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            _health--;
            if(_health <= 0) {
                StartCoroutine(Death());
            }
        }
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
    }

    private void MoveToWaypoint(Vector2 waypoint) {
        transform.position = Vector2.MoveTowards(transform.position, waypoint, _speed * Time.deltaTime);
        if(transform.position.x == waypoint.x && transform.position.y == waypoint.y) {
            _currentWayPoint++;
        }
    }

    private void Shoot() {
        GameObject newBullet = Instantiate(_bullet);
        newBullet.transform.position = transform.position + new Vector3(0, _bulletOffset, 0);
    }

}
