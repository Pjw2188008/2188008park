using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;

    private List<string> stageScenes = new List<string> { "Stage0", "Stage1", "Stage2", "Stage3" }; // �Ϲ� �������� �̸�
    public string bossStage = "Boss Stage"; // ���� �������� �̸�

    private List<string> remainingStages;
    private HashSet<string> clearedStages;

    private void Awake()
    {
        // Singleton ���� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeStages();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeStages()
    {
        // �������� ���� �ʱ�ȭ
        remainingStages = new List<string>(stageScenes);
        clearedStages = new HashSet<string>();
        Debug.Log("���� �������� �ʱ�ȭ: " + string.Join(", ", remainingStages));
    }

    public void MoveToRandomStage()
    {
        string currentStage = SceneManager.GetActiveScene().name;
        Debug.Log("���� ��������: " + currentStage);

        // ���� ���������� Ŭ���� ó��
        if (!clearedStages.Contains(currentStage))
        {
            clearedStages.Add(currentStage);

            if (remainingStages.Contains(currentStage))
            {
                remainingStages.Remove(currentStage);
                Debug.Log("Ŭ����� �������� ����: " + currentStage);
            }
        }

        // ��� �������� Ŭ���� �� ���� ���������� �̵�
        if (remainingStages.Count == 0)
        {
            Debug.Log("��� �������� Ŭ����! ���� ���������� �̵�.");
            LoadBossStage();
        }
        else
        {
            LoadRandomStage();
        }
    }

    private void LoadRandomStage()
    {
        int randomIndex = Random.Range(0, remainingStages.Count);
        string nextStage = remainingStages[randomIndex];
        Debug.Log("���� ��������: " + nextStage);
        SceneManager.LoadScene(nextStage);
    }

    private void LoadBossStage()
    {
        Debug.Log("���� ���������� �̵�: " + bossStage);
        SceneManager.LoadScene("Boss Stage");
    }
}
