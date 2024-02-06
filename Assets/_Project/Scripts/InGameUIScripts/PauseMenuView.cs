using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _closePauseMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        _setButtons();
    }

    private void _setButtons()
    {
        _restartButton.onClick.AddListener(delegate { GameManager.Instance.LoadLevel(DataManager.Instance.GetLevel()); });
        _exitButton.onClick.AddListener(delegate { Application.Quit(); });
    }
}
