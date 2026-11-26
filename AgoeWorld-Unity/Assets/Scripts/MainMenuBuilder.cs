using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuBuilder : MonoBehaviour
{
    [SerializeField] private Color backgroundColor =
        new Color(0.035f, 0.055f, 0.09f, 1f);

    [SerializeField] private Color accentColor =
        new Color(0.1f, 0.75f, 0.95f, 1f);

    [ContextMenu("Create AgoeWorld Main Menu")]
    public void CreateMainMenu()
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();

        if (canvas == null)
        {
            GameObject canvasObject = new GameObject("MainMenuCanvas");
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080f, 1920f);

            canvasObject.AddComponent<GraphicRaycaster>();
        }

        if (FindFirstObjectByType<EventSystem>() == null)
        {
            GameObject eventObject = new GameObject("EventSystem");
            eventObject.AddComponent<EventSystem>();
            eventObject.AddComponent<StandaloneInputModule>();
        }

        Transform existing = canvas.transform.Find("AgoeWorldMainMenu");

        if (existing != null)
            DestroyImmediate(existing.gameObject);

        GameObject menu = new GameObject("AgoeWorldMainMenu");
        menu.transform.SetParent(canvas.transform, false);

        RectTransform menuRect = menu.AddComponent<RectTransform>();
        menuRect.anchorMin = Vector2.zero;
        menuRect.anchorMax = Vector2.one;
        menuRect.offsetMin = Vector2.zero;
        menuRect.offsetMax = Vector2.zero;

        Image background = menu.AddComponent<Image>();
        background.color = backgroundColor;

        CreateText(menu.transform, "AGOE", 92,
            new Vector2(0f, 520f), accentColor);

        CreateText(menu.transform, "WORLD", 92,
            new Vector2(0f, 420f), Color.white);

        CreateText(menu.transform, "ENTER THE WORLD", 28,
            new Vector2(0f, 325f), new Color(0.7f, 0.8f, 0.9f));

        CreateButton(menu.transform, "PLAY", "PlayButton",
            new Vector2(0f, 80f), accentColor);

        CreateButton(menu.transform, "PROFILE", "ProfileButton",
            new Vector2(0f, -70f), new Color(0.12f, 0.2f, 0.3f));

        CreateButton(menu.transform, "SETTINGS", "SettingsButton",
            new Vector2(0f, -220f), new Color(0.12f, 0.2f, 0.3f));

        CreateText(menu.transform, "AGOeWORLD • MOBILE",
            22, new Vector2(0f, -650f), Color.gray);

        Debug.Log("AgoeWorld Main Menu created successfully.");
    }

    private void CreateText(
        Transform parent,
        string content,
        int fontSize,
        Vector2 position,
        Color color)
    {
        GameObject textObject = new GameObject(content + "Text");
        textObject.transform.SetParent(parent, false);

        RectTransform rect = textObject.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = new Vector2(850f, 120f);

        Text text = textObject.AddComponent<Text>();
        text.text = content;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = fontSize;
        text.fontStyle = FontStyle.Bold;
        text.color = color;
        text.alignment = TextAnchor.MiddleCenter;
    }

    private void CreateButton(
        Transform parent,
        string label,
        string objectName,
        Vector2 position,
        Color color)
    {
        GameObject buttonObject = new GameObject(objectName);
        buttonObject.transform.SetParent(parent, false);

        RectTransform rect = buttonObject.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = new Vector2(600f, 110f);

        Image image = buttonObject.AddComponent<Image>();
        image.color = color;

        Button button = buttonObject.AddComponent<Button>();
        button.targetGraphic = image;

        AgoeWorldNavigationButton navigation =
            buttonObject.AddComponent<AgoeWorldNavigationButton>();

        if (label == "PLAY")
            navigation.buttonAction = AgoeWorldButtonAction.OpenLobby;
        else if (label == "PROFILE")
            navigation.buttonAction = AgoeWorldButtonAction.Profile;
        else if (label == "SETTINGS")
            navigation.buttonAction = AgoeWorldButtonAction.Settings;

        GameObject labelObject = new GameObject("Label");
        labelObject.transform.SetParent(buttonObject.transform, false);

        RectTransform labelRect =
            labelObject.AddComponent<RectTransform>();

        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;

        Text text = labelObject.AddComponent<Text>();
        text.text = label;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 38;
        text.fontStyle = FontStyle.Bold;
        text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;
    }

    private void OnMenuButton(string buttonName)
    {
        Debug.Log("AgoeWorld Menu Button: " + buttonName);

        if (buttonName == "PLAY")
        {
            if (AgoeWorldGameFlow.Instance != null)
                AgoeWorldGameFlow.Instance.OpenLobby();
            else
                Debug.LogError("AgoeWorldGameFlow is not initialized.");
        }
        else if (buttonName == "PROFILE")
        {
            Debug.Log("Profile selected. Profile screen is pending.");
        }
        else if (buttonName == "SETTINGS")
        {
            Debug.Log("Settings selected. Settings screen is pending.");
        }
    }
}
