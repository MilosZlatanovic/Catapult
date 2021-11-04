using UnityEngine;
using UnityEngine.UI;

public class HudBar : MonoBehaviour
{
    public Button weaponBtn1, weaponBtn2, weaponBtn3;
    public Sprite weaponRed, weaponNormal;

    [HideInInspector]
    public int selectWeapon;

    PlayerFireControler _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerFireControler>();
        if (_player == null)
        {
            Debug.LogError("_player is Null");
        }
        WeaponOne();
    }

    public void SwapWeapon(int weaponSwap)
    {
        switch (weaponSwap)
        {
            case 1:
                //  WeaponOne
                weaponBtn1.image.fillAmount -= 2f * Time.deltaTime;
                break;

            case 2: //  WeaponTwo
                weaponBtn2.image.fillAmount -= 2f * Time.deltaTime;
                break;

            case 3:// WeaponThree
                weaponBtn3.image.fillAmount -= 2f * Time.deltaTime;
                break;

            default:
                break;
        }
    }
    public void WeaponOne()
    {
        selectWeapon = 1;
        SwapWeapon(selectWeapon);
        weaponBtn1.gameObject.GetComponent<Image>().sprite = weaponRed;
        weaponBtn2.gameObject.GetComponent<Image>().sprite = weaponNormal;
        weaponBtn3.gameObject.GetComponent<Image>().sprite = weaponNormal;
        // FireIndicator();

    }
    public void WeaponTwo()
    {
        selectWeapon = 2;
        SwapWeapon(selectWeapon);
        weaponBtn2.gameObject.GetComponent<Image>().sprite = weaponRed;
        weaponBtn1.gameObject.GetComponent<Image>().sprite = weaponNormal;
        weaponBtn3.gameObject.GetComponent<Image>().sprite = weaponNormal;

    }
    public void WeaponThree()
    {
        selectWeapon = 3;
        SwapWeapon(selectWeapon);
        weaponBtn3.gameObject.GetComponent<Image>().sprite = weaponRed;
        weaponBtn2.gameObject.GetComponent<Image>().sprite = weaponNormal;
        weaponBtn1.gameObject.GetComponent<Image>().sprite = weaponNormal;
    }
    public void Indicator()
    {
        weaponBtn1.image.fillAmount = 1;
        weaponBtn2.image.fillAmount = 1;
        weaponBtn3.image.fillAmount = 1;
    }
}


/* public void TurnRed()
 {
     ColorBlock colors = weaponBtn1.colors;
     colors.normalColor = Color.red;
     colors.highlightedColor = new Color32(255, 100, 100, 255);
     weaponBtn1.colors = colors;
 }

 public void TurnWhite()
 {
     ColorBlock colors = weaponBtn1.colors;
     colors.normalColor = Color.white;
     colors.highlightedColor = new Color32(225, 225, 225, 255);
     weaponBtn1.colors = colors;
 }*/

