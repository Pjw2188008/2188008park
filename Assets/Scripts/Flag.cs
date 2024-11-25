using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    public GameObject flagPrefab; // ��� Prefab�� �����մϴ�.
    public Transform flagSpawnPoint; // ��� ���� ��ġ.

    private GameObject[] monsters; // ���͸� ������ �迭.

    void Update()
    {
        monsters = GameObject.FindGameObjectsWithTag("Monster"); // "Monster" �±׸� ���� ������Ʈ�� ã���ϴ�.

        if (monsters.Length == 0)
        {
            SpawnFlag(); // ��� ���Ͱ� ó���Ǹ� ��� ����.
        }
    }

    private void SpawnFlag()
    {
        if (GameObject.FindWithTag("Flag") == null) // ����� �̹� �����Ǿ����� Ȯ��.
        {
            Instantiate(flagPrefab, flagSpawnPoint.position, Quaternion.identity).tag = "Flag";
        }
    }
}
