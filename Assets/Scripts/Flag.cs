using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    public GameObject flagPrefab; // 깃발 Prefab을 연결합니다.
    public Transform flagSpawnPoint; // 깃발 생성 위치.

    private GameObject[] monsters; // 몬스터를 저장할 배열.

    void Update()
    {
        monsters = GameObject.FindGameObjectsWithTag("Monster"); // "Monster" 태그를 가진 오브젝트를 찾습니다.

        if (monsters.Length == 0)
        {
            SpawnFlag(); // 모든 몬스터가 처지되면 깃발 생성.
        }
    }

    private void SpawnFlag()
    {
        if (GameObject.FindWithTag("Flag") == null) // 깃발이 이미 생성되었는지 확인.
        {
            Instantiate(flagPrefab, flagSpawnPoint.position, Quaternion.identity).tag = "Flag";
        }
    }
}
