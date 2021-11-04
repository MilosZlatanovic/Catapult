using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DaysCounting : MonoBehaviour
{
    GameManager gameManager;
    ScoreMenu scoreMenu;

    public Image startDayImage;
    const float levelStartDelay = 2.5f;
    public TextMeshProUGUI startDayText;
    public Text hudDayText;
    public bool doingSetup;
    public int _level;

    private void Awake()
    {
        gameManager = GameManager.instance;
        scoreMenu = ScoreMenu.instance;

    }
    public void DaysPast()
    {
        scoreMenu.SocreMenuActive(false);
        _level = gameManager.spawnManagerScript.level;
        hudDayText.text = "Day " + _level.ToString();
        startDayText.text = "Day " + _level.ToString();
        startDayImage.gameObject.SetActive(true);
    }

    public void HideLevelImage()
    {
        scoreMenu.SocreMenuActive(true);
        startDayImage.gameObject.SetActive(false);
        doingSetup = true;
        gameManager.spawnManagerScript.StartSpawningSky();
        gameManager.spawnManagerScript.StartSpawningGround();
    }
    public void LevelLoaded()
    {
        doingSetup = false;
        Invoke(nameof(HideLevelImage), levelStartDelay);

    }
}
