using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int level;

    [Header("ENEMIES")]
    [SerializeField]
    GameObject[] groundEnemies = null;

    [Header("TIMERS")]
    [SerializeField]
    float _timerGround;
    public float _timerSky;
    [SerializeField]
    float _timeRespawnSky;
    [SerializeField]
    float _timeRespawnGround;
    [SerializeField]
    float _time;

    public bool _isDeadSky;
    public bool _isDeadGround = true;
    float _searchCountdown = 1f;         // Reduce the number of checks by 1.second
    float _gameStage;                  // Control Game Dificulity through the GameLevel
    public int saveLevel;

    void Update()
    {
        TimeCalculator();
    }
    public void GroundInstantiate()
    {
        if (UIManager.instance.daysCountingScript.doingSetup)
        {
            if (_isDeadGround)
            {
                int enemyGround = Random.Range(0, groundEnemies.Length);
                Instantiate(groundEnemies[enemyGround], new Vector3(13f, -4.7f, 0), Quaternion.identity);
            }
            _isDeadGround = false;
        }
    }

    public void SkyInstantiate()
    {
        if (IsEnemyAlive() && UIManager.instance.daysCountingScript.doingSetup)
        {

            GameManager.instance.boardScript.SetupScene(level);
        }
    }

    public IEnumerator GroundEnemyRoutine()
    {
        _timeRespawnGround = Random.Range(1f, 2f) - _gameStage;
        WaitUntil delay = new WaitUntil(() => _timerGround >= 3f);
        while (true)
        {
            yield return delay;
            GroundInstantiate();
            _timerGround = 0f;
        }
    }

    public IEnumerator SkyEnemyRoutine()
    {
        _timeRespawnSky = Random.Range(1f, 2f) - _gameStage;
        WaitUntil delay = new WaitUntil(() => _timerSky >= 3f);
        while (true)
        {
            yield return delay;
            SkyInstantiate();
            _timerSky = 0f;
        }
    }

    public void StartSpawningGround()
    {
        _isDeadGround = true;
        _timerGround = 0f;
    }

    public void StartSpawningSky() => _timerSky = 0;

    public void TimeCalculator()
    {
        _timerSky += Time.deltaTime;
        _searchCountdown -= Time.deltaTime;
        _timerGround += Time.deltaTime;
        _time += Time.deltaTime;
    }

    bool IsEnemyAlive()
    {
        if (_searchCountdown <= 0f)
        {
            int enemyCounting = GameObject.FindGameObjectsWithTag("SkyEnemy").Length;
            _searchCountdown = 1f;
            if (enemyCounting <= 0)
            {
                return true;
            }
        }
        return false;
    }
}
