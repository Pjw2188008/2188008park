using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject flag; // 깃발 오브젝트
    public Transform[] stagePositions; // 스테이지 중심 위치 배열
    public float stageRadius = 10f; // 스테이지 반경
    private List<int> remainingStages = new List<int>(); // 남은 스테이지 인덱스
    private int currentStageIndex = 0; // 현재 스테이지 인덱스
    private List<GameObject> monstersInStage = new List<GameObject>(); // 현재 스테이지의 몬스터 리스트

    void Start()
    {
        // 모든 스테이지를 초기화
        for (int i = 0; i < stagePositions.Length; i++)
        {
            remainingStages.Add(i);
        }

        // 깃발 비활성화
        if (flag != null) flag.SetActive(false);

        // 첫 번째 스테이지 시작
        MoveToStage(0);
    }

    void Update()
    {
        // 현재 스테이지 몬스터 리스트를 정리 (null 제거)
        monstersInStage.RemoveAll(monster => monster == null);

        // 모든 몬스터가 제거되었으면 깃발 활성화
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

        // 현재 스테이지를 업데이트
        currentStageIndex = stageIndex;

        // 현재 스테이지 몬스터 초기화
        UpdateMonstersInStage();

        // 플레이어를 해당 스테이지 위치로 이동
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

        // 깃발 비활성화
        if (flag != null)
        {
            flag.SetActive(false);
        }
    }

    void UpdateMonstersInStage()
    {
        // 현재 스테이지 내 몬스터 초기화
        monstersInStage.Clear();
        GameObject[] allMonsters = GameObject.FindGameObjectsWithTag("Enemy1");
        foreach (GameObject monster in allMonsters)
        {
            float distance = Vector3.Distance(stagePositions[currentStageIndex].position, monster.transform.position);
            if (distance <= stageRadius) // 반경 내에 있는 몬스터만 추가
            {
                monstersInStage.Add(monster);
            }
        }

        Debug.Log($"Monsters in stage {currentStageIndex + 1}: {monstersInStage.Count}");
    }

    public void ClearStage()
    {
        Debug.Log($"Stage {currentStageIndex + 1} Cleared!");

        // 현재 스테이지를 남은 스테이지에서 제거
        if (remainingStages.Contains(currentStageIndex))
        {
            remainingStages.Remove(currentStageIndex);
        }

        // 깃발 비활성화
        if (flag != null)
        {
            flag.SetActive(false);
        }

        // 다음 스테이지로 이동
        if (remainingStages.Count > 0)
        {
            MoveToRandomStage();
        }
        else
        {
            Debug.Log("All stages cleared!");
            // 게임 종료 또는 기타 동작 수행
        }
    }

    void MoveToRandomStage()
    {
        if (remainingStages.Count == 0)
        {
            Debug.LogWarning("No remaining stages to move to!");
            return;
        }

        // 랜덤으로 남은 스테이지 선택
        int randomIndex = Random.Range(0, remainingStages.Count);
        int nextStageIndex = remainingStages[randomIndex];

        // 선택된 스테이지로 이동
        MoveToStage(nextStageIndex);
    }

    // 스테이지 반경을 표시하는 Gizmos
    private void OnDrawGizmos()
    {
        if (stagePositions == null) return;

        // 모든 스테이지 반경 그리기
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
