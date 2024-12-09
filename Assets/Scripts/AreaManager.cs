using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public GameObject nextArea; // 다음 구역
    public GameObject barrier; // 이동을 막는 벽

    void Start()
    {
        // 초기 상태: 벽 활성화
        if (barrier != null)
        {
            barrier.SetActive(true);
        }

        // 다음 구역 비활성화
        if (nextArea != null)
        {
            nextArea.SetActive(false);
        }
    }

    public void CheckMonsters()
    {
        // 현재 구역의 자식(몬스터) 개수 확인
        int remainingMonsters = transform.childCount;
        Debug.Log("남은 몬스터 수: " + remainingMonsters);

        // 몬스터가 모두 제거되었을 때
        if (remainingMonsters == 0)
        {
            Debug.Log("모든 몬스터가 제거되었습니다!");
            UnlockNextArea();
        }
    }

    void UnlockNextArea()
    {
        // 베리어 제거
        if (barrier != null)
        {
            barrier.SetActive(false);
            Debug.Log("베리어가 제거되었습니다.");
        }

        // 다음 구역 활성화
        if (nextArea != null)
        {
            nextArea.SetActive(true);
            Debug.Log("다음 구역이 활성화되었습니다.");
        }
    }
}
