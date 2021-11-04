using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(DaysCounting))]
[RequireComponent(typeof(MainMenu))]
[RequireComponent(typeof(VictoryMenu))]
[RequireComponent(typeof(DefeatMenu))]
public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public DaysCounting daysCountingScript;
    [HideInInspector]
    public MainMenu mainMenuScript;
    [HideInInspector]
    public VictoryMenu victoryMenuScript;
    [HideInInspector]
    public DefeatMenu defeatMenuScript;

    public static UIManager instance = null;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("UI Manager is Null");
            }
            return instance;
        }
    }

    [Header("LEVELS BAR`S")]
    public Image hpLevel;

    float levelEnd = 5f;
    float _enemyCounting;

    PlayerInput _inputPlayer;
    public int saveLevel;

    private void Awake()
    {
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.DeleteKey("SaveLevel");

        daysCountingScript = GetComponent<DaysCounting>();
        mainMenuScript = GetComponent<MainMenu>();
        victoryMenuScript = GetComponent<VictoryMenu>();
        defeatMenuScript = GetComponent<DefeatMenu>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        _inputPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();

        if (_inputPlayer == null)
        {
            Debug.LogError("_player == null");
        }
    }

    void Start()
    {
        NextLevel();
    }

    public void NextLevel()
    {
        // PlayerPrefs.SetInt("SaveLevel", _level); //PlayerPrefs.Save();

        daysCountingScript.DaysPast();
        victoryMenuScript.VictoryMenuActive(false);
        //  RestartHudMenu();
        Time.timeScale = 1f;
        mainMenuScript.LoadGame();
        daysCountingScript.LevelLoaded();
        GameManager.instance.spawnManagerScript._isDeadGround = true;
    }

    public void StageCount(float _enemyCounting)
    {
        hpLevel.fillAmount = _enemyCounting / levelEnd;
        if (_enemyCounting >= levelEnd)
        {
            Invoke(nameof(FinishLevel), 1.5f);
            RestartHudMenu();
            //  _inputPlayer.firingProjectileScript.canFire = false;
            // _inputPlayer.gameObject.SetActive(false);
        }
    }

    private void FinishLevel()
    {
        GameManager.instance.spawnManagerScript.enabled = false;
        ObjectPooler.instance.gameObject.SetActive(false);
        // SpawnManager.instance.enemies.Clear();
        daysCountingScript.doingSetup = false;
        victoryMenuScript.VictoryMenuActive(true);
        ScoreMenu.instance.gameObject.SetActive(false);
    }

    public void RestartHudMenu()
    {
        hpLevel.fillAmount = 0;
    }
}

