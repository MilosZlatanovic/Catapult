using UnityEngine;
using UnityEngine.UI;

public class DefeatMenu : MonoBehaviour
{
    // Double advertising button
    // Buttons "Back"
    // Days after "Reloading" 


    ScoreMenu scoreMenu;

    public Image defeatPanel;
    [SerializeField]
    Text curentLevel;
    [SerializeField]
    private Text coinsText;
    public Text levelCoinText;

    int _level;
    int _score;
    public int currentLevelScore;
    private const string daysText = "Day ";


    void Awake()
    {
        scoreMenu = ScoreMenu.instance;
    }
    public void SetupDefeatMenu(bool isActiveMenu)
    {
        DaysText();
        CoinText();
        CurrentCoints();
        defeatPanel.gameObject.SetActive(isActiveMenu);
        scoreMenu.SocreMenuActive(false);
        Time.timeScale = 0;
    }
    void DaysText()
    {
        _level = GameManager.instance.spawnManagerScript.level;
        _level -= 1;
        curentLevel.text = daysText + _level.ToString();
    }
    void CoinText()
    {
        _score = scoreMenu.score;
        coinsText.text = _score.ToString();
    }
    public void CurrentCoints()
    {
        levelCoinText.text = currentLevelScore.ToString();
    }
}
