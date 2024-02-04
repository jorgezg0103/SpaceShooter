using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    private Transform[] _bgScreens = new Transform[2];
    private float _screenOffset = 0;
    [SerializeField] private float _speed = 1;

    private void Start() {
        for(int i = 0; i < transform.childCount; i++) {
            _bgScreens[i] = transform.GetChild(i);
        }
        _screenOffset = _bgScreens[1].transform.position.y;
    }

    private void Update() {
        for(int i = 0; i < transform.childCount; i++) {
            if(_bgScreens[i].position.y >= -_screenOffset) {
                _bgScreens[i].Translate(Vector2.left * _speed * Time.deltaTime);
            }
            else {
                _bgScreens[i].position = new Vector2(0, _screenOffset);
            }
        }
    }
}
