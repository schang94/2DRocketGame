using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    // �������� : ���� �̵�����, �ӵ�, ����
    private float x = 0f, y = 0f;
    public float speed = 0.5f;
    public MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
    }

    void Update()
    {
        BackGroundMove();
    }

    private void BackGroundMove()
    {
        //x += Time.deltaTime * speed;
        y += Time.deltaTime * speed;
        meshRenderer.material.mainTextureOffset = new Vector2(x, y);
        // �޽������� �ȿ� ���͸��� �ȿ� �����ؽ����� ������ = ����2(x, y)
    }
}
