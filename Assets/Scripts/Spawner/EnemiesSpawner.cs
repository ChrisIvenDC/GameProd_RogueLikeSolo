using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
/*using UnityEditor.ShaderGraph.Internal;*/
using Unity.VisualScripting;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] int maxEnemyNum;
    public int currentEnemyNum;
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject[] Bosses;
    public float spawnerTime;
    public bool startTimer;
    GameObject Boss;
    public bool spawnerTimerDone, isEnemySpawn;

    float leftMapX, rightMapX, topMapY, bottomMapY, xSpawn, ySpawn;
    [SerializeField] Transform TopRight, BottomLeft;
    [SerializeField] GameObject mapPoints;
    PlayerXP playerXP;

    Transform player;

    private void Awake()
    {

        mapPoints = GameObject.FindGameObjectWithTag("MapSize");
        BottomLeft = mapPoints.transform.GetChild(0);
        TopRight = mapPoints.transform.GetChild(1);

        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXP>();
        player  = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        leftMapX = BottomLeft.position.x;
        rightMapX = TopRight.position.x;
        topMapY = TopRight.position.y;
        bottomMapY = BottomLeft.position.y;

        currentEnemyNum = 0;

        spawnerTimerDone = true;
        startTimer = false;
    }

    private void Update()
    {
        if (spawnerTimerDone && currentEnemyNum < maxEnemyNum)
        {

            Instantiate(enemies[Random.Range(0, enemies.Length - 1)], SpawnPoint(), Quaternion.identity);
            currentEnemyNum++;
            Debug.Log("EnemyShould Spawn");

            spawnerTimerDone = false;
            startTimer = true;

        }

        if (playerXP.lvl%5  == 0 && playerXP.lvl !=0)
        {
            if (Boss == null)
            {
                Boss = Instantiate(Bosses[Random.Range(0, enemies.Length - 1)], SpawnPoint(), Quaternion.identity);
            }
        }

        if (startTimer)
        {
            StartCoroutine(WaitForSpawnerTime());
        }

    }

    IEnumerator WaitForSpawnerTime()
    {
        startTimer = false;
        yield return new WaitForSeconds(spawnerTime);
        spawnerTimerDone = true;
    }

    Vector3 SpawnPoint()
    {
        xSpawn = Random.Range(leftMapX, rightMapX);
        ySpawn = Random.Range(bottomMapY, topMapY);

        return new Vector3(xSpawn, ySpawn, 0f);
    }

}
