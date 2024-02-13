using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public enum UI {
        Hud,
        PauseMenu,
        GameOverMenu,
        MainMenu,
        OptionsMenu,
        CreditsMenu
    }

    private enum HUD {
        Score,
        PauseButton
    }

    private List<GameObject> _uiComponents = new List<GameObject>();

    Transform _score;
    Transform _pauseButton;

    private bool _gamePausedButton = false;
    [SerializeField] private Sprite _play;
    [SerializeField] private Sprite _pause;

    private void Awake() {
        foreach(Transform child in transform) {
            _uiComponents.Add(child.gameObject);
        }
        GetHUDReferences();
    }

    private void ClearUI() {
        foreach(Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }

    public void SetUIComponent(UI component) {
        ClearUI();
        transform.GetChild((int) component).gameObject.SetActive(true);
    }

    public void ChangeScore(int value) {
        _score.GetComponent<TextMeshProUGUI>().text = "SCORE: " + value;
    }

    private void GetHUDReferences() {
        _score = _uiComponents[(int) UI.Hud].transform.GetChild((int) HUD.Score);
        _pauseButton = _uiComponents[(int) UI.Hud].transform.GetChild((int) HUD.PauseButton);
    }

    public void SetGameOverScore(int value) {
        Transform scoreTransform = _uiComponents[(int) UI.GameOverMenu].transform.Find("Score");
        scoreTransform.GetComponent<TextMeshProUGUI>().text = "SCORE: " + value;
    }

    public void TogglePauseButton() {
        if(_gamePausedButton) {
            GameController.Instance.ResumeGame();
            _pauseButton.GetComponent<Image>().sprite = _pause;
            _uiComponents[(int) UI.PauseMenu].SetActive(false);
            _gamePausedButton = false;
        }
        else {
            GameController.Instance.PauseGame();
            _pauseButton.GetComponent<Image>().sprite = _play;
            _uiComponents[(int) UI.PauseMenu].SetActive(true);
            _gamePausedButton = true;
        }
    }

}
