using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public GameObject controlKeysUI;

    public void OnStartGameButtonClicked()
    {
        // Stage0 ������ ��ȯ
        SceneManager.LoadScene("Stage0");
    }

    public void OnControlKeysButtonClicked()
    {
        // ����Ű UI Ȱ��ȭ
        if (controlKeysUI != null)
        {
            controlKeysUI.SetActive(true);
        }
    }

    public void OnCloseControlKeysUI()
    {
        // ����Ű UI ��Ȱ��ȭ
        if (controlKeysUI != null)
        {
            controlKeysUI.SetActive(false);
        }
    }

    public void OnExitButtonClicked()
    {
        // ���� ����
        Application.Quit();

        // �����Ϳ��� Ȯ�ο� �޽��� (Unity �����Ϳ����� ����)
        Debug.Log("Game is exiting...");
    }
}
