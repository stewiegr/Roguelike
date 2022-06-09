using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    GameManager GM;
    float DelayWave = 1;
    public TextMeshProUGUI WaveWarn;

    [Tooltip("Monsters that can spawn")]
    public List<GameObject> Monsters;
    [Tooltip("Delay in frames between each monster spawn")]
    public float SpawnDelay = 10;
    [Tooltip("How many waves in this level")]
    public List<int> Waves;
    [Tooltip("Multiplier - can more monsters spawn in each wave?")]
    public List<GameObject> RelicEnemies;
    [Tooltip("List Length must match WAVES list length. Leave null unless that wave should spawn a unique enemy")]
    public float WaveModifier = 1.25f;
    [Tooltip("Cap on monsters at one time")]
    public float MaxAliveAtOnce = 300;
    [Tooltip("PLACEHOLDER - spawn a chest when done")]

    bool relicEnemyThisWave = false;

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
        WaveWarn = GameObject.Find("WaveWarning").GetComponent<TextMeshProUGUI>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        CommonChest = Resources.Load<GameObject>("Chests/CommonChest");
        RareChest = Resources.Load<GameObject>("Chests/RareChest");
        UniqueChest = Resources.Load<GameObject>("Chests/UniqueChest");
        LegendaryChest = Resources.Load<GameObject>("Chests/LegendaryChest");
        delay = SpawnDelay;
    }

    public List<GameObject> GetActiveEnemies()
    {
        return SpawnedMonsters;
    }
    // Update is called once per frame
    void Update()
    {
        if (DelayWave <= 0)
        {
            if (!waveStarted && currentWave < Waves.Count - 1)
            {
                InitWave();
            }
            else if (waveStarted && GM.currentKillsThisWave < setSpawnNumber - 4)
            {
                if (spawnedSoFar <= setSpawnNumber && GM.LivingEnemies < MaxAliveAtOnce && GameInfo.PlayerStatus.Alive)
                {
                    if (delay > 0)
                    {
                        delay -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed; ;
                        if (delay <= 0)
                        {
                            if (spawnedSoFar < 20)
                                SpawnMonster(1);
                            else
                            {
                                if (Random.Range(0, 10) > 6)
                                    SpawnMonster(Random.Range(20, 30));
                                else
                                    SpawnMonster(Random.Range(5, 15));
                            }
                        }
                    }
                }
            }
        }
        if (GM.currentKillsThisWave > setSpawnNumber && GM.currentKillsThisWave >= spawnedSoFar && waveStarted)
        {
            currentWave++;
            waveStarted = false;
            DoChestSpawn();
        }

        if (DelayWave > 0)
        {
            DelayWave -= 60 * Time.deltaTime;
            WaveWarn.text = "Next Wave In: " + (int)(DelayWave / 60);
        }
        else
            WaveWarn.text = "";
    }

    public void RemoveMe(GameObject remove)
    {
        SpawnedMonsters.Remove(remove);
    }
    void SpawnMonster(int _amt)
    {
        Vector2 pos;
        int random = Random.Range(0, setSpawnNumber);
        // do
        // {
        int index = Random.Range(0, GM.Abyss.Count);
        Vector2 colliderPos = (Vector2)GM.Abyss[index].transform.position + GM.Abyss[index].offset;
        float randomPosX = Random.Range(colliderPos.x - (GM.Abyss[index].size.x * GM.Abyss[index].transform.localScale.x) / 2, colliderPos.x + (GM.Abyss[index].size.x * GM.Abyss[index].transform.localScale.x) / 2);
        float randomPosY = Random.Range(colliderPos.y - (GM.Abyss[index].size.y * GM.Abyss[index].transform.localScale.y) / 2, colliderPos.y + (GM.Abyss[index].size.y * GM.Abyss[index].transform.localScale.y) / 2);
        pos = new Vector3(randomPosX, randomPosY, 0);
        //  }
        // while (Mathf.Abs(Vector2.Distance(GameInfo.PlayerPos, pos)) < ForcedDistanceFromPlayer);

        for (int i = 0; i < _amt; i++)
        {
            if (spawnedSoFar <= setSpawnNumber)
            {
                GameObject NPC = Instantiate(Monsters[Random.Range(0, Monsters.Count)], pos + new Vector2(Random.Range(-15, 15), Random.Range(-15, 15)), transform.rotation);
                NPC.GetComponent<NPCStatus>().GM = GM;
                SpawnedMonsters.Add(NPC.gameObject);
                spawnedSoFar++;
                GM.LivingEnemies++;
            }
        }

        if (RelicEnemies[currentWave] != null && relicEnemyThisWave == false)
        {
            if (random <= spawnedSoFar)
            {
                GameObject NPC = Instantiate(RelicEnemies[currentWave], pos + new Vector2(Random.Range(-15, 15), Random.Range(-15, 15)), transform.rotation);
                NPC.GetComponent<NPCStatus>().GM = GM;
                SpawnedMonsters.Add(NPC.gameObject);
                spawnedSoFar++;
                GM.LivingEnemies++;
                relicEnemyThisWave = true;
            }
        }

        delay = SpawnDelay;

    }



    public void ResetLevel()
    {
        relicEnemyThisWave = false;
        DelayWave = 600;
        currentWave = 0;
        spawnedSoFar = 0;
        GM.currentKillsThisWave = 0;
        waveStarted = false;
        for (int i = SpawnedMonsters.Count - 1; i >= 0; i--)
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
        if (chest > 100 - ChanceOfLegendaryChest * currentWave)
            RewardChest = LegendaryChest;
        else if (chest > 100 - ChanceOfUniqueChest * currentWave)
            RewardChest = UniqueChest;
        else if (chest > 100 - ChanceOfRareChest * currentWave)
            RewardChest = RareChest;
        else
            RewardChest = CommonChest;

        RewardChest = Instantiate(RewardChest);
        RewardChest.transform.position = GameInfo.PlayerPos + new Vector2(Random.Range(1f, 2f), Random.Range(1f, 2f));
        RewardChest.gameObject.SetActive(true);
    }



    void InitWave()
    {
        relicEnemyThisWave = false;
        DelayWave = 200;
        spawnedSoFar = 0;
        setSpawnNumber = Waves[currentWave];
        GM.currentKillsThisWave = 0;
        waveStarted = true;
        for (int i = SpawnedMonsters.Count - 1; i >= 0; i--)
        {
            if (SpawnedMonsters[i] != null)
                GameObject.Destroy(SpawnedMonsters[i]);
        }
        for (int i = GM.TemporaryDebris.Count - 1; i >= 0; i--)
        {
            if (GM.TemporaryDebris[i] != null)
                GameObject.Destroy(GM.TemporaryDebris[i]);
        }
        GM.LivingEnemies = 0;
        delay = 60;
        chestUp = false;
    }
}
