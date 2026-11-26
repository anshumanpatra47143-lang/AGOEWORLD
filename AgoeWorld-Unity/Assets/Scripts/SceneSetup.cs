using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] private Material groundMaterial;
    [SerializeField] private Material playerMaterial;

    [ContextMenu("Create AgoeWorld Prototype Scene")]
    public void CreatePrototypeScene()
    {
        if (GameObject.Find("Ground") == null)
        {
            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Ground";
            ground.transform.position = Vector3.zero;
            ground.transform.localScale = new Vector3(10f, 1f, 10f);

            if (groundMaterial != null)
                ground.GetComponent<Renderer>().material = groundMaterial;
        }

        GameObject player = GameObject.Find("Player");

        if (player == null)
        {
            player = new GameObject("Player");
            player.transform.position = Vector3.zero;

            CharacterController controller =
                player.AddComponent<CharacterController>();

            controller.height = 2f;
            controller.radius = 0.5f;
            controller.center = new Vector3(0f, 1f, 0f);

            player.AddComponent<PlayerController>();

            GameObject visual =
                GameObject.CreatePrimitive(PrimitiveType.Capsule);

            visual.name = "PlayerVisual";
            visual.transform.SetParent(player.transform, false);
            visual.transform.localPosition = new Vector3(0f, 1f, 0f);

            Collider visualCollider = visual.GetComponent<Collider>();

            if (visualCollider != null)
                visualCollider.enabled = false;

            if (playerMaterial != null)
                visual.GetComponent<Renderer>().material = playerMaterial;
        }

        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            mainCamera = cameraObject.AddComponent<Camera>();
            mainCamera.transform.position = new Vector3(0f, 5f, -7f);
        }

        CameraFollow cameraFollow =
            mainCamera.GetComponent<CameraFollow>();

        if (cameraFollow == null)
            cameraFollow = mainCamera.gameObject.AddComponent<CameraFollow>();

        cameraFollow.SetTarget(player.transform);

        Debug.Log("AgoeWorld prototype scene objects created.");
    }
}
