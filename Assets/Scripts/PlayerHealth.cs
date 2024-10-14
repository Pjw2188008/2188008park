using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI Elements")]
    public Slider healthBar; // 체력바 Slider

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
    }

    /// <summary>
    /// 플레이어가 데미지를 받을 때 호출되는 메서드
    /// </summary>
    /// <param name="damage">입는 데미지 양</param>
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

    /// <summary>
    /// 플레이어가 죽었을 때 호출되는 메서드
    /// </summary>
    void Die()
    {
        Debug.Log("Player has died.");
        // 추가적인 게임오버 처리 로직을 여기에 작성하세요.
        // 예: 게임오버 화면 표시, 캐릭터 비활성화 등
    }

    /// <summary>
    /// 테스트용 Update 메서드 (예: 키 입력으로 데미지 받기)
    /// </summary>
   //void Update()
   //{
        // 예시: 스페이스 키를 누르면 데미지를 받음
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            //TakeDamage(10);
        //}
    //}
}


