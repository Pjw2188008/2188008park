using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject flag; // ��� ������Ʈ
    public Transform[] stagePositions; // �������� �߽� ��ġ �迭
    public float stageRadius = 10f; // �������� �ݰ�
    private List<int> remainingStages = new List<int>(); // ���� �������� �ε���
    private int currentStageIndex = 0; // ���� �������� �ε���
    private List<GameObject> monstersInStage = new List<GameObject>(); // ���� ���������� ���� ����Ʈ

    void Start()
    {
        // ��� ���������� �ʱ�ȭ
        for (int i = 0; i < stagePositions.Length; i++)
        {
            remainingStages.Add(i);
        }

        // ��� ��Ȱ��ȭ
        if (flag != null) flag.SetActive(false);

        // ù ��° �������� ����
        MoveToStage(0);
    }

    void Update()
    {
        // ���� �������� ���� ����Ʈ�� ���� (null ����)
        monstersInStage.RemoveAll(monster => monster == null);

        // ��� ���Ͱ� ���ŵǾ����� ��� Ȱ��ȭ
        if (monstersInStage.Count == 0 && flag != null && !flag.activeSelf)
        {
            flag.SetActive(true);
            Debug.Log("All monsters defeated in the current stage! Flag activated!");
        }
    }

    void MoveToStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stagePositions.Length)
        {
            Debug.LogError("Invalid stage index!");
            return;
        }

        // ���� ���������� ������Ʈ
        currentStageIndex = stageIndex;

        // ���� �������� ���� �ʱ�ȭ
        UpdateMonstersInStage();

        // �÷��̾ �ش� �������� ��ġ�� �̵�
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = stagePositions[stageIndex].position;
            Debug.Log($"Moved to stage {stageIndex + 1}");
        }
        else
        {
            Debug.LogError("Player not found!");
        }

        // ��� ��Ȱ��ȭ
        if (flag != null)
        {
            flag.SetActive(false);
        }
    }

    void UpdateMonstersInStage()
    {
        // ���� �������� �� ���� �ʱ�ȭ
        monstersInStage.Clear();
        GameObject[] allMonsters = GameObject.FindGameObjectsWithTag("Enemy1");
        foreach (GameObject monster in allMonsters)
        {
            float distance = Vector3.Distance(stagePositions[currentStageIndex].position, monster.transform.position);
            if (distance <= stageRadius) // �ݰ� ���� �ִ� ���͸� �߰�
            {
                monstersInStage.Add(monster);
            }
        }

        Debug.Log($"Monsters in stage {currentStageIndex + 1}: {monstersInStage.Count}");
    }

    public void ClearStage()
    {
        Debug.Log($"Stage {currentStageIndex + 1} Cleared!");

        // ���� ���������� ���� ������������ ����
        if (remainingStages.Contains(currentStageIndex))
        {
            remainingStages.Remove(currentStageIndex);
        }

        // ��� ��Ȱ��ȭ
        if (flag != null)
        {
            flag.SetActive(false);
        }

        // ���� ���������� �̵�
        if (remainingStages.Count > 0)
        {
            MoveToRandomStage();
        }
        else
        {
            Debug.Log("All stages cleared!");
            // ���� ���� �Ǵ� ��Ÿ ���� ����
        }
    }

    void MoveToRandomStage()
    {
        if (remainingStages.Count == 0)
        {
            Debug.LogWarning("No remaining stages to move to!");
            return;
        }

        // �������� ���� �������� ����
        int randomIndex = Random.Range(0, remainingStages.Count);
        int nextStageIndex = remainingStages[randomIndex];

        // ���õ� ���������� �̵�
        MoveToStage(nextStageIndex);
    }

    // �������� �ݰ��� ǥ���ϴ� Gizmos
    private void OnDrawGizmos()
    {
        if (stagePositions == null) return;

        // ��� �������� �ݰ� �׸���
        for (int i = 0; i < stagePositions.Length; i++)
        {
            if (stagePositions[i] != null)
            {
                Gizmos.color = (i == currentStageIndex) ? Color.green : Color.red;
                Gizmos.DrawWireSphere(stagePositions[i].position, stageRadius);
            }
        }
    }
}
