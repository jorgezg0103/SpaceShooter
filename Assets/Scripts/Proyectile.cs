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
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.name == "Bottom") {
            gameObject.SetActive(false);
        }
    }

}
