using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public bool isGameOver = false;
    //void Start()
    //{

    //}

    //void Update()
    //{

    //}

    public void LobbySceneMove()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void GameStart()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void GameQuit()
    {
#if UNITY_EDITOR // ��ũ�� ���� �����Ϸ��� �����Ϳ��� ���� ������ Ȯ��
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� ���� ����
#else
        Application.Quit(); // ���� ���� (���� ����)
#endif
    }
}
