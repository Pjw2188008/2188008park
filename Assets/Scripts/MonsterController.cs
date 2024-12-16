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
        Debug.Log($"���� ü��: {currentHealth}/{maxHealth}");  // ü�� ���� �α�

        if (currentHealth <= 0)
        {
            Die();  // ü���� 0 �����̸� ���� ó��
        }
    }

    // ���Ͱ� �׾��� �� ����� �Լ�
    void Die()
    {
        Debug.Log("���Ͱ� �׾����ϴ�.");  // ���� ���� �α�

        // AreaManager�� �˸���
        NotifyAreaManager();

        // �ִϸ��̼� ���� �ʿ� �� �Ʒ� �ּ��� Ǯ�� ����� �� ����
        // animator.SetTrigger("4_Death");

        // ���� �θ� ������Ʈ ����
        GameObject parent = transform.root.gameObject;
        Destroy(parent);  // �ֻ��� �θ� ����
    }

    // �θ� ������Ʈ�� AreaManager�� ���� ���� �˸�
    void NotifyAreaManager()
    {
        Transform areaParent = transform.root.parent;  // �ֻ��� �θ��� �θ�(���� ������Ʈ)
        if (areaParent != null)
        {
            AreaManager areaManager = areaParent.GetComponent<AreaManager>();
            if (areaManager != null)
            {
                areaManager.CheckMonsters();  // AreaManager�� ���� ������Ʈ ��û
            }
        }
    }
}