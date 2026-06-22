using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationWindowUI : MonoBehaviour
{
    public enum ButtonAction
    {
        Restart,
        Exit    
    }

    private const string RESTART_MESSAGE = "Are you sure you want to restart the game?";
    private const string EXIT_MESSAGE = "Are you sure you want to exit the game?";

    [SerializeField] private TextMeshProUGUI confirmMessage;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private ButtonAction buttonAction;

    private void Awake() 
    {
        gameObject.SetActive(false);    
    }

    private void Start() 
    {
        yesButton.onClick.AddListener(() =>
        {
            switch (buttonAction)
            {
                case ButtonAction.Restart:
                    G.game.Restart();
                    break;

                case ButtonAction.Exit:
                    Application.Quit();
                    break;
            }
        });

        noButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }
    
    public void SetButtonAction(ButtonAction buttonAction)
    {
        this.buttonAction = buttonAction;

        switch (buttonAction)
            {
                case ButtonAction.Restart:
                    confirmMessage.text = RESTART_MESSAGE;
                    break;

                case ButtonAction.Exit:
                    confirmMessage.text = EXIT_MESSAGE;
                    break;
            }
    }
}
