using UnityEngine;

public class LobbyCharacterBuilder : MonoBehaviour
{
    [Header("Character Colors")]
    [SerializeField] private Color bodyColor =
        new Color(0.08f, 0.16f, 0.25f);

    [SerializeField] private Color armorColor =
        new Color(0.05f, 0.65f, 0.85f);

    [SerializeField] private Color visorColor =
        new Color(0.15f, 0.8f, 1f);

    [SerializeField] private Color bootColor =
        new Color(0.025f, 0.04f, 0.07f);

    [ContextMenu("Create AgoeWorld Player Avatar")]
    public void CreateCharacter()
    {
        Transform oldCharacter = transform.Find("LobbyCharacter");

        if (oldCharacter != null)
            DestroyImmediate(oldCharacter.gameObject);

        GameObject character = new GameObject("LobbyCharacter");
        character.transform.SetParent(transform, false);
        character.transform.localPosition = new Vector3(0f, 0.52f, 0f);

        CreateBody(character.transform);
        CreateHead(character.transform);
        CreateArms(character.transform);
        CreateLegs(character.transform);

        Debug.Log("AgoeWorld original player avatar created.");
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

    private void CreatePart(
        Transform parent,
        string partName,
        PrimitiveType shape,
        Vector3 position,
        Vector3 scale,
        Color color,
        float metallic = 0f)
    {
        GameObject part = GameObject.CreatePrimitive(shape);
        part.name = partName;
        part.transform.SetParent(parent, false);
        part.transform.localPosition = position;
        part.transform.localScale = scale;

        Collider partCollider = part.GetComponent<Collider>();

        if (partCollider != null)
            partCollider.enabled = false;

        part.GetComponent<Renderer>().material =
            CreateMaterial(color, metallic);
    }

    private void CreateBody(Transform parent)
    {
        CreatePart(
            parent, "AvatarBody", PrimitiveType.Capsule,
            new Vector3(0f, 1.35f, 0f),
            new Vector3(0.65f, 0.7f, 0.42f),
            bodyColor, 0.35f
        );

        CreatePart(
            parent, "ChestArmor", PrimitiveType.Cube,
            new Vector3(0f, 1.52f, 0.19f),
            new Vector3(0.48f, 0.35f, 0.12f),
            armorColor, 0.55f
        );
    }

    private void CreateHead(Transform parent)
    {
        CreatePart(
            parent, "AvatarHead", PrimitiveType.Sphere,
            new Vector3(0f, 2.12f, 0f),
            new Vector3(0.42f, 0.46f, 0.4f),
            bodyColor, 0.2f
        );

        CreatePart(
            parent, "FuturisticVisor", PrimitiveType.Cube,
            new Vector3(0f, 2.16f, 0.18f),
            new Vector3(0.32f, 0.13f, 0.12f),
            visorColor, 0.7f
        );
    }

    private void CreateArms(Transform parent)
    {
        CreatePart(
            parent, "LeftArm", PrimitiveType.Capsule,
            new Vector3(-0.48f, 1.35f, 0f),
            new Vector3(0.22f, 0.65f, 0.22f),
            bodyColor
        );

        CreatePart(
            parent, "RightArm", PrimitiveType.Capsule,
            new Vector3(0.48f, 1.35f, 0f),
            new Vector3(0.22f, 0.65f, 0.22f),
            bodyColor
        );

        CreatePart(
            parent, "LeftShoulder", PrimitiveType.Sphere,
            new Vector3(-0.43f, 1.62f, 0f),
            new Vector3(0.3f, 0.28f, 0.3f),
            armorColor, 0.5f
        );

        CreatePart(
            parent, "RightShoulder", PrimitiveType.Sphere,
            new Vector3(0.43f, 1.62f, 0f),
            new Vector3(0.3f, 0.28f, 0.3f),
            armorColor, 0.5f
        );
    }

    private void CreateLegs(Transform parent)
    {
        CreatePart(
            parent, "LeftLeg", PrimitiveType.Capsule,
            new Vector3(-0.18f, 0.58f, 0f),
            new Vector3(0.25f, 0.65f, 0.28f),
            bodyColor
        );

        CreatePart(
            parent, "RightLeg", PrimitiveType.Capsule,
            new Vector3(0.18f, 0.58f, 0f),
            new Vector3(0.25f, 0.65f, 0.28f),
            bodyColor
        );

        CreatePart(
            parent, "LeftBoot", PrimitiveType.Cube,
            new Vector3(-0.18f, 0.12f, 0.08f),
            new Vector3(0.3f, 0.2f, 0.42f),
            bootColor
        );

        CreatePart(
            parent, "RightBoot", PrimitiveType.Cube,
            new Vector3(0.18f, 0.12f, 0.08f),
            new Vector3(0.3f, 0.2f, 0.42f),
            bootColor
        );
    }
}
