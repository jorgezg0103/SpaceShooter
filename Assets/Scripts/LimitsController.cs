using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitsController : MonoBehaviour
{

    private enum Limits {
        Top,
        Bottom,
        Left,
        Right
    };

    private GameObject[] _limits = new GameObject[4];


    void SetLimitsInCameraBoundaries() {
        float topLimitHeight = _limits[(int)Limits.Top].transform.localScale.y / 2;
        float bottomLimitHeight = _limits[(int)Limits.Bottom].transform.localScale.y / 2;

        float leftLimitWidth = _limits[(int)Limits.Left].transform.localScale.x;
        float rightLimitWidth = _limits[(int)Limits.Right].transform.localScale.x;

        _limits[(int)Limits.Top].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0));
        _limits[(int)Limits.Bottom].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0));

        _limits[(int)Limits.Top].transform.Translate(new Vector3(0, topLimitHeight, 0));
        _limits[(int)Limits.Bottom].transform.Translate(new Vector3(0, -bottomLimitHeight, 0));

        _limits[(int)Limits.Left].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(-1, Screen.height / 2, 0));
        _limits[(int)Limits.Right].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0));

        _limits[(int)Limits.Left].transform.Translate(new Vector3(-leftLimitWidth/2, 0, 0));
        _limits[(int)Limits.Right].transform.Translate(new Vector3(rightLimitWidth/2, 0, 0));
    }

    void SetLimitsWidthAndHeight() {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        Vector3 currentTopLimitScale = _limits[(int)Limits.Top].transform.localScale;
        Vector3 currentBottomLimitScale = _limits[(int)Limits.Bottom].transform.localScale;
        currentTopLimitScale.x = width;
        currentBottomLimitScale.x = width;
        _limits[(int)Limits.Top].transform.localScale = currentTopLimitScale;
        _limits[(int)Limits.Bottom].transform.localScale = currentBottomLimitScale;

        Vector3 currentLeftLimitScale = _limits[(int)Limits.Left].transform.localScale;
        Vector3 currentRightLimitScale = _limits[(int)Limits.Right].transform.localScale;
        currentLeftLimitScale.y = height;
        currentRightLimitScale.y = height;
        _limits[(int)Limits.Left].transform.localScale = currentLeftLimitScale;
        _limits[(int)Limits.Right].transform.localScale = currentRightLimitScale;
    }

    void Start()
    {

        for(int i = 0; i < _limits.Length; i++) {
            _limits[i] = new GameObject();
            _limits[i].AddComponent<BoxCollider2D>();
            _limits[i].transform.parent = transform;
            _limits[i].name = ((Limits) i).ToString();
        }

        SetLimitsInCameraBoundaries();
        SetLimitsWidthAndHeight();
    }

}
