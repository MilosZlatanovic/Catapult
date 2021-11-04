using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    UIManager uiManager;
    ScoreMenu scoreMenu;
    GameManager gameManager;

    [SerializeField]
    Image pauseMenuPanel = null;
    public int _levelScore;

    private void Awake()
    {
        uiManager = UIManager.instance;
        scoreMenu = ScoreMenu.instance;
        gameManager = GameManager.instance;
    }

    public void LoadGame()
    {

        SceneManager.LoadScene(1);
        if (uiManager != null)
        {

        }
        uiManager.defeatMenuScript.currentLevelScore = 0;
        if (gameManager.spawnManagerScript != null)
        {
            gameManager.spawnManagerScript.enabled = true;
        }
        return;
    }
    public void PauseMenu()
    {
        pauseMenuPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ReloadLevel()
    {

        SceneManager.LoadScene(1);
        pauseMenuPanel.gameObject.SetActive(false);
        uiManager.daysCountingScript.LevelLoaded();
        uiManager.RestartHudMenu();
        uiManager.defeatMenuScript.SetupDefeatMenu(false);

        uiManager.daysCountingScript.startDayImage.gameObject.SetActive(true);
        uiManager.defeatMenuScript.currentLevelScore = 0;
        Time.timeScale = 1f; scoreMenu.SocreMenuActive(true);
    }
    public void ResumeGame()
    {
        pauseMenuPanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ReturnMenu()
    {
        // SceneManager.LoadScene(0);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        gameManager.spawnManagerScript.enabled = false;
        Time.timeScale = 1f;
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.gameObject.SetActive(false);
        }
    }
}
