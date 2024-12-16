using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // 나가기 버튼 클릭 시 호출
    public void ExitApplication()
    {
        Debug.Log("게임 종료 버튼이 눌렸습니다.");

        // 유니티 에디터에서는 에디터 중지, 빌드된 앱에서는 애플리케이션 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
