using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : GameEntity
{
    [SerializeField] private int _healthQuantity = 5;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            gameObject.SetActive(false);
        }
    }

    public int getHealth() {
        return _healthQuantity;
    }

    void Start()
    {
        
    }

}
