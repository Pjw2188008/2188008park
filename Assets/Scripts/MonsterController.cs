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
        Debug.Log($"몬스터 체력: {currentHealth}/{maxHealth}");  // 체력 상태 로그

        if (currentHealth <= 0)
        {
            Die();  // 체력이 0 이하이면 죽음 처리
        }
    }

    // 몬스터가 죽었을 때 실행될 함수
    void Die()
    {
        Debug.Log("몬스터가 죽었습니다.");  // 죽음 상태 로그

        // AreaManager에 알리기
        NotifyAreaManager();

        // 애니메이션 실행 필요 시 아래 주석을 풀고 사용할 수 있음
        // animator.SetTrigger("4_Death");

        // 상위 부모 오브젝트 제거
        GameObject parent = transform.root.gameObject;
        Destroy(parent);  // 최상위 부모 제거
    }

    // 부모 오브젝트의 AreaManager에 몬스터 제거 알림
    void NotifyAreaManager()
    {
        Transform areaParent = transform.root.parent;  // 최상위 부모의 부모(구역 오브젝트)
        if (areaParent != null)
        {
            AreaManager areaManager = areaParent.GetComponent<AreaManager>();
            if (areaManager != null)
            {
                areaManager.CheckMonsters();  // AreaManager에 상태 업데이트 요청
            }
        }
    }
}