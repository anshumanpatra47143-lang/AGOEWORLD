using UnityEngine;

public class AgoeWorldBootstrap : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("AgoeWorld initialized successfully.");

        if (Camera.main == null)
        {
            Debug.LogWarning("Main Camera is not assigned in the scene.");
        }
    }
}
