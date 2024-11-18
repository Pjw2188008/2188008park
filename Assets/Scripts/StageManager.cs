using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviour
{
    public GameObject flag; // ��� ������Ʈ
    public Transform[] stagePositions; // Ÿ�ϸʿ��� �� ���������� �ֿ� ��ġ
    private List<int> remainingStages = new List<int>(); // ���� �������� �ε��� ����       
    private List<GameObject> Enemy1sInStage = new List<GameObject>();

    void Start()
    {
        // ���� �������� �ʱ�ȭ
        for (int i = 0; i < stagePositions.Length; i++)
        {
            remainingStages.Add(i);
        }

        // ��� ��Ȱ��ȭ
        if (flag != null) flag.SetActive(false);
    }

    void Update()
    {
        // ���� �ִ� ���� ����Ʈ���� null ����
        Enemy1sInStage.RemoveAll(monster => monster == null);

        // ���� �������� �� ��� ���Ͱ� ���ŵǾ��� ��� ��� Ȱ��ȭ
        if (Enemy1sInStage.Count == 0 && flag != null && !flag.activeSelf)
        {
            flag.SetActive(true);
            Debug.Log("All monsters defeated in the current stage! Go to the flag!");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �������� ������ ���� ���͸� ����
        if (other.CompareTag("Enemy1"))
        {
            if (!Enemy1sInStage.Contains(other.gameObject))
            {
                Enemy1sInStage.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �������� �������� ���� ���͸� ����
        if (other.CompareTag("Enemy1"))
        {
            if (Enemy1sInStage.Contains(other.gameObject))
            {
                Enemy1sInStage.Remove(other.gameObject);
            }
        }
    }

    public void ClearStage()
    {
        Debug.Log("Stage Cleared!");
        flag.SetActive(false); // ��� ��Ȱ��ȭ

        // ���� �������� ����
        int currentStage = GetCurrentStageIndex();
        if (remainingStages.Contains(currentStage))
        {
            remainingStages.Remove(currentStage);
        }

        // ���� ���������� �̵�
        if (remainingStages.Count > 0)
        {
            MoveToRandomStage();
        }
        else
        {
            Debug.Log("All stages cleared!");
        }
    }

    void MoveToRandomStage()
    {
        // ���� �������� ����
        int randomIndex = Random.Range(0, remainingStages.Count);
        int nextStageIndex = remainingStages[randomIndex];
        Transform nextStagePosition = stagePositions[nextStageIndex];

        // �÷��̾� �̵�
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = nextStagePosition.position;
        }

        Debug.Log($"Moved to stage {nextStageIndex + 1}");
    }

    int GetCurrentStageIndex()
    {
        // ���� �÷��̾� ��ġ���� ����� �������� ã��
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;

            for (int i = 0; i < stagePositions.Length; i++)
            {
                if (Vector3.Distance(playerPosition, stagePositions[i].position) < 1f)
                {
                    return i;
                }
            }
        }
        return -1; // �� �� ���� ��������
    }
}
