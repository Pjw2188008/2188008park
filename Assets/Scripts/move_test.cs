using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_test : MonoBehaviour
{
    private Animator animator;
    public int attackDamage = 10;  // 플레이어가 줄 데미지
    public float attackRange = 1.0f;  // 공격 범위
    public float attackCooldown = 1.0f;  // 공격 속도 조절 (초 단위)
    public float hitCooldown = 1.0f;  // 피격 후 공격 불가 시간

    private float nextAttackTime = 0f;  // 다음 공격 가능 시간
    private bool isHit = false;  // 피격 상태 여부
    private float hitTime;       // 피격 당한 시간

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 피격 상태가 활성화된 경우 1초가 지나면 해제
        if (isHit && Time.time >= hitTime + hitCooldown)
        {
            isHit = false;  // 피격 상태 해제
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(moveX, moveY, 0f);
        transform.Translate(moveVector.normalized * Time.deltaTime * 5f);

        if (moveX != 0 || moveY != 0)
        {
            animator.SetBool("1_Move", true);
            if (moveX > 0)
                transform.localScale = new Vector2(-1, 1);
            if (moveX < 0)
                transform.localScale = new Vector2(1, 1);
        }
        else
        {
            animator.SetBool("1_Move", false);
        }

        // 공격 키 입력 (Z키)와 공격 속도 및 피격 상태 확인
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= nextAttackTime && !isHit)
        {
            animator.SetTrigger("2_Attack");
            Attack();  // 공격 함수 실행
            nextAttackTime = Time.time + attackCooldown;  // 공격 쿨다운 설정
        }
    }

    void Attack()
    {
        Vector2 attackDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 attackPosition = (Vector2)transform.position + attackDirection * attackRange;
        Vector2 attackSize = new Vector2(1.0f, 0.5f);

        RaycastHit2D[] hitMonsters = Physics2D.BoxCastAll(attackPosition, attackSize, 0f, Vector2.zero);

        foreach (var hit in hitMonsters)
        {
            MonsterController monster = hit.collider.GetComponent<MonsterController>();
            if (monster != null)
            {
                Debug.Log("몬스터에게 데미지를 입힙니다.");  // 디버그 로그
                monster.TakeDamage(attackDamage);  // 공격력에 따라 체력 감소
            }
        }
    }

    // 피격 처리 함수 (몬스터가 공격할 때 호출)
    public void TakeHit()
    {
        isHit = true;              // 피격 상태 활성화
        hitTime = Time.time;       // 피격 시간 기록
        Debug.Log("플레이어가 피격되었습니다!");  // 디버그 로그
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 attackDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 attackPosition = (Vector2)transform.position + attackDirection * attackRange;
        Vector2 attackSize = new Vector2(1.0f, 0.5f);

        Gizmos.DrawWireCube(attackPosition, attackSize);
    }
}
