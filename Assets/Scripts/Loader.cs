using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject objectPool;
    public GameObject gameManager;
    public GameObject scoreMenu;
    public GameObject uiManager;

    void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        if (ObjectPooler.instance == null)
        {
            Instantiate(objectPool);
        }

        if (ScoreMenu.instance == null)
        {
            Instantiate(scoreMenu);
        }

        if (UIManager.instance == null)
        {
            Instantiate(uiManager);
        }
    }
}