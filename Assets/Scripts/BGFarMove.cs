using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFarMove : MonoBehaviour
{
    public float speed;
    public Transform tr;
    public BoxCollider2D box2d;
    private float width;
    void Start()
    {
        tr = GetComponent<Transform>();
        box2d = GetComponent<BoxCollider2D>();
        speed = 5f;
        
        width = box2d.size.x; // 박스콜라이더의 사이즈 x값을 너비로 저정
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver) return; 
        RePosition();
    }

    private void RePosition()
    {
        if (tr.position.x <= -width * 1.8f) // 트랜스폼의 포지션 x값이 너비보다 작으면
        {
            Vector2 ofsset = new Vector2(width * 2.98f, 0); // 오프셋을 너비의 2.5배
            tr.position = (Vector2)tr.position + ofsset; // 트랜스폼의 포지션에 오프셋을 더함
        }
        tr.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
