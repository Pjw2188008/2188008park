using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public List<string> stageNames = new List<string> {"Stage1", "Stage2", "Stage3", "Boss Stage" }; // 모든 스테이지 이름 목록
    public Dictionary<string, List<string>> exclusionMap; // 스테이지별 제외 목록
    public static HashSet<string> clearedStages = new HashSet<string>(); // 클리어한 스테이지 목록

    private void Start()
    {
        // 스테이지별 제외 목록 설정
        exclusionMap = new Dictionary<string, List<string>>()
        {
            { "Stage0", new List<string> { "BossStage" } }, // Stage0에서 보스 제외
            { "Stage1", new List<string> { "Stage0", "BossStage" } }, // Stage1에서 Stage0, Boss 제외
            { "Stage2", new List<string> { "Stage0", "BossStage" } },  // Stage2에서 Stage0, Boss 제외
            { "Stage3", new List<string> { "Stage0", "BossStage" } }
        };
    }

    public void MoveToRandomStage()
    {
        string currentStage = SceneManager.GetActiveScene().name; // 현재 스테이지 이름
        List<string> excludedStages = exclusionMap.ContainsKey(currentStage) ? exclusionMap[currentStage] : new List<string>();

        List<string> availableStages = new List<string>();

        foreach (var stage in stageNames)
        {
            // 클리어한 스테이지와 제외 스테이지를 모두 제외
            if (!clearedStages.Contains(stage) && !excludedStages.Contains(stage))
            {
                availableStages.Add(stage);
            }
        }

        if (availableStages.Count > 0)
        {
            string nextStage = availableStages[Random.Range(0, availableStages.Count)];
            clearedStages.Add(currentStage); // 현재 스테이지를 클리어 목록에 추가
            SceneManager.LoadScene(nextStage);
        }
        else
        {
            Debug.Log("이동할 스테이지가 없습니다!");
        }
    }
}
