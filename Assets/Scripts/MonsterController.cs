using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int maxHealth = 100;  // ������ �ִ� ü��
    private int currentHealth;   // ���� ü��
    private Animator animator;   // �ִϸ����� ����

    void Start()
    {
        currentHealth = maxHealth;   // ���� �� �ִ� ü������ ����
        animator = GetComponent<Animator>();  // �ִϸ����� ������Ʈ ��������
    }

    // ���Ϳ��� �������� �ִ� �Լ�
    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;  // �̹� �׾����� �� �̻� ������ ���� ����

        currentHealth -= damage;  // ������ ���

        if (currentHealth <= 0)
        {
            Die();  // ü���� 0 �����̸� ���� ó��
        }
    }

    // ���Ͱ� �׾��� �� ����� �Լ�
    void Die()
    {
        // ���� �ִϸ��̼� ���� �ʿ� �� �Ʒ� �ּ��� Ǯ�� ����� �� ����
        // animator.SetTrigger("4_Death");

        // �ִϸ��̼� ���� ��� ���� �ٷ� ���� ����
        Destroy(gameObject);  // ���� ������Ʈ ��� �ı�
    }
}

