using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public int Points = 0;
    public Text PointsText;

    public int MaxSpawnSimultaneously;
    public float EnemySpawnRate;
    public int PointsThreshold;
    public Collider2D SpawnArea;
    public GameObject EnemyPrefab;
    public bool PlayerDead = false;

    private float nextSpawn;
    [HideInInspector]
    public AudioSource AudioSource;
    private Transform player;
    private float oldEnemySpawnRate;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").transform;
        oldEnemySpawnRate = EnemySpawnRate;
    }

    private void Update()
    {
        PointsText.text = Points.ToString();

        if(!PlayerDead)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + EnemySpawnRate;

                for (int i = 0; i < GetRamdomAmount(MaxSpawnSimultaneously); i++)
                {
                    Instantiate(EnemyPrefab, RandomPointInBounds(SpawnArea), Quaternion.identity);
                }
                IncreaseDifficulty();
            }
        }
        
    }
    private Vector3 RandomPointInBounds(Collider2D spawnArea)
    {
        var bounds = spawnArea.bounds;
        var center = bounds.center;

        float x = 0;
        float y = 0;
        do
        {
            x = UnityEngine.Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
            y = UnityEngine.Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);
        } while (Physics2D.OverlapPoint(new Vector2(x, y), LayerMask.NameToLayer("Area")) == null);

        return new Vector3(x, y, 0);
    }

    public void IncreaseDifficulty()
    {
        if (MaxSpawnSimultaneously >= 20)
            MaxSpawnSimultaneously = 3;

        if (EnemySpawnRate <= 0)
            EnemySpawnRate = oldEnemySpawnRate;

        if (Points >= PointsThreshold)
        {
            Debug.Log("Increased Difficulty");
            EnemySpawnRate = EnemySpawnRate / 1.02f;
            PointsThreshold += 2;
            MaxSpawnSimultaneously++;
        }
    }   

    private int GetRamdomAmount(int maxSpawnSimultaneously)
    {
        return Random.Range(1, maxSpawnSimultaneously + 1);
    }
}
