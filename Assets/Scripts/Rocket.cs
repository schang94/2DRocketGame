using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rocket;

public class Rocket : MonoBehaviour
{
    private readonly string asteroid = "Asteroid";
    private float h = 0f; // A,DŰ �ޱ� ���� ����
    private float v = 0f; // W,SŰ �ޱ� ���� ����
    public float speed = 10f;
    private Transform tr;
    public GameObject effect;
    public AudioSource source;
    public AudioClip hitClip;
    private Vector2 Startpos = Vector2.zero; // ���� ��ġ

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
        //    // ����ȭ
        //    //tr.Translate(Vector3.up * v * speed * Time.deltaTime);
        //    #region �ʺ���
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

        //    #region �߱�
        //    tr.position = new Vector3(Mathf.Clamp(tr.position.x, -7f, 7f), Mathf.Clamp(tr.position.y, -4.5f, 4.5f), tr.position.z);
        //    // Mathf.Clamp(�Է°�, �ּҰ�, �ִ�) // ���� Ư�� ���� ���� �����ϴ� �Լ�
        //    #endregion
        //}
        //else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    if (Input.touchCount > 0) // �ѹ��̶� ��ġ ������
        //    {
        //        Touch touch = Input.GetTouch(0); // ��ġ�� ������ ������ (��ġ�� ��ġ���� �迭�� ���� �Ǿ����� ù��° ��ġ�� ��ġ�� ������

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

    // trigger �浹 ó�� �ݹ� �Լ�
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == asteroid)
        {
            int astroidDamage = 10 * Mathf.CeilToInt(col.gameObject.GetComponent<Transform>().localScale.x);
            
            //Destroy(col.gameObject); // �浹�� ������Ʈ ����
            col.gameObject.SetActive(false);
            var eff = Instantiate(effect, this.transform.position, Quaternion.identity);
            Destroy(eff, 1f);
            //GameManager.Instance.TurnOn(); // ī�޶� ����
            OnCameraShake();
            GameManager.Instance.RocketHealthPoint(astroidDamage); // HP
            source.PlayOneShot(hitClip, 1f); // �Ҹ����
            //    �ѹ������(������, ����);
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
