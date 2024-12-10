using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartUI : MonoBehaviour
{
    public void StartGame()
    {
        // 게임 씬 로드 (씬 이름을 실제 씬 이름으로 바꾸세요)
        SceneManager.LoadScene("GameStartUI");
    }

    public void ExitGame()
    {
        // 애플리케이션 종료
        Application.Quit();
        Debug.Log("Game Exit"); // 에디터 환경에서 동작 확인용
    }

    public void ShowControls()
    {
        // 조작키 설명 UI를 표시하거나 로그로 출력
        Debug.Log("WASD - Move, Mouse - Aim");
    }
}
