using UnityEngine;

public class LobbySceneBuilder : MonoBehaviour
{
    [Header("Lobby Settings")]
    [SerializeField] private Color floorColor =
        new Color(0.035f, 0.055f, 0.09f);

    [SerializeField] private Color platformColor =
        new Color(0.05f, 0.35f, 0.55f);

    [SerializeField] private Color lightColor =
        new Color(0.1f, 0.65f, 1f);

    [ContextMenu("Create AgoeWorld 3D Lobby")]
    public void CreateLobby()
    {
        Transform root = transform.Find("AgoeWorldLobby");

        if (root != null)
            DestroyImmediate(root.gameObject);

        GameObject lobby = new GameObject("AgoeWorldLobby");
        lobby.transform.SetParent(transform, false);

        CreateFloor(lobby.transform);
        CreatePlatform(lobby.transform);
        CreateLobbyWalls(lobby.transform);
        CreateLighting(lobby.transform);
        CreateCamera(lobby.transform);

        Debug.Log("AgoeWorld 3D Lobby created successfully.");
    }

    private Material CreateMaterial(Color color, float metallic = 0f)
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");

        if (shader == null)
            shader = Shader.Find("Standard");

        Material material = new Material(shader);
        material.color = color;
        material.SetFloat("_Metallic", metallic);
        return material;
    }

    private void CreateFloor(Transform parent)
    {
        GameObject floor =
            GameObject.CreatePrimitive(PrimitiveType.Plane);

        floor.name = "LobbyFloor";
        floor.transform.SetParent(parent, false);
        floor.transform.localScale = new Vector3(12f, 1f, 12f);

        floor.GetComponent<Renderer>().material =
            CreateMaterial(floorColor, 0.65f);
    }

    private void CreatePlatform(Transform parent)
    {
        GameObject platform =
            GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        platform.name = "PlayerDisplayPlatform";
        platform.transform.SetParent(parent, false);
        platform.transform.localPosition = new Vector3(0f, 0.2f, 0f);
        platform.transform.localScale = new Vector3(3.5f, 0.2f, 3.5f);

        platform.GetComponent<Renderer>().material =
            CreateMaterial(platformColor, 0.8f);

        GameObject center =
            GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        center.name = "PlatformCenter";
        center.transform.SetParent(parent, false);
        center.transform.localPosition = new Vector3(0f, 0.42f, 0f);
        center.transform.localScale = new Vector3(2.7f, 0.025f, 2.7f);

        center.GetComponent<Renderer>().material =
            CreateMaterial(lightColor, 0.4f);
    }

    private void CreateLobbyWalls(Transform parent)
    {
        CreateWall(parent, "BackWall",
            new Vector3(0f, 3f, 8f),
            new Vector3(18f, 6f, 0.3f));

        CreateWall(parent, "LeftWall",
            new Vector3(-9f, 3f, 0f),
            new Vector3(0.3f, 6f, 16f));

        CreateWall(parent, "RightWall",
            new Vector3(9f, 3f, 0f),
            new Vector3(0.3f, 6f, 16f));
    }

    private void CreateWall(
        Transform parent,
        string objectName,
        Vector3 position,
        Vector3 scale)
    {
        GameObject wall =
            GameObject.CreatePrimitive(PrimitiveType.Cube);

        wall.name = objectName;
        wall.transform.SetParent(parent, false);
        wall.transform.localPosition = position;
        wall.transform.localScale = scale;

        wall.GetComponent<Renderer>().material =
            CreateMaterial(floorColor, 0.3f);
    }

    private void CreateLighting(Transform parent)
    {
        GameObject lightObject = new GameObject("LobbyMainLight");
        lightObject.transform.SetParent(parent, false);
        lightObject.transform.localPosition =
            new Vector3(0f, 7f, -3f);

        Light mainLight = lightObject.AddComponent<Light>();
        mainLight.type = LightType.Directional;
        mainLight.color = lightColor;
        mainLight.intensity = 1.3f;

        lightObject.transform.rotation =
            Quaternion.Euler(45f, -25f, 0f);

        GameObject fillObject = new GameObject("LobbyFillLight");
        fillObject.transform.SetParent(parent, false);
        fillObject.transform.localPosition =
            new Vector3(0f, 4f, 3f);

        Light fillLight = fillObject.AddComponent<Light>();
        fillLight.type = LightType.Point;
        fillLight.color = lightColor;
        fillLight.intensity = 5f;
        fillLight.range = 12f;
    }

    private void CreateCamera(Transform parent)
    {
        GameObject cameraObject = new GameObject("LobbyCamera");
        cameraObject.transform.SetParent(parent, false);
        cameraObject.transform.localPosition =
            new Vector3(0f, 4f, -9f);

        cameraObject.transform.LookAt(
            new Vector3(0f, 1.5f, 0f));

        Camera camera = cameraObject.AddComponent<Camera>();
        camera.fieldOfView = 55f;
        camera.nearClipPlane = 0.1f;
        camera.farClipPlane = 150f;
    }
}
