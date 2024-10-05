using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // 유저의 Transform
    public float chaseRange = 5f; // 적이 유저를 쫓는 범위
    public float attackRange = 1.5f; // 공격 범위
    public float moveSpeed = 2f; // 적의 이동 속도
    public float returnSpeed = 2f; // 원래 자리로 돌아가는 속도
    public float attackCooldown = 2f; // 공격 대기 시간
    public int attackDamage = 10; // 공격 데미지

    private Vector3 startingPosition; // 적의 시작 위치
    private bool isChasing = false; // 유저를 쫓는지 여부
    private float lastAttackTime = 0f; // 마지막 공격 시간 기록
    private Animator animator; // 애니메이터

    void Start()
    {
        // 플레이어 태그를 통해 플레이어를 자동으로 찾습니다.
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // 적의 시작 위치를 저장
        startingPosition = transform.position;

        // 애니메이터 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 유저가 범위 내에 있을 때
        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            ChasePlayer();

            // 공격 범위에 도달하면 공격 실행
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        // 유저가 범위 밖에 있을 때
        else
        {
            isChasing = false;
            ReturnToStart();
        }
    }

    // 유저를 따라가는 함수
    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        // 유저가 있는 방향으로 적의 스프라이트 회전
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 오른쪽을 바라보게 설정
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // 왼쪽을 바라보게 설정
        }

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // 원래 자리로 돌아가는 함수
    void ReturnToStart()
    {
        Vector3 direction = (startingPosition - transform.position).normalized;
        transform.position += direction * returnSpeed * Time.deltaTime;

        // 원래 자리 근처에 도착하면 멈춤
        if (Vector3.Distance(transform.position, startingPosition) < 0.1f)
        {
            transform.position = startingPosition;
        }
    }

    // 유저를 공격하는 함수
    void AttackPlayer()
    {
        // 공격이 쿨타임 중인지 확인
        if (Time.time - lastAttackTime > attackCooldown)
        {
            // 공격 애니메이션 실행
            animator.SetTrigger("2_Attack");

            // 마지막 공격 시간을 현재 시간으로 갱신
            lastAttackTime = Time.time;

            // 실제 공격 로직 (플레이어에게 데미지 주기)
            // player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    // 유저 범위를 시각적으로 확인하기 위해 기즈모 표시 (디버깅용)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // 공격 범위 기즈모도 추가
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}






