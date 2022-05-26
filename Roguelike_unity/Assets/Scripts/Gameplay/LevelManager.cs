using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameManager GM;

    [Tooltip("Monsters that can spawn")]
    public List<GameObject> Monsters;
    [Tooltip("Delay in frames between each monster spawn")]
    public float SpawnDelay = 10;
    [Tooltip("How many waves in this level")]
    public int Waves = 3;
    [Tooltip("Maximum number of monsters that could be in a wave")]
    public int MaxPerWave = 200;
    [Tooltip("Minimum number of monsters that could be in a wave")]
    public int MinPerWave = 120;
    [Tooltip("Multiplier - can more monsters spawn in each wave?")]
    public float WaveModifier = 1.25f;

    [Tooltip("Upper Left Limit of Spawn Area")]
    public Transform UpperLeftBounds;
    [Tooltip("Lower Right Limit of Spawn Area")]
    public Transform LowerRightBounds;
    [Tooltip("PLACEHOLDER - spawn a chest when done")]

    public float ChanceOfRareChest = 0;
    public float ChanceOfUniqueChest = 0;
    public float ChanceOfLegendaryChest = 0;

    private GameObject RewardChest;

    private GameObject CommonChest;
    private GameObject RareChest;
    private GameObject UniqueChest;
    private GameObject LegendaryChest;

    List<GameObject> SpawnedMonsters = new List<GameObject>();

    int spawnedSoFar = 0;
    int setSpawnNumber;
    bool waveStarted = false;
    //int enemiesKilled = 0;
    private int currentWave = 0;
    float delay;
   // bool treasureWave = false;
    bool chestUp = false;
    [Tooltip("1.5-5 is a good range")]
    public float ForcedDistanceFromPlayer = 1.5f;

    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        CommonChest = Resources.Load<GameObject>("Chests/CommonChest");
        RareChest = Resources.Load<GameObject>("Chests/RareChest");
        UniqueChest = Resources.Load<GameObject>("Chests/UniqueChest");
        LegendaryChest = Resources.Load<GameObject>("Chests/LegendaryChest");
        delay = SpawnDelay;
    }
    // Update is called once per frame
    void Update()
    {
        if (!waveStarted && currentWave < Waves)
        {
            InitWave();
        }
        else if (waveStarted && GM.currentKillsThisWave < setSpawnNumber - 4)
        {
            if (spawnedSoFar <= setSpawnNumber && GameInfo.PlayerStatus.Alive)
            {
                if (delay > 0)
                {
                    delay -= 60 * Time.deltaTime;
                    if (delay <= 0)
                    {
                        if (spawnedSoFar < 50)
                            SpawnMonster(1);
                        else
                        {
                            SpawnMonster(Random.Range(15, 30));
                        }
                    }
                }
            }
        }
        if (GM.currentKillsThisWave >= setSpawnNumber)
        {
            waveStarted = false;
        }
        if (currentWave >= Waves && GM.LivingEnemies <= 0 && !chestUp && GM.currentKillsThisWave >= setSpawnNumber)
        {
            DoChestSpawn();
            //CamID.Cam.ShakeScreen(2, 5);
        }
    }

    void SpawnMonster(int _amt)
    {
        Vector2 pos;
        do
        {
            pos = new Vector2(Random.Range(UpperLeftBounds.position.x, LowerRightBounds.position.x), Random.Range(UpperLeftBounds.position.y, LowerRightBounds.position.y));
        }
        while (Mathf.Abs(Vector2.Distance(GameInfo.PlayerPos, pos)) < ForcedDistanceFromPlayer || Physics2D.OverlapCircleAll(pos, 2).Length != 0);

        for (int i = 0; i < _amt; i++)
        {
            if (spawnedSoFar <= setSpawnNumber)
            {
                GameObject NPC = Instantiate(Monsters[Random.Range(0, Monsters.Count)], pos + new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)), transform.rotation);
                NPC.GetComponent<NPCStatus>().GM = GM;
                SpawnedMonsters.Add(NPC.gameObject);
                spawnedSoFar++;
                GM.LivingEnemies++;
            }
        }
        delay = SpawnDelay + (_amt * 15);

    }

    public void ResetLevel()
    {
        currentWave = 0;
        spawnedSoFar = 0;
        GM.currentKillsThisWave = 0;
        waveStarted = false;
        for(int i=SpawnedMonsters.Count-1; i>=0; i--)
        {
            if (SpawnedMonsters[i] != null)
                GameObject.Destroy(SpawnedMonsters[i]);
        }
        for (int i = GM.TemporaryDebris.Count - 1; i >= 0; i--)
        {
            if (GM.TemporaryDebris[i] != null)
                GameObject.Destroy(GM.TemporaryDebris[i]);
        }
        GameInfo.Player.transform.position = Vector2.zero;
        GameInfo.PlayerStatus.Alive = true;
        GameInfo.Player.GetComponentInChildren<PlayerSkills>().AtkDlySet(20);
        GameInfo.PlayerStatus.CurrentLife = GameInfo.PlayerStatus.MaxLife;
        CamID.CMController.dead = false;
        GameInfo.PlayerStatus.RetryButton.SetActive(false);
        GameInfo.Player.GetComponent<Animator>().SetBool("Dead", false);
        GM.LivingEnemies = 0;
        delay = 90;
    }

    void DoChestSpawn()
    {
        chestUp = true;
        float chest = Random.Range(0f, 100f);
        if (chest > 100 - ChanceOfLegendaryChest)
            RewardChest = LegendaryChest;
        else if (chest > 100 - ChanceOfUniqueChest)
            RewardChest = UniqueChest;
        else if (chest > 100 - ChanceOfRareChest)
            RewardChest = RareChest;
        else
            RewardChest = CommonChest;

        RewardChest = Instantiate(RewardChest);
        RewardChest.transform.position = GameInfo.PlayerPos + new Vector2(Random.Range(1f, 2f), Random.Range(1f, 2f));
        RewardChest.gameObject.SetActive(true);
    }



    void InitWave()
    {
        currentWave++;
        GM.currentKillsThisWave = 0;
        setSpawnNumber = Random.Range(MinPerWave, MaxPerWave) * (int)(currentWave * WaveModifier);
        spawnedSoFar = 0;
        waveStarted = true;
    }
}
