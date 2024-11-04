using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player �±װ� �ִ� ������Ʈ�� �浹���� ��
        if (other.CompareTag("Player"))
        {
            // PlayerHealth ��ũ��Ʈ ��������
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.HealToFull();  // ü���� �ִ�ġ�� ȸ��
                Destroy(gameObject);  // ���� ������Ʈ ����
            }
        }
    }
}
