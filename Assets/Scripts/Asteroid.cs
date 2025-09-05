using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private readonly string coinTag = "Coin";
    public float speed;
    private Transform tr;
    void Start()
    {
        speed = Random.Range(10f, 20f);
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        tr.Translate(Vector3.left * speed * Time.deltaTime);

        //if (tr.position.x <= -10f)
        //    Destroy(tr.gameObject); // 이방법으로 하면 가비지 컬렉터에 의해 프레임이 느려짐
        StartCoroutine(EnableAsteroid());
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == coinTag)
        {
            col.gameObject.SetActive(false);
            
            //Destroy(col.gameObject);
            col.gameObject.SetActive(false);
            //Destroy(tr.gameObject, 0.1f);
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator EnableAsteroid()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    
}
