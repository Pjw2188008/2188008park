using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI Elements")]
    public Slider healthBar; // ü�¹� Slider
    public GameObject gameOverPanel; // ���� ���� UI �г�

    [Header("Animator")]
    public Animator animator; // Animator ������Ʈ

    void Start()
    {
        // �ʱ� ü�� ����
        currentHealth = maxHealth;

        // ü�¹� ����
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        else
        {
            Debug.LogError("HealthBar Slider is not assigned in the Inspector.");
        }

        // Animator ����
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on the GameObject.");
            }
        }

        // ���� ���� �г� ��Ȱ��ȭ
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

        // ü�¹� ������Ʈ
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ü�� ���� ����

        // ü�¹� ������Ʈ
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        Debug.Log($"Player took {damage} damage. Current Health: {currentHealth}");

        if (currentHealth > 0)
        {
            // ������ �ִϸ��̼� Ʈ����
            if (animator != null)
            {
                animator.SetTrigger("3_Damaged");
                Debug.Log("Triggered 3_Damaged animation.");
            }
        }
        else
        {
            // ���� �ִϸ��̼� Ʈ����
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

        // ���� ���� �г� Ȱ��ȭ
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // ���� ���� (���� ���� ����)
        Time.timeScale = 0;
    }

    /// <summary>
    /// ������ ������ϴ� �޼���, Stage0 ���� �ٽ� �ε��մϴ�.
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1; // �ð� �帧�� ����ȭ
        SceneManager.LoadScene("Stage0"); // Stage0 ���� �ٽ� �ε�
    }
}
