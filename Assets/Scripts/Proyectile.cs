using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    
    void Update() {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if((collision.tag == "Player" && gameObject.tag == "EnemyBullet") || (collision.tag == "Enemy" && gameObject.tag == "PlayerBullet"))
            gameObject.SetActive(false);
    }

    public void SetEnemyBullet() {
        gameObject.tag = "EnemyBullet";
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation = new Vector3(currentRotation.x, currentRotation.y, 180);
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    public void SetPlayerBullet() {
        gameObject.tag = "PlayerBullet";
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation = new Vector3(currentRotation.x, currentRotation.y, 0);
        transform.rotation = Quaternion.Euler(currentRotation);
    }

}
