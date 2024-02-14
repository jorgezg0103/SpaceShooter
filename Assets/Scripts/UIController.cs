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
        CreditsMenu,
        ShopMenu
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

    [Header("Shop UI")]
    [SerializeField] private TextMeshProUGUI _cashText;
    [SerializeField] private TextMeshProUGUI _engineLevelText;
    [SerializeField] private TextMeshProUGUI _blasterLevelText;
    [SerializeField] private Button _engineUpgradeButton;
    [SerializeField] private Button _blasterUpgradeButton;

    private static string[] _costs = { "50", "100", "200", "400", "MAX" };
    private static int _maxLevel = 4;

    private void Awake() {
        foreach(Transform child in transform) {
            _uiComponents.Add(child.gameObject);
        }
        GetHUDReferences();
        RefreshShopUI();
    }

    private void Start() {
        LoadPlayerVolume();
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

    public void UpgradeShip(string component) {
        int cost = int.Parse(_costs[PlayerPrefs.GetInt(component)]);
        if(cost < PlayerPrefs.GetInt("Points")) {
            int currentLevel = PlayerPrefs.GetInt(component);
            if(currentLevel < _maxLevel) {
                PlayerPrefs.SetInt(component, currentLevel + 1);
                RefreshShopUI();
            }
        }
    }

    private void LoadPlayerVolume() {
        GameObject slider = transform.GetChild((int) UI.OptionsMenu).GetChild(2).gameObject;
        slider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume");
    }

    private void RefreshShopUI() {
        _cashText.text = "SCORE: " + PlayerPrefs.GetInt("Points");
        _engineLevelText.text = "ENGINE LEVEL: " + (PlayerPrefs.GetInt("Engine") + 1);
        _blasterLevelText.text = "BLASTER LEVEL: " + (PlayerPrefs.GetInt("Blaster") + 1);
        _engineUpgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _costs[PlayerPrefs.GetInt("Engine")].ToString();
        _blasterUpgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _costs[PlayerPrefs.GetInt("Blaster")].ToString();
    }

}
