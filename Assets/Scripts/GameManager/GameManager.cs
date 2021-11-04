using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpawnManager))]
[RequireComponent(typeof(BoardManager))]
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    [HideInInspector]
    public BoardManager boardScript;
    [HideInInspector]
    public SpawnManager spawnManagerScript;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Spawn Manager is Null");
            }
            return instance;
        }
    }

    public List<BoardManager> enemies;           //List of all Enemy units, used to issue them move commands.

    void Awake()
    {
        // PlayerPrefs.DeleteKey("SaveLevel");
        //  PlayerPrefs.DeleteAll();

        // level = PlayerPrefs.GetInt("SaveLevel", level);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
        spawnManagerScript = GetComponent<SpawnManager>();

        StartCoroutine(spawnManagerScript.GroundEnemyRoutine());
        StartCoroutine(spawnManagerScript.SkyEnemyRoutine());
        enemies = new List<BoardManager>();
    }
    /*  private void Start()
      {
           saveLevel = level;
            PlayerPrefs.SetInt("SaveLevel", level); PlayerPrefs.Save();
      }*/
   
    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //Reload Level
        spawnManagerScript.level = UIManager.instance.daysCountingScript._level;
        //Clear any Enemy objects in our List to prepare for next level.
        enemies.Clear();
        //Add one to our level number.
        spawnManagerScript.level++;
    }
    void OnEnable()
    {
        //Tell our ‘OnLevelFinishedLoading’ function to start listening for a scene change event as soon as this script is enabled.

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    void OnDisable()
    {
        //Tell our ‘OnLevelFinishedLoading’ function to stop listening for a scene change event as soon as this script is disabled.
        //Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public void AddEnemyToList(BoardManager script)
    {
        //Add Enemy to List enemies.
        enemies.Add(script);
    }
}


