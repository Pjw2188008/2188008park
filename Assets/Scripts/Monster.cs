using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 2f; // 몬스터 이동 속도
    public float attackRange = 1.5f; // 공격 범위
    public float detectionRange = 5f; // 플레이어 감지 범위
    public int attackDamage = 10; // 공격 데미지
    public Animator animator; // 애니메이터 컴포넌트
    public Transform player; // 플레이어의 트랜스폼
    private bool isAttacking = false; // 공격 중인지 여부
    private Vector2 originalPosition; // 원래 자리
    private bool isReturning = false; // 돌아가는 중인지 여부

    void Start()
    {
        originalPosition = transform.position; // 원래 자리 저장
    }

    void Update()
    {
        if (isReturning)
        {
            // 원래 자리로 돌아가기
            ReturnToOriginalPosition();
            return; // 원래 자리로 돌아가는 동안 다른 동작을 하지 않음
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 플레이어 감지 범위 내에 있으면
        if (distanceToPlayer <= detectionRange)
        {
            // 공격 범위 내에 있으면 공격
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                StartCoroutine(Attack());
            }
            else if (!isAttacking)
            {
                // 플레이어에게 접근
                MoveTowardsPlayer();
            }
        }
        else
        {
            // 플레이어가 범위를 벗어났을 경우 원래 자리로 돌아가기
            isReturning = true;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        // 벽을 피하는 로직
        if (!IsWallInDirection(direction))
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            Flip(direction); // 방향에 따라 몬스터 회전
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("2_Attack"); // 공격 애니메이션 재생

        // 애니메이션 재생 후 잠시 대기
        yield return new WaitForSeconds(0.5f); // 애니메이션 재생 시간을 설정 (조정 가능)

        // 공격 범위에 플레이어가 있는지 확인
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); // 플레이어의 Health 스크립트를 가져옵니다.
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage); // 데미지 주기
            }
        }

        yield return new WaitForSeconds(0.5f); // 다음 공격 전 대기 시간
        isAttacking = false;
    }

    void ReturnToOriginalPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);

        // 원래 자리로 돌아가면 isReturning을 false로 설정
        if (Vector2.Distance(transform.position, originalPosition) < 0.1f)
        {
            isReturning = false; // 원래 자리 도착
        }
    }

    void Flip(Vector2 direction)
    {
        if (direction.x > 0)
        {
            // 플레이어가 오른쪽에 있을 때
            transform.localScale = new Vector3(-1, 1, 1); // 오른쪽으로 얼굴을 돌림
        }
        else if (direction.x < 0)
        {
            // 플레이어가 왼쪽에 있을 때
            transform.localScale = new Vector3(1, 1, 1); // 왼쪽으로 얼굴을 돌림
        }
    }

    bool IsWallInDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f); // 방향으로 레이캐스트
        return hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"); // 벽 레이어에 충돌하면 true 반환
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // 공격 범위 기즈모도 추가
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
