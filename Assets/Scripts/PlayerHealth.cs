using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI Elements")]
    public Slider healthBar; // 체력바 Slider
    public GameObject gameOverPanel; // 게임 오버 UI 패널

    [Header("Animator")]
    public Animator animator; // Animator 컴포넌트

    void Start()
    {
        // 초기 체력 설정
        currentHealth = maxHealth;

        // 체력바 설정
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        else
        {
            Debug.LogError("HealthBar Slider is not assigned in the Inspector.");
        }

        // Animator 설정
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on the GameObject.");
            }
        }

        // 게임 오버 패널 비활성화
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("GameOverPanel is not assigned in the Inspector.");
        }
    }

    public void HealToFull()
    {
        currentHealth = maxHealth;

        // 체력바 업데이트
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        Debug.Log("Player's health is fully restored!");
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Player is already dead.");
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 체력 범위 제한

        // 체력바 업데이트
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        Debug.Log($"Player took {damage} damage. Current Health: {currentHealth}");

        if (currentHealth > 0)
        {
            // 데미지 애니메이션 트리거
            if (animator != null)
            {
                animator.SetTrigger("3_Damaged");
                Debug.Log("Triggered 3_Damaged animation.");
            }
        }
        else
        {
            // 죽음 애니메이션 트리거
            if (animator != null)
            {
                animator.SetTrigger("4_Death");
                Debug.Log("Triggered 4_Death animation.");
            }
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died.");

        // 게임 오버 패널 활성화
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // 게임 멈춤 (게임 오버 상태)
        Time.timeScale = 0;
    }

    /// <summary>
    /// 게임을 재시작하는 메서드, Stage0 씬을 다시 로드합니다.
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1; // 시간 흐름을 정상화
        SceneManager.LoadScene("Stage0"); // Stage0 씬을 다시 로드
    }
}
