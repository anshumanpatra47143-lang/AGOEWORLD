using UnityEngine;
using UnityEngine.UI;

public enum AgoeWorldButtonAction
{
    OpenLobby,
    OpenCharacterSelection,
    StartGameplay,
    BackToMainMenu,
    BackToLobby,
    Profile,
    Settings
}

public class AgoeWorldNavigationButton : MonoBehaviour
{
    public AgoeWorldButtonAction buttonAction;

    private void Awake()
    {
        Button button = GetComponent<Button>();

        if (button != null)
            button.onClick.AddListener(Navigate);
    }

    public void Navigate()
    {
        AgoeWorldGameFlow flow = AgoeWorldGameFlow.Instance;

        if (buttonAction == AgoeWorldButtonAction.Profile)
        {
            Debug.Log("Profile screen is pending.");
            return;
        }

        if (buttonAction == AgoeWorldButtonAction.Settings)
        {
            Debug.Log("Settings screen is pending.");
            return;
        }

        if (flow == null)
        {
            Debug.LogError("AgoeWorldGameFlow is not initialized.");
            return;
        }

        switch (buttonAction)
        {
            case AgoeWorldButtonAction.OpenLobby:
                flow.OpenLobby();
                break;

            case AgoeWorldButtonAction.OpenCharacterSelection:
                flow.OpenCharacterSelection();
                break;

            case AgoeWorldButtonAction.StartGameplay:
                flow.StartGameplay();
                break;

            case AgoeWorldButtonAction.BackToMainMenu:
                flow.OpenMainMenu();
                break;

            case AgoeWorldButtonAction.BackToLobby:
                flow.OpenLobby();
                break;
        }
    }
}
