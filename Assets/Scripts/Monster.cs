using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 2f; // ���� �̵� �ӵ�
    public float attackRange = 1.5f; // ���� ����
    public float detectionRange = 5f; // �÷��̾� ���� ����
    public int attackDamage = 10; // ���� ������
    public Animator animator; // �ִϸ����� ������Ʈ
    public Transform player; // �÷��̾��� Ʈ������
    private bool isAttacking = false; // ���� ������ ����
    private Vector2 originalPosition; // ���� �ڸ�
    private bool isReturning = false; // ���ư��� ������ ����

    void Start()
    {
        originalPosition = transform.position; // ���� �ڸ� ����
    }

    void Update()
    {
        if (isReturning)
        {
            // ���� �ڸ��� ���ư���
            ReturnToOriginalPosition();
            return; // ���� �ڸ��� ���ư��� ���� �ٸ� ������ ���� ����
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // �÷��̾� ���� ���� ���� ������
        if (distanceToPlayer <= detectionRange)
        {
            // ���� ���� ���� ������ ����
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                StartCoroutine(Attack());
            }
            else if (!isAttacking)
            {
                // �÷��̾�� ����
                MoveTowardsPlayer();
            }
        }
        else
        {
            // �÷��̾ ������ ����� ��� ���� �ڸ��� ���ư���
            isReturning = true;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        // ���� ���ϴ� ����
        if (!IsWallInDirection(direction))
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            Flip(direction); // ���⿡ ���� ���� ȸ��
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("2_Attack"); // ���� �ִϸ��̼� ���

        // �ִϸ��̼� ��� �� ��� ���
        yield return new WaitForSeconds(0.5f); // �ִϸ��̼� ��� �ð��� ���� (���� ����)

        // ���� ������ �÷��̾ �ִ��� Ȯ��
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); // �÷��̾��� Health ��ũ��Ʈ�� �����ɴϴ�.
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage); // ������ �ֱ�
            }
        }

        yield return new WaitForSeconds(0.5f); // ���� ���� �� ��� �ð�
        isAttacking = false;
    }

    void ReturnToOriginalPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);

        // ���� �ڸ��� ���ư��� isReturning�� false�� ����
        if (Vector2.Distance(transform.position, originalPosition) < 0.1f)
        {
            isReturning = false; // ���� �ڸ� ����
        }
    }

    void Flip(Vector2 direction)
    {
        if (direction.x > 0)
        {
            // �÷��̾ �����ʿ� ���� ��
            transform.localScale = new Vector3(-1, 1, 1); // ���������� ���� ����
        }
        else if (direction.x < 0)
        {
            // �÷��̾ ���ʿ� ���� ��
            transform.localScale = new Vector3(1, 1, 1); // �������� ���� ����
        }
    }

    bool IsWallInDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f); // �������� ����ĳ��Ʈ
        return hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"); // �� ���̾ �浹�ϸ� true ��ȯ
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // ���� ���� ����� �߰�
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
