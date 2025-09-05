using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGNearMove : MonoBehaviour
{
    public float speed = 7f;
    public float w;
    private BoxCollider2D box;
    private Transform tr;
    void Start()
    {
        tr = GetComponent<Transform>();
        box = GetComponent<BoxCollider2D>();
        w = box.size.x * tr.localScale.x;
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        RePosition();
    }

    private void RePosition()
    {
        if (tr.position.x <= -w * 1.8f)
        {
            Vector2 vector2 = new Vector2(w * 2.99f, 0);
            tr.position = (Vector2)tr.position + vector2;
        }
        tr.Translate(Vector2.left * Time.deltaTime * speed);
    }
}
