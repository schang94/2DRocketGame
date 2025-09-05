using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCtrl : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public float speed = 10f;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        
    }

    private void OnEnable()
    {
        rb2D.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        Invoke("EnableCoin", 0.5f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void EnableCoin()
    {
        transform.gameObject.SetActive(false);
    }

}
