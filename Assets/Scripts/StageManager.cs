using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;

    private List<string> stageScenes = new List<string> { "Stage0", "Stage1", "Stage2", "Stage3" }; // 일반 스테이지 이름
    public string gameClearScene = "Game Clear"; // 게임 클리어 씬 이름

    private List<string> remainingStages;
    private HashSet<string> clearedStages;

    private void Awake()
    {
        // Singleton 패턴 적용
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
        // 스테이지 상태 초기화
        remainingStages = new List<string>(stageScenes);
        clearedStages = new HashSet<string>();
        Debug.Log("남은 스테이지 초기화: " + string.Join(", ", remainingStages));
    }

    public void MoveToRandomStage()
    {
        string currentStage = SceneManager.GetActiveScene().name;
        Debug.Log("현재 스테이지: " + currentStage);

        // 현재 스테이지를 클리어 처리
        if (!clearedStages.Contains(currentStage))
        {
            clearedStages.Add(currentStage);

            if (remainingStages.Contains(currentStage))
            {
                remainingStages.Remove(currentStage);
                Debug.Log("클리어된 스테이지 제거: " + currentStage);
            }
        }

        // 모든 스테이지 클리어 시 게임 클리어 씬으로 이동
        if (remainingStages.Count == 0)
        {
            Debug.Log("모든 스테이지 클리어! 게임 클리어 씬으로 이동.");
            LoadGameClearScene();
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
        Debug.Log("다음 스테이지: " + nextStage);
        SceneManager.LoadScene(nextStage);
    }

    private void LoadGameClearScene()
    {
        Debug.Log("게임 클리어 씬으로 이동: " + gameClearScene);
        SceneManager.LoadScene(gameClearScene);
    }
}
