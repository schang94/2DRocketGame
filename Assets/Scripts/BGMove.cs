using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    // 변수지정 : 어디로 이동할지, 속도, 방향
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
        // 메쉬렌더러 안에 머터리얼 안에 메인텍스쳐의 오프셋 = 벡터2(x, y)
    }
}
