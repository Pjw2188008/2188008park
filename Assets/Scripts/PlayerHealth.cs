using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI Elements")]
    public Slider healthBar; // ü�¹� Slider

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
    }

    /// <summary>
    /// �÷��̾ �������� ���� �� ȣ��Ǵ� �޼���
    /// </summary>
    /// <param name="damage">�Դ� ������ ��</param>
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

    /// <summary>
    /// �÷��̾ �׾��� �� ȣ��Ǵ� �޼���
    /// </summary>
    void Die()
    {
        Debug.Log("Player has died.");
        // �߰����� ���ӿ��� ó�� ������ ���⿡ �ۼ��ϼ���.
        // ��: ���ӿ��� ȭ�� ǥ��, ĳ���� ��Ȱ��ȭ ��
    }

    /// <summary>
    /// �׽�Ʈ�� Update �޼��� (��: Ű �Է����� ������ �ޱ�)
    /// </summary>
   //void Update()
   //{
        // ����: �����̽� Ű�� ������ �������� ����
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            //TakeDamage(10);
        //}
    //}
}


