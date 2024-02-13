using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    private Animator _camAnimator;
    [SerializeField] private GameObject _player;
    private UIController _uiController;

    private int _score = 0;

    public static UnityAction OnGameStart;


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
        _uiController = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    public void ShakeCamera() {
        _camAnimator.SetTrigger("Shake");
    }

    public void GameOver() {
        _uiController.SetGameOverScore(_score);
        _uiController.SetUIComponent(UIController.UI.GameOverMenu);
        int currentPoints = PlayerPrefs.GetInt("Points");
        PlayerPrefs.SetInt("Points", currentPoints + _score);
        Debug.Log("Current points: " + PlayerPrefs.GetInt("Points"));
    }

    public void ResumeGame() {
        Time.timeScale = 1;
    }

    public void PauseGame() {
        Time.timeScale = 0;
    }

    public void ResetGame() {
        SceneManager.LoadScene(0);
    }

    public void InitGame() {
        Instantiate(_player);
        OnGameStart.Invoke();
    }

    public void AddScore() {
        _score += 10;
        _uiController.ChangeScore(_score);
    }

    public void ControlVolume(System.Single vol) {
        PlayerPrefs.SetFloat("Volume",vol);
    }

}
