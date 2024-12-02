using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private List<string> stageScenes = new List<string> { "Stage1", "Stage2", "Stage3" }; // �Ϲ� �������� �̸�
    public string bossStage = "Boss Stage"; // ���� �������� �̸�

    private List<string> remainingStages; // Ŭ�������� ���� ��������
    private HashSet<string> clearedStages = new HashSet<string>(); // Ŭ������ ��������

    void Start()
    {
        // �������� �ʱ�ȭ
        remainingStages = new List<string>(stageScenes);
    }

    public void MoveToRandomStage()
    {
        string currentStage = SceneManager.GetActiveScene().name;

        // ���� ���������� Ŭ���� ó��
        if (!clearedStages.Contains(currentStage))
        {
            clearedStages.Add(currentStage);
            remainingStages.Remove(currentStage);
        }

        if (remainingStages.Count > 0)
        {
            LoadRandomStage();
        }
        else
        {
            LoadBossStage();
        }
    }

    private void LoadRandomStage()
    {
        int randomIndex = Random.Range(0, remainingStages.Count);
        string nextStage = remainingStages[randomIndex];
        SceneManager.LoadScene(nextStage);
    }

    private void LoadBossStage()
    {
        SceneManager.LoadScene(bossStage);
    }
}
