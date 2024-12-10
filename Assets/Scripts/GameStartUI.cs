using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartUI : MonoBehaviour
{
    public void StartGame()
    {
        // ���� �� �ε� (�� �̸��� ���� �� �̸����� �ٲټ���)
        SceneManager.LoadScene("GameStartUI");
    }

    public void ExitGame()
    {
        // ���ø����̼� ����
        Application.Quit();
        Debug.Log("Game Exit"); // ������ ȯ�濡�� ���� Ȯ�ο�
    }

    public void ShowControls()
    {
        // ����Ű ���� UI�� ǥ���ϰų� �α׷� ���
        Debug.Log("WASD - Move, Mouse - Aim");
    }
}
