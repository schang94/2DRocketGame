using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager p_instance;

    public GameObject asteroidPref;
    private int maxAstetoid = 10;
    private List<GameObject> asteroidPool = new List<GameObject>();
    private int curIdx = 0;

    public GameObject coinPref;
    private int maxCoin = 10;
    private List<GameObject> coinPool = new List<GameObject>();
    private void Awake()
    {
        if (p_instance == null)
        {
            p_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (p_instance != this)
        {
            Destroy(this.gameObject);
        }
        CreateAsteroid();
        CreateCoin();
    }

    private void Start()
    {
        
    }
    void CreateAsteroid()
    {
        GameObject _object = new GameObject("asteroid");

        for (int i = 0; i < maxAstetoid; i++)
        {
            var _asteroid = Instantiate(asteroidPref, _object.transform);
            _asteroid.name = $"{i + 1}¹ø asteroid";
            _asteroid.SetActive(false);
            asteroidPool.Add(_asteroid);
        }
    }

    public GameObject GetAsteroid()
    {
        for (int i = curIdx; i < asteroidPool.Count; i++)
        {
            if (!asteroidPool[curIdx].activeSelf)
            {
                return asteroidPool[curIdx];
            }
            curIdx = (i+1) % asteroidPool.Count;
        }
        return null;
    }

    void CreateCoin()
    {
        GameObject _object = new GameObject("Coin");

        for (int i = 0; i < maxCoin; i++)
        {
            var _coin = Instantiate(coinPref, _object.transform);
            _coin.name = $"{i + 1}¹ø coin";
            _coin.SetActive(false);
            coinPool.Add(_coin);
        }
    }

    public GameObject GetCoin()
    {
        for(int i = 0; i< coinPool.Count; i++)
        {
            if (!coinPool[i].activeSelf)
            {
                return coinPool[i];
            }
        }
        return null;
    }
}
