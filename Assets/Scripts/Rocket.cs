using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rocket;

public class Rocket : MonoBehaviour
{
    private readonly string asteroid = "Asteroid";
    private float h = 0f; // A,D키 받기 위한 변수
    private float v = 0f; // W,S키 받기 위한 변수
    public float speed = 10f;
    private Transform tr;
    public GameObject effect;
    public AudioSource source;
    public AudioClip hitClip;
    private Vector2 Startpos = Vector2.zero; // 시작 위치

    public Transform firePos;
    public GameObject coinPrefab;

    public delegate void CameraShake();
    public static event CameraShake OnCameraShake;


    void Start()
    {
        source = GetComponent<AudioSource>();
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        Vector3 Normal = (h * Vector3.right) + (v * Vector3.up);
        tr.Translate(Normal.normalized * speed * Time.deltaTime);
        tr.position = new Vector3(Mathf.Clamp(tr.position.x, -7f, 7f), Mathf.Clamp(tr.position.y, -4.5f, 4.5f), tr.position.z);

        //if (Application.platform == RuntimePlatform.WindowsEditor)
        //{
        //    h = Input.GetAxisRaw("Horizontal");
        //    v = Input.GetAxisRaw("Vertical");

        //    Vector3 Normal = (h * Vector3.right) + (v * Vector3.up);
        //    tr.Translate(Normal.normalized * speed * Time.deltaTime);
        //    // 정규화
        //    //tr.Translate(Vector3.up * v * speed * Time.deltaTime);
        //    #region 초보자
        //    //if (tr.position.x >= 7.6f)
        //    //{
        //    //    tr.position = new Vector3(7.6f, tr.position.y, tr.position.z);
        //    //}
        //    //else if (tr.position.x <= -7.8f)
        //    //{
        //    //    tr.position = new Vector3(-7.8f, tr.position.y, tr.position.z);
        //    //}

        //    //if (tr.position.y >= 4.5f)
        //    //{
        //    //    tr.position = new Vector3(tr.position.x, 4.5f, tr.position.z);
        //    //}
        //    //else if(tr.position.y <= -4.5f)
        //    //{
        //    //    tr.position = new Vector3(tr.position.x, -4.5f, tr.position.z);
        //    //}
        //    #endregion

        //    #region 중급
        //    tr.position = new Vector3(Mathf.Clamp(tr.position.x, -7f, 7f), Mathf.Clamp(tr.position.y, -4.5f, 4.5f), tr.position.z);
        //    // Mathf.Clamp(입력값, 최소값, 최댓값) // 값을 특정 범위 내로 제한하는 함수
        //    #endregion
        //}
        //else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    if (Input.touchCount > 0) // 한번이라도 터치 했을때
        //    {
        //        Touch touch = Input.GetTouch(0); // 터치한 정보를 가져옴 (터치한 위치값을 배열로 저장 되었을때 첫번째 터치한 위치를 가져옴

        //        switch (touch.phase)
        //        {
        //            case TouchPhase.Began:
        //                Startpos = touch.position;
        //                break;
        //            case TouchPhase.Moved:
        //                Vector3 touchDelta = touch.position - Startpos;
        //                Vector3 moveDir = new Vector3(touchDelta.x, touchDelta.y, 0f);
        //                tr.Translate(moveDir.normalized * speed * Time.deltaTime);
        //                Startpos = touch.position;
        //                break;
        //        }

        //    }
        //}



    }

    // trigger 충돌 처리 콜백 함수
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == asteroid)
        {
            int astroidDamage = 10 * Mathf.CeilToInt(col.gameObject.GetComponent<Transform>().localScale.x);
            
            //Destroy(col.gameObject); // 충돌한 오브젝트 삭제
            col.gameObject.SetActive(false);
            var eff = Instantiate(effect, this.transform.position, Quaternion.identity);
            Destroy(eff, 1f);
            //GameManager.Instance.TurnOn(); // 카메라 흔들기
            OnCameraShake();
            GameManager.Instance.RocketHealthPoint(astroidDamage); // HP
            source.PlayOneShot(hitClip, 1f); // 소리재생
            //    한번만재생(무엇을, 볼륨);
        }
    }

    public void Fire()
    {

        //GameObject inst =  Instantiate(coinPrefab, firePos.position, Quaternion.identity);
        GameObject _coin = PoolingManager.p_instance.GetCoin();
        _coin.transform.position = firePos.position;
        _coin.gameObject.SetActive(true);
        //Destroy(inst, 0.5f);
    }

    public void OnStickPos(Vector3 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }
}
