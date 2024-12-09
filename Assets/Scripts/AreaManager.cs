using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public GameObject nextArea; // ���� ����
    public GameObject barrier; // �̵��� ���� ��

    void Start()
    {
        // �ʱ� ����: �� Ȱ��ȭ
        if (barrier != null)
        {
            barrier.SetActive(true);
        }

        // ���� ���� ��Ȱ��ȭ
        if (nextArea != null)
        {
            nextArea.SetActive(false);
        }
    }

    public void CheckMonsters()
    {
        // ���� ������ �ڽ�(����) ���� Ȯ��
        int remainingMonsters = transform.childCount;
        Debug.Log("���� ���� ��: " + remainingMonsters);

        // ���Ͱ� ��� ���ŵǾ��� ��
        if (remainingMonsters == 0)
        {
            Debug.Log("��� ���Ͱ� ���ŵǾ����ϴ�!");
            UnlockNextArea();
        }
    }

    void UnlockNextArea()
    {
        // ������ ����
        if (barrier != null)
        {
            barrier.SetActive(false);
            Debug.Log("����� ���ŵǾ����ϴ�.");
        }

        // ���� ���� Ȱ��ȭ
        if (nextArea != null)
        {
            nextArea.SetActive(true);
            Debug.Log("���� ������ Ȱ��ȭ�Ǿ����ϴ�.");
        }
    }
}
