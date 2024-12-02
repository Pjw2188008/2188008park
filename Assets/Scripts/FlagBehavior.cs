using UnityEngine;

public class FlagBehavior : MonoBehaviour
{
    private StageManager stageManager;

    private void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 태그와 충돌 확인
        if (collision.CompareTag("Player"))
        {
            stageManager.MoveToRandomStage();
        }
    }
}

