using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public int Points = 0;
    public Text PointsText; 
    public float EnemySpawnRate;
    public int PointDifficultyIncrease;
    public Collider2D SpawnArea;
    public GameObject Enemy;
    public bool PlayerDead = false;

    private float nextSpawn;
    [HideInInspector]
    public AudioSource AudioSource;
    private Transform player;
    
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
    }

    private void Update()
    {
        PointsText.text = Points.ToString();

        if(!PlayerDead)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + EnemySpawnRate;
                Instantiate(Enemy, RandomPointInBounds(SpawnArea), Quaternion.identity);
            }
        }
    }
        public Vector3 RandomPointInBounds(Collider2D spawnArea)
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
}
