using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // ������ Transform
    public float chaseRange = 5f; // ���� ������ �Ѵ� ����
    public float attackRange = 1.5f; // ���� ����
    public float moveSpeed = 2f; // ���� �̵� �ӵ�
    public float returnSpeed = 2f; // ���� �ڸ��� ���ư��� �ӵ�
    public float attackCooldown = 2f; // ���� ��� �ð�
    public int attackDamage = 10; // ���� ������

    private Vector3 startingPosition; // ���� ���� ��ġ
    private bool isChasing = false; // ������ �Ѵ��� ����
    private float lastAttackTime = 0f; // ������ ���� �ð� ���
    private Animator animator; // �ִϸ�����

    void Start()
    {
        // �÷��̾� �±׸� ���� �÷��̾ �ڵ����� ã���ϴ�.
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // ���� ���� ��ġ�� ����
        startingPosition = transform.position;

        // �ִϸ����� ������Ʈ ��������
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // ������ ���� ���� ���� ��
        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            ChasePlayer();

            // ���� ������ �����ϸ� ���� ����
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        // ������ ���� �ۿ� ���� ��
        else
        {
            isChasing = false;
            ReturnToStart();
        }
    }

    // ������ ���󰡴� �Լ�
    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        // ������ �ִ� �������� ���� ��������Ʈ ȸ��
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // �������� �ٶ󺸰� ����
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // ������ �ٶ󺸰� ����
        }

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // ���� �ڸ��� ���ư��� �Լ�
    void ReturnToStart()
    {
        Vector3 direction = (startingPosition - transform.position).normalized;
        transform.position += direction * returnSpeed * Time.deltaTime;

        // ���� �ڸ� ��ó�� �����ϸ� ����
        if (Vector3.Distance(transform.position, startingPosition) < 0.1f)
        {
            transform.position = startingPosition;
        }
    }

    // ������ �����ϴ� �Լ�
    void AttackPlayer()
    {
        // ������ ��Ÿ�� ������ Ȯ��
        if (Time.time - lastAttackTime > attackCooldown)
        {
            // ���� �ִϸ��̼� ����
            animator.SetTrigger("2_Attack");

            // ������ ���� �ð��� ���� �ð����� ����
            lastAttackTime = Time.time;

            // ���� ���� ���� (�÷��̾�� ������ �ֱ�)
            // player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    // ���� ������ �ð������� Ȯ���ϱ� ���� ����� ǥ�� (������)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // ���� ���� ����� �߰�
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}






