using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_test : MonoBehaviour
{
    private Animator animator;
    public int attackDamage = 10;  // �÷��̾ �� ������
    public float attackRange = 1.0f;  // ���� ����

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

        // �÷��̾��� �̵�
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

        // ���� Ű �Է� (ZŰ)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("2_Attack");
            Attack();  // ���� �Լ� ����
        }
    }

    // ���� ó�� �Լ�
    void Attack()
    {
        // ���� ���⿡ �ִ� ���͸� �ٷ� �����ϱ� ���� ������ ��Ŭ(OverlapCircle)�� ���
        Collider2D[] hitMonsters = Physics2D.OverlapCircleAll(transform.position + transform.right * attackRange, 0.5f);

        foreach (var hit in hitMonsters)
        {
            MonsterController monster = hit.GetComponent<MonsterController>();
            if (monster != null)
            {
                // ������ ������ �ִϸ��̼��� �ٷ� ����ǵ��� ������ ����
                monster.TakeDamage(attackDamage);
            }
        }
    }

    // ���� ������ �ð�ȭ�ϱ� ���� ����� �ڵ� (����Ƽ�� Scene �信�� Ȯ�� ����)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.right * attackRange, 0.5f);
    }
}
