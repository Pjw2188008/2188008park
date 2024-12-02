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
        // �÷��̾� �±׿� �浹 Ȯ��
        if (collision.CompareTag("Player"))
        {
            stageManager.MoveToRandomStage();
        }
    }
}

