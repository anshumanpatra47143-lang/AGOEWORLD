using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileUIBuilder : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [ContextMenu("Create AgoeWorld Mobile UI")]
    public void CreateMobileUI()
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();

        if (canvas == null)
        {
            GameObject canvasObject = new GameObject("MobileCanvas");
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);

            canvasObject.AddComponent<GraphicRaycaster>();
        }

        if (FindFirstObjectByType<EventSystem>() == null)
        {
            GameObject eventObject = new GameObject("EventSystem");
            eventObject.AddComponent<EventSystem>();
            eventObject.AddComponent<StandaloneInputModule>();
        }

        CreateJoystick(canvas.transform);
        CreateHUD(canvas.transform);

        Debug.Log("AgoeWorld Mobile UI created successfully.");
    }

    private void CreateJoystick(Transform parent)
    {
        if (FindFirstObjectByType<MobileJoystick>() != null)
            return;

        GameObject area = new GameObject("JoystickArea");
        area.transform.SetParent(parent, false);

        RectTransform areaRect = area.AddComponent<RectTransform>();
        areaRect.anchorMin = Vector2.zero;
        areaRect.anchorMax = Vector2.zero;
        areaRect.pivot = Vector2.zero;
        areaRect.anchoredPosition = new Vector2(50f, 50f);
        areaRect.sizeDelta = new Vector2(180f, 180f);

        Image areaImage = area.AddComponent<Image>();
        areaImage.color = new Color(1f, 1f, 1f, 0.2f);

        GameObject handle = new GameObject("JoystickHandle");
        handle.transform.SetParent(area.transform, false);

        RectTransform handleRect = handle.AddComponent<RectTransform>();
        handleRect.anchorMin = new Vector2(0.5f, 0.5f);
        handleRect.anchorMax = new Vector2(0.5f, 0.5f);
        handleRect.pivot = new Vector2(0.5f, 0.5f);
        handleRect.sizeDelta = new Vector2(75f, 75f);

        Image handleImage = handle.AddComponent<Image>();
        handleImage.color = new Color(1f, 1f, 1f, 0.65f);

        MobileJoystick joystick = area.AddComponent<MobileJoystick>();
        joystick.Initialize(areaRect, handleRect);

        if (player == null)
            player = FindFirstObjectByType<PlayerController>();

        if (player != null)
            player.SetMobileJoystick(joystick);
    }

    private void CreateHUD(Transform parent)
    {
        if (FindFirstObjectByType<MobileHUD>() != null)
            return;

        GameObject hudObject = new GameObject("MobileHUD");
        hudObject.transform.SetParent(parent, false);

        MobileHUD hud = hudObject.AddComponent<MobileHUD>();

        Text health = CreateText(
            parent, "HealthText", "HP: 100",
            new Vector2(20f, -25f),
            new Vector2(0f, 1f),
            TextAnchor.UpperLeft
        );

        Text timer = CreateText(
            parent, "MatchTimer", "10:00",
            new Vector2(0f, -25f),
            new Vector2(0.5f, 1f),
            TextAnchor.UpperCenter
        );

        Text status = CreateText(
            parent, "PlayerStatus", "READY",
            new Vector2(-20f, -25f),
            new Vector2(1f, 1f),
            TextAnchor.UpperRight
        );

        hud.Initialize(health, timer, status);
    }

    private Text CreateText(
        Transform parent,
        string objectName,
        string content,
        Vector2 position,
        Vector2 anchor,
        TextAnchor alignment)
    {
        GameObject textObject = new GameObject(objectName);
        textObject.transform.SetParent(parent, false);

        RectTransform rect = textObject.AddComponent<RectTransform>();
        rect.anchorMin = anchor;
        rect.anchorMax = anchor;
        rect.pivot = anchor;
        rect.anchoredPosition = position;
        rect.sizeDelta = new Vector2(220f, 50f);

        Text text = textObject.AddComponent<Text>();
        text.text = content;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 28;
        text.color = Color.white;
        text.alignment = alignment;

        return text;
    }
}
