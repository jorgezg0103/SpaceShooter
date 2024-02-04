using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _health = 4;
    [SerializeField] float _speed = 3;

    private void MovePlayerToMousePosition() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, _speed * Time.deltaTime);
    }

    private void Update() {
        MovePlayerToMousePosition();
    }

}
