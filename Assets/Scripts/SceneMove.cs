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
#if UNITY_EDITOR // 메크로 지정 컴파일러가 에디터에서 실행 중인지 확인
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 게임 종료
#else
        Application.Quit(); // 게임 종료 (빌드 종료)
#endif
    }
}
