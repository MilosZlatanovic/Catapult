using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HudBar))]

public class ScoreMenu : MonoBehaviour
{
    [SerializeField]
    private Text _coinsText;
    public int score;

    [HideInInspector]
    public HudBar hudBarScript;
    
    public static ScoreMenu instance;
    public static ScoreMenu Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Score Manager is Null");
            }
            return instance;
        }
    }

    void Awake()
    {
        hudBarScript = GetComponent<HudBar>();
        
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        // For Testing
        //  PlayerPrefs.DeleteKey("Score");
    }
    private void Start()
    {
        // score = PlayerPrefs.GetInt("Score", score);
        // _coinsText.text = score.ToString();
    }
    public void AddCoins(int enemyValue)
    {
        score += enemyValue;
        UIManager.instance.defeatMenuScript.currentLevelScore += enemyValue;
        //  PlayerPrefs.SetInt("Score", score);
        _coinsText.text = score.ToString();
    }
    public void SocreMenuActive(bool isActive) => this.gameObject.SetActive(isActive);
}
