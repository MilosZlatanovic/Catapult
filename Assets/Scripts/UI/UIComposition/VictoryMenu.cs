using UnityEngine;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
    // in title show Days pass
    // setap statistic(Img,Text)
    // coin in last level(Img,Text)
    // Shop Button
    // coin in total(Img,Text)
    // Double advertising button
    // Three stars.Img for win

    [SerializeField]
    Image victoryMenuPanel;

    public void VictoryMenuActive(bool isPanelActive) => victoryMenuPanel.gameObject.SetActive(isPanelActive);

}
