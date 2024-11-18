using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviour
{
    public GameObject flag; // 깃발 오브젝트
    public Transform[] stagePositions; // 타일맵에서 각 스테이지의 주요 위치
    private List<int> remainingStages = new List<int>(); // 남은 스테이지 인덱스 관리       
    private List<GameObject> Enemy1sInStage = new List<GameObject>();

    void Start()
    {
        // 남은 스테이지 초기화
        for (int i = 0; i < stagePositions.Length; i++)
        {
            remainingStages.Add(i);
        }

        // 깃발 비활성화
        if (flag != null) flag.SetActive(false);
    }

    void Update()
    {
        // 남아 있는 몬스터 리스트에서 null 제거
        Enemy1sInStage.RemoveAll(monster => monster == null);

        // 현재 스테이지 내 모든 몬스터가 제거되었을 경우 깃발 활성화
        if (Enemy1sInStage.Count == 0 && flag != null && !flag.activeSelf)
        {
            flag.SetActive(true);
            Debug.Log("All monsters defeated in the current stage! Go to the flag!");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 스테이지 영역에 들어온 몬스터를 추적
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
        // 스테이지 영역에서 나간 몬스터를 제거
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
        flag.SetActive(false); // 깃발 비활성화

        // 현재 스테이지 제거
        int currentStage = GetCurrentStageIndex();
        if (remainingStages.Contains(currentStage))
        {
            remainingStages.Remove(currentStage);
        }

        // 다음 스테이지로 이동
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
        // 랜덤 스테이지 선택
        int randomIndex = Random.Range(0, remainingStages.Count);
        int nextStageIndex = remainingStages[randomIndex];
        Transform nextStagePosition = stagePositions[nextStageIndex];

        // 플레이어 이동
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = nextStagePosition.position;
        }

        Debug.Log($"Moved to stage {nextStageIndex + 1}");
    }

    int GetCurrentStageIndex()
    {
        // 현재 플레이어 위치에서 가까운 스테이지 찾기
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
        return -1; // 알 수 없는 스테이지
    }
}
