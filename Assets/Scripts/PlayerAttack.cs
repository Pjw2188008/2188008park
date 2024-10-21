using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 20;  // 유저의 공격력

    void OnTriggerEnter2D(Collider2D other)
    {
        // 몬스터와 충돌했을 때 몬스터에게 데미지 주기
        if (other.CompareTag("Enemy1"))  // 몬스터 태그 확인
        {
            MonsterController monster = other.GetComponent<MonsterController>();
            if (monster != null)
            {
                monster.TakeDamage(attackDamage);  // 몬스터에게 데미지 적용
            }
        }
    }
}

