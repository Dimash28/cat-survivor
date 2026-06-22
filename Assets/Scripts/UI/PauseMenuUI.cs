using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    

    [SerializeField] private GameObject template;
    [SerializeField] private ConfirmationWindowUI confirmWindowUI;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    
    private void Awake() 
    {
        template.SetActive(false);
    }

    private void Start() 
    {
        G.input.OnEscapePerformed += ShowPauseMenuUI;

        restartButton.onClick.AddListener(() =>
        {
            ShowConfirmationWindow(ConfirmationWindowUI.ButtonAction.Restart);
        });

        exitButton.onClick.AddListener(() =>
        {
            ShowConfirmationWindow(ConfirmationWindowUI.ButtonAction.Exit);
        });
    }

    private void ShowPauseMenuUI(object sender, System.EventArgs e)
    {
        template.SetActive(!G.game.IsPaused());
    }

    private void ShowConfirmationWindow(ConfirmationWindowUI.ButtonAction buttonAction)
    {
        confirmWindowUI.gameObject.SetActive(true);
        confirmWindowUI.SetButtonAction(buttonAction);
    }
}
