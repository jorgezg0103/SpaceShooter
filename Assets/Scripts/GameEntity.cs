using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEntity : MonoBehaviour
{

    private const int _screenOffset = -14;
    [SerializeField] protected float _speed = 2;

    protected void move() {
        if(transform.position.y >= _screenOffset) {
            transform.Translate(Vector2.down * _speed * Time.deltaTime);
        }
        else {
            this.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        move();
    }
}
