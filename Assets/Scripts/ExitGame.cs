using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // ������ ��ư Ŭ�� �� ȣ��
    public void ExitApplication()
    {
        Debug.Log("���� ���� ��ư�� ���Ƚ��ϴ�.");

        // ����Ƽ �����Ϳ����� ������ ����, ����� �ۿ����� ���ø����̼� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
