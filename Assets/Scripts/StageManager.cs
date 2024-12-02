using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private List<string> stageScenes = new List<string> { "Stage1", "Stage2", "Stage3" }; // 일반 스테이지 이름
    public string bossStage = "Boss Stage"; // 보스 스테이지 이름

    private List<string> remainingStages; // 클리어하지 않은 스테이지
    private HashSet<string> clearedStages = new HashSet<string>(); // 클리어한 스테이지

    void Start()
    {
        // 스테이지 초기화
        remainingStages = new List<string>(stageScenes);
    }

    public void MoveToRandomStage()
    {
        string currentStage = SceneManager.GetActiveScene().name;

        // 현재 스테이지를 클리어 처리
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
