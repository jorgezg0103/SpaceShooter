using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    private Animator _camAnimator;

    private void OnEnable() {
        Player.OnPlayerDeath += GameOver;
    }

    private void OnDisable() {
        Player.OnPlayerDeath -= GameOver;
    }

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }

        _camAnimator = GameObject.Find("MainCamera").GetComponent<Animator>();
    }

    public void ShakeCamera() {
        _camAnimator.SetTrigger("Shake");
    }

    private void GameOver() {
        Debug.Log("WIP");
    }
}
