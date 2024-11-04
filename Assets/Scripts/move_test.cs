using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_test : MonoBehaviour
{
    private Animator animator;
    public int attackDamage = 10;  // �÷��̾ �� ������
    public float attackRange = 1.0f;  // ���� ����
    public float attackCooldown = 1.0f;  // ���� �ӵ� ���� (�� ����)
    public float hitCooldown = 1.0f;  // �ǰ� �� ���� �Ұ� �ð�

    private float nextAttackTime = 0f;  // ���� ���� ���� �ð�
    private bool isHit = false;  // �ǰ� ���� ����
    private float hitTime;       // �ǰ� ���� �ð�

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // �ǰ� ���°� Ȱ��ȭ�� ��� 1�ʰ� ������ ����
        if (isHit && Time.time >= hitTime + hitCooldown)
        {
            isHit = false;  // �ǰ� ���� ����
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

        // ���� Ű �Է� (ZŰ)�� ���� �ӵ� �� �ǰ� ���� Ȯ��
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= nextAttackTime && !isHit)
        {
            animator.SetTrigger("2_Attack");
            Attack();  // ���� �Լ� ����
            nextAttackTime = Time.time + attackCooldown;  // ���� ��ٿ� ����
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
                Debug.Log("���Ϳ��� �������� �����ϴ�.");  // ����� �α�
                monster.TakeDamage(attackDamage);  // ���ݷ¿� ���� ü�� ����
            }
        }
    }

    // �ǰ� ó�� �Լ� (���Ͱ� ������ �� ȣ��)
    public void TakeHit()
    {
        isHit = true;              // �ǰ� ���� Ȱ��ȭ
        hitTime = Time.time;       // �ǰ� �ð� ���
        Debug.Log("�÷��̾ �ǰݵǾ����ϴ�!");  // ����� �α�
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
