using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_test : MonoBehaviour
{
    private Animator animator;
    public int attackDamage = 10;  // 플레이어가 줄 데미지
    public float attackRange = 1.0f;  // 공격 범위

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(moveX, moveY, 0f);

        // 플레이어의 이동
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

        // 공격 키 입력 (Z키)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("2_Attack");
            Attack();  // 공격 함수 실행
        }
    }

    // 공격 처리 함수
    void Attack()
    {
        // 공격 방향에 있는 몬스터를 바로 감지하기 위해 오버랩 서클(OverlapCircle)을 사용
        Collider2D[] hitMonsters = Physics2D.OverlapCircleAll(transform.position + transform.right * attackRange, 0.5f);

        foreach (var hit in hitMonsters)
        {
            MonsterController monster = hit.GetComponent<MonsterController>();
            if (monster != null)
            {
                // 몬스터의 데미지 애니메이션이 바로 실행되도록 데미지 적용
                monster.TakeDamage(attackDamage);
            }
        }
    }

    // 공격 범위를 시각화하기 위한 디버그 코드 (유니티의 Scene 뷰에서 확인 가능)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.right * attackRange, 0.5f);
    }
}
