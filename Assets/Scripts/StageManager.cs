using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public List<string> stageNames = new List<string> {"Stage1", "Stage2", "Stage3", "Boss Stage" }; // ��� �������� �̸� ���
    public Dictionary<string, List<string>> exclusionMap; // ���������� ���� ���
    public static HashSet<string> clearedStages = new HashSet<string>(); // Ŭ������ �������� ���

    private void Start()
    {
        // ���������� ���� ��� ����
        exclusionMap = new Dictionary<string, List<string>>()
        {
            { "Stage0", new List<string> { "BossStage" } }, // Stage0���� ���� ����
            { "Stage1", new List<string> { "Stage0", "BossStage" } }, // Stage1���� Stage0, Boss ����
            { "Stage2", new List<string> { "Stage0", "BossStage" } },  // Stage2���� Stage0, Boss ����
            { "Stage3", new List<string> { "Stage0", "BossStage" } }
        };
    }

    public void MoveToRandomStage()
    {
        string currentStage = SceneManager.GetActiveScene().name; // ���� �������� �̸�
        List<string> excludedStages = exclusionMap.ContainsKey(currentStage) ? exclusionMap[currentStage] : new List<string>();

        List<string> availableStages = new List<string>();

        foreach (var stage in stageNames)
        {
            // Ŭ������ ���������� ���� ���������� ��� ����
            if (!clearedStages.Contains(stage) && !excludedStages.Contains(stage))
            {
                availableStages.Add(stage);
            }
        }

        if (availableStages.Count > 0)
        {
            string nextStage = availableStages[Random.Range(0, availableStages.Count)];
            clearedStages.Add(currentStage); // ���� ���������� Ŭ���� ��Ͽ� �߰�
            SceneManager.LoadScene(nextStage);
        }
        else
        {
            Debug.Log("�̵��� ���������� �����ϴ�!");
        }
    }
}
