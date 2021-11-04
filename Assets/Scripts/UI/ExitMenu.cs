using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    public void ExitGame()
    {
        gameObject.SetActive(true);
    }
    public void StayGame()
    {
        gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
