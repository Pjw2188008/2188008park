using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player 태그가 있는 오브젝트와 충돌했을 때
        if (other.CompareTag("Player"))
        {
            // PlayerHealth 스크립트 가져오기
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.HealToFull();  // 체력을 최대치로 회복
                Destroy(gameObject);  // 물약 오브젝트 삭제
            }
        }
    }
}
