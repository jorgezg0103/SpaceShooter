using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private int _health = 4;
    private int _maxHealth = 4;
    [SerializeField] float _speed = 3;
    private PoolController _poolController;

    private float _bulletOffset = 0.6f;

    private Animator _playerAnimator;

    public static UnityAction OnPlayerDeath;

    [SerializeField] private AudioClip _blasterSound;
    [SerializeField] private AudioClip _playerHitSound;
    [SerializeField] private AudioClip _pickUpSound;

    private void Awake() {
        _poolController = GameObject.Find("Spawner").GetComponent<PoolController>();
        _playerAnimator = gameObject.GetComponent<Animator>();
        _speed += PlayerPrefs.GetInt("Engine"); 
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    private void FixedUpdate() {
        MovePlayerToMousePosition();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "EnemyBullet") {
            DamagePlayer();
        }
        if(collision.gameObject.tag == "Health") {
            AudioSource.PlayClipAtPoint(_pickUpSound, Camera.main.transform.position, PlayerPrefs.GetFloat("Volume"));
            if(_health < _maxHealth) {
                _health += collision.gameObject.GetComponent<Health>().getHealth();
            }
        }
    }

    private void MovePlayerToMousePosition() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, _speed * Time.deltaTime);
    }

    private void Shoot() {
        GameObject newBullet = _poolController.GetBullet();
        newBullet.GetComponent<Proyectile>().SetPlayerBullet();
        newBullet.transform.position = transform.position + new Vector3(0, _bulletOffset, 0);
        AudioSource.PlayClipAtPoint(_blasterSound, Camera.main.transform.position, PlayerPrefs.GetFloat("Volume"));
    }

    private void DamagePlayer() {
        _health--;
        _playerAnimator.SetInteger("Health", _health);
        GameController.Instance.ShakeCamera();
        AudioSource.PlayClipAtPoint(_playerHitSound, Camera.main.transform.position, PlayerPrefs.GetFloat("Volume"));
        if(_health <= 0) {
            OnPlayerDeath.Invoke();
            Destroy(gameObject);
        }
    }

}
