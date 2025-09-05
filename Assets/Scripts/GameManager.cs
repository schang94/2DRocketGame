using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null; //�̱��� ����

    // asteroid ������ ����
    public GameObject asteroidPrefab;

    // ���� �ֱ� �� �ð�
    private float timePreve;

    [Header("bool GameOver")] // �ֵฮ��Ʈ
    public bool isGameOver = false;
    
    // ī�޶� ����ũ
    [Header("CameraShake Logic Member")]
    public bool isShake = false;
    public Vector3 PosCamera; // ī�޶� ��ġ ����
    public float beginTime; // ī�޶� ��鸮�� ������ �ð�

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
    private float curScore = 0; //���� ����
    private float plusScore = 1f; // ���� ������

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            DontDestroyOnLoad(this.gameObject); // ���� �ٲ� �ı����� ����



       
            // ���� ������ ����ð��� ����
        timePreve = Time.time; // ���� �ð� ����
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
        //realtimeSinceStartup : ����Ƽ ������ ���۵� �� ���� �ð��� ����� �ð��� �ʴ����� ��ȯ�ϴ� �б� ����(read-only)

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

        // Instantiate(what?, where, how ȸ�� �� ���ΰ�) : ������Ʈ ���� �Լ�
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
            Invoke("LobbySceneMove", 3.0f); // ��Ʈ�� ���ڸ� �о ���ϴ� �ð��� ȣ���ϴ� �Լ� 
        }
    }
    public void LobbySceneMove()
    {
        SceneManager.LoadScene("LobbyScene");
    }


}
