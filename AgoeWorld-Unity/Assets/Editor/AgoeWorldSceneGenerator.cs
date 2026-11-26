using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;

public class AgoeWorldSceneGenerator
{
    private const string SceneFolder = "Assets/Scenes";

    [MenuItem("AgoeWorld/Generate All Game Scenes")]
    public static void GenerateAllScenes()
    {
        if (!Directory.Exists(SceneFolder))
            Directory.CreateDirectory(SceneFolder);

        CreateScene("AgoeWorld_MainMenu", SetupMainMenu);
        CreateScene("AgoeWorld_Lobby", SetupLobby);
        CreateScene("AgoeWorld_CharacterSelection", SetupCharacterSelection);
        CreateScene("AgoeWorld_Gameplay", SetupGameplay);

        ConfigureBuildScenes();

        AssetDatabase.Refresh();
        Debug.Log("AgoeWorld: All four scenes generated.");
    }


    private static void ConfigureBuildScenes()
    {
        string[] sceneNames =
        {
            "AgoeWorld_MainMenu",
            "AgoeWorld_Lobby",
            "AgoeWorld_CharacterSelection",
            "AgoeWorld_Gameplay"
        };

        EditorBuildSettingsScene[] scenes =
            new EditorBuildSettingsScene[sceneNames.Length];

        for (int i = 0; i < sceneNames.Length; i++)
        {
            string path = SceneFolder + "/" + sceneNames[i] + ".unity";
            scenes[i] = new EditorBuildSettingsScene(path, true);
        }

        EditorBuildSettings.scenes = scenes;
        Debug.Log("AgoeWorld: Four scenes added to Build Settings.");
    }

    private static void CreateScene(string sceneName, System.Action setup)
    {
        Scene scene = EditorSceneManager.NewScene(
            NewSceneSetup.EmptyScene,
            NewSceneMode.Single
        );

        setup.Invoke();

        string path = SceneFolder + "/" + sceneName + ".unity";
        EditorSceneManager.SaveScene(scene, path);
        Debug.Log("Saved scene: " + path);
    }

    private static void CreateFlowManager()
    {
        GameObject flow = new GameObject("AgoeWorldGameFlow");
        flow.AddComponent<AgoeWorldGameFlow>();
    }

    private static void SetupMainMenu()
    {
        CreateFlowManager();

        GameObject builder = new GameObject("MainMenuBuilder");
        builder.AddComponent<MainMenuBuilder>().CreateMainMenu();
    }

    private static void SetupLobby()
    {
        GameObject builder = new GameObject("LobbyBuilder");
        builder.AddComponent<LobbySceneBuilder>().CreateLobby();

        GameObject character = new GameObject("LobbyCharacterBuilder");
        character.AddComponent<LobbyCharacterBuilder>().CreateCharacter();

        GameObject cameraObject = GameObject.Find("LobbyCamera");
        if (cameraObject != null)
            cameraObject.tag = "MainCamera";

        CreateNavigationUI(
            "LOBBY NAVIGATION",
            new string[] { "CHARACTER", "START MATCH" },
            new AgoeWorldButtonAction[]
            {
                AgoeWorldButtonAction.OpenCharacterSelection,
                AgoeWorldButtonAction.StartGameplay
            }
        );
    }

    private static void SetupCharacterSelection()
    {
        GameObject lobbyEnvironment = new GameObject("CharacterSelectionEnvironment");
        lobbyEnvironment.AddComponent<LobbySceneBuilder>().CreateLobby();

        GameObject cameraObject = GameObject.Find("LobbyCamera");
        if (cameraObject != null)
            cameraObject.tag = "MainCamera";

        GameObject builder = new GameObject("CharacterSelectionBuilder");
        builder.AddComponent<LobbyCharacterBuilder>().CreateCharacter();

        CreateNavigationUI(
            "CHARACTER SELECTION",
            new string[] { "BACK TO LOBBY", "START MATCH" },
            new AgoeWorldButtonAction[]
            {
                AgoeWorldButtonAction.BackToLobby,
                AgoeWorldButtonAction.StartGameplay
            }
        );
    }


    private static void CreateNavigationUI(
        string canvasName,
        string[] labels,
        AgoeWorldButtonAction[] actions)
    {
        GameObject canvasObject = new GameObject(canvasName);
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080f, 1920f);

        canvasObject.AddComponent<GraphicRaycaster>();

        if (Object.FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventObject = new GameObject("EventSystem");
            eventObject.AddComponent<EventSystem>();
            eventObject.AddComponent<StandaloneInputModule>();
        }

        for (int i = 0; i < labels.Length; i++)
        {
            CreateNavigationButton(
                canvasObject.transform,
                labels[i],
                actions[i],
                new Vector2(0f, 230f + i * 145f)
            );
        }
    }

    private static void CreateNavigationButton(
        Transform parent,
        string label,
        AgoeWorldButtonAction action,
        Vector2 position)
    {
        GameObject buttonObject = new GameObject(label);
        buttonObject.transform.SetParent(parent, false);

        RectTransform rect = buttonObject.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0f);
        rect.anchorMax = new Vector2(0.5f, 0f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = new Vector2(650f, 110f);

        Image image = buttonObject.AddComponent<Image>();
        image.color = new Color(0.02f, 0.35f, 0.55f, 0.95f);

        Button button = buttonObject.AddComponent<Button>();
        button.targetGraphic = image;

        AgoeWorldNavigationButton navigation =
            buttonObject.AddComponent<AgoeWorldNavigationButton>();
        navigation.buttonAction = action;

        GameObject labelObject = new GameObject("Label");
        labelObject.transform.SetParent(buttonObject.transform, false);

        RectTransform labelRect = labelObject.AddComponent<RectTransform>();
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;

        Text text = labelObject.AddComponent<Text>();
        text.text = label;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 36;
        text.fontStyle = FontStyle.Bold;
        text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;
    }

    private static void SetupGameplay()
    {
        GameObject setup = new GameObject("GameplaySetup");
        setup.AddComponent<SceneSetup>().CreatePrototypeScene();

        GameObject ui = new GameObject("MobileUIBuilder");
        ui.AddComponent<MobileUIBuilder>().CreateMobileUI();
    }
}
