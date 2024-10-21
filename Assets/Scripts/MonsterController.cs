using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int maxHealth = 100;  // 몬스터의 최대 체력
    private int currentHealth;   // 현재 체력
    private Animator animator;   // 애니메이터 참조

    void Start()
    {
        currentHealth = maxHealth;   // 시작 시 최대 체력으로 설정
        animator = GetComponent<Animator>();  // 애니메이터 컴포넌트 가져오기
    }

    // 몬스터에게 데미지를 주는 함수
    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;  // 이미 죽었으면 더 이상 데미지 받지 않음

        currentHealth -= damage;  // 데미지 계산

        if (currentHealth <= 0)
        {
            Die();  // 체력이 0 이하이면 죽음 처리
        }
    }

    // 몬스터가 죽었을 때 실행될 함수
    void Die()
    {
        // 죽음 애니메이션 실행 필요 시 아래 주석을 풀고 사용할 수 있음
        // animator.SetTrigger("4_Death");

        // 애니메이션 실행 대기 없이 바로 몬스터 삭제
        Destroy(gameObject);  // 몬스터 오브젝트 즉시 파괴
    }
}

