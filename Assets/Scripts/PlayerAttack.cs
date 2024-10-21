using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 20;  // ������ ���ݷ�

    void OnTriggerEnter2D(Collider2D other)
    {
        // ���Ϳ� �浹���� �� ���Ϳ��� ������ �ֱ�
        if (other.CompareTag("Enemy1"))  // ���� �±� Ȯ��
        {
            MonsterController monster = other.GetComponent<MonsterController>();
            if (monster != null)
            {
                monster.TakeDamage(attackDamage);  // ���Ϳ��� ������ ����
            }
        }
    }
}

