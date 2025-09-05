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
        
        width = box2d.size.x; // �ڽ��ݶ��̴��� ������ x���� �ʺ�� ����
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver) return; 
        RePosition();
    }

    private void RePosition()
    {
        if (tr.position.x <= -width * 1.8f) // Ʈ�������� ������ x���� �ʺ񺸴� ������
        {
            Vector2 ofsset = new Vector2(width * 2.98f, 0); // �������� �ʺ��� 2.5��
            tr.position = (Vector2)tr.position + ofsset; // Ʈ�������� �����ǿ� �������� ����
        }
        tr.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
