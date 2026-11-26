using UnityEngine;
using UnityEngine.SceneManagement;

public class AgoeWorldGameFlow : MonoBehaviour
{
    public static AgoeWorldGameFlow Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OpenMainMenu()
    {
        LoadScene("AgoeWorld_MainMenu");
    }

    public void OpenLobby()
    {
        LoadScene("AgoeWorld_Lobby");
    }

    public void OpenCharacterSelection()
    {
        LoadScene("AgoeWorld_CharacterSelection");
    }

    public void StartGameplay()
    {
        LoadScene("AgoeWorld_Gameplay");
    }

    public void LoadScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError(
                "AgoeWorld scene is not available: " + sceneName
            );
        }
    }
}
