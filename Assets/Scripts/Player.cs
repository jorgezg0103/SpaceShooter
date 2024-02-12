using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _health = 4;
    [SerializeField] float _speed = 3;
    private PoolController _poolController;

    private float _bulletOffset = 0.6f;

    private void Awake() {
        _poolController = GameObject.Find("Spawner").GetComponent<PoolController>();
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
            _health--;
            if(_health <= 0) {
                Debug.Log("Game Over");
            }
        }
        if(collision.gameObject.tag == "Health") {
            _health += collision.gameObject.GetComponent<Health>().getHealth();
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
    }

}
