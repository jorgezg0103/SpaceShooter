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
    private GameObject _triggerCollider;


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
        currentTopLimitScale = new Vector3(width, 0.1f);
        currentBottomLimitScale = new Vector3(width, 0.5f);
        _limits[(int)Limits.Top].transform.localScale = currentTopLimitScale;
        _limits[(int)Limits.Bottom].transform.localScale = currentBottomLimitScale;

        Vector3 currentLeftLimitScale = _limits[(int)Limits.Left].transform.localScale;
        Vector3 currentRightLimitScale = _limits[(int)Limits.Right].transform.localScale;
        currentLeftLimitScale = new Vector3(0.1f, height);
        currentRightLimitScale = new Vector3(0.1f, height);
        _limits[(int)Limits.Left].transform.localScale = currentLeftLimitScale;
        _limits[(int)Limits.Right].transform.localScale = currentRightLimitScale;
    }

    void CreateTriggerLimit() {
        _triggerCollider = new GameObject("TriggerLimit");
        _triggerCollider.transform.parent = transform;
        BoxCollider2D collider = _triggerCollider.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        _triggerCollider.transform.position = _limits[(int) Limits.Bottom].transform.position - new Vector3(0f, 1f);
        _triggerCollider.transform.localScale = _limits[(int) Limits.Bottom].transform.localScale;
    }

    void Start()
    {

        for(int i = 0; i < _limits.Length; i++) {
            _limits[i] = new GameObject(((Limits) i).ToString());
            _limits[i].AddComponent<BoxCollider2D>();
            _limits[i].transform.parent = transform;
        }
        SetLimitsWidthAndHeight();
        SetLimitsInCameraBoundaries();
        CreateTriggerLimit();
    }

}
