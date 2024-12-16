using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public GameObject controlKeysUI;

    public void OnStartGameButtonClicked()
    {
        // Stage0 씬으로 전환
        SceneManager.LoadScene("Stage0");
    }

    public void OnControlKeysButtonClicked()
    {
        // 조작키 UI 활성화
        if (controlKeysUI != null)
        {
            controlKeysUI.SetActive(true);
        }
    }

    public void OnCloseControlKeysUI()
    {
        // 조작키 UI 비활성화
        if (controlKeysUI != null)
        {
            controlKeysUI.SetActive(false);
        }
    }

    public void OnExitButtonClicked()
    {
        // 게임 종료
        Application.Quit();

        // 에디터에서 확인용 메시지 (Unity 에디터에서만 보임)
        Debug.Log("Game is exiting...");
    }
}
