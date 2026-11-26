using UnityEngine;
using UnityEngine.UI;

public class MobileHUD : MonoBehaviour
{
    [Header("HUD References")]
    [SerializeField] private Text healthText;
    [SerializeField] private Text matchTimerText;
    [SerializeField] private Text playerStatusText;

    [Header("Match Settings")]
    [SerializeField] private float matchDuration = 600f;
    [SerializeField] private float playerHealth = 100f;

    private float remainingTime;

    public void Initialize(Text health, Text timer, Text status)
    {
        healthText = health;
        matchTimerText = timer;
        playerStatusText = status;

        remainingTime = matchDuration;
        UpdateHUD();
    }

    private void Start()
    {
        remainingTime = matchDuration;
        UpdateHUD();
    }

    private void Update()
    {
        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Mathf.Max(0f, remainingTime);
        }

        UpdateHUD();
    }

    public void SetHealth(float health)
    {
        playerHealth = Mathf.Clamp(health, 0f, 100f);
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (healthText != null)
            healthText.text = "HP: " + Mathf.CeilToInt(playerHealth);

        if (matchTimerText != null)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);

            matchTimerText.text =
                minutes.ToString("00") + ":" +
                seconds.ToString("00");
        }

        if (playerStatusText != null)
        {
            playerStatusText.text =
                playerHealth > 0f ? "READY" : "OUT";
        }
    }
}
