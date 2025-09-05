using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null; //싱글톤 패턴

    // asteroid 프리팹 생성
    public GameObject asteroidPrefab;

    // 생성 주기 및 시간
    private float timePreve;

    [Header("bool GameOver")] // 애듀리뷰트
    public bool isGameOver = false;
    
    // 카메라 쉐이크
    [Header("CameraShake Logic Member")]
    public bool isShake = false;
    public Vector3 PosCamera; // 카메라 위치 저장
    public float beginTime; // 카메라가 흔들리기 시작한 시간

    [Header("HP UI")]
    public int hp;
    public int maxHp = 100;
    public Image hpBar;
    public Text hpText;
    
    [Header("GameOver Obj")]
    public GameObject gameOverObj;
    public bool isPlay = false;

    [Header("Score UI")]
    public Text scoreTxt;
    private float curScore = 0; //현재 점수
    private float plusScore = 1f; // 점수 증가량

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            DontDestroyOnLoad(this.gameObject); // 씬이 바뀌어도 파괴되지 않음



       
            // 게임 시작전 현재시간을 저장
        timePreve = Time.time; // 현재 시간 저장
        hp = maxHp;
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }
        if (Time.time - timePreve > Random.Range(0.5f, 2f) && !isGameOver)
        {
            AsteroidSpwan();
        }

        if (isShake)
        {
            float x = Random.Range(-0.1f, 0.1f);
            float y = Random.Range(-0.1f, 0.1f);
            Camera.main.transform.position += new Vector3(x, y, 0f);

            if (Time.time - beginTime > 0.3f)
            {
                isShake = false;
                Camera.main.transform.position = PosCamera;
            }
        }

        ScoreCount();
    }

    private void ScoreCount()
    {
        curScore += plusScore * Time.realtimeSinceStartup;
        //realtimeSinceStartup : 유니티 엔진이 시작된 후 실제 시간을 경과한 시간을 초단위로 반환하는 읽기 전용(read-only)

        scoreTxt.text = $"{Mathf.FloorToInt(curScore)}";
    }

    public void TurnOn()
    {
        isShake = true;
        PosCamera = Camera.main.transform.position;
        beginTime = Time.time;
    }
    private void AsteroidSpwan()
    {
        GameObject _asteroid = PoolingManager.p_instance.GetAsteroid();
        float randomScale = Random.Range(0.5f, 3f);
        float randomY = Random.Range(-4f, 4f);
        if (_asteroid != null)
        {
            _asteroid.transform.localScale = new Vector3(randomScale, randomScale, 0);
            _asteroid.transform.position = new Vector3(12f, randomY, _asteroid.transform.position.z);
            _asteroid.SetActive(true);
        }
        //asteroidPrefab.transform.localScale = new Vector3(randomScale, randomScale, 0);

        // Instantiate(what?, where, how 회전 할 것인가) : 오브젝트 생성 함수
        //Instantiate(asteroidPrefab, new Vector3(12f, randomY, asteroidPrefab.transform.position.z), Quaternion.identity);
        timePreve = Time.time;
    }
    public void RocketHealthPoint(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, 100);
        hpText.text = $"HP : <color=#ff0000>{hp}</color>";
        hpBar.fillAmount = (float)hp / (float)maxHp;
        if (hp <= 0)
        {
            isGameOver = true;
            gameOverObj.SetActive(true);
            Invoke("LobbySceneMove", 3.0f); // 스트링 문자를 읽어서 원하는 시간에 호출하는 함수 
        }
    }
    public void LobbySceneMove()
    {
        SceneManager.LoadScene("LobbyScene");
    }


}
