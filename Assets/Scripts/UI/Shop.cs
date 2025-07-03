using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Shop : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private GameObject shop;
    [SerializeField] private Text feedback;
    [SerializeField] private GameObject fade;

    [Header("Item References")]
    [SerializeField] private Text weapon1;
    [SerializeField] private Text weapon2;
    [SerializeField] private Text doubleJump;
    [SerializeField] private Text heart4;
    [SerializeField] private Text heart5;
    [SerializeField] private Text heart6;
    [SerializeField] private Text heart7;
    [SerializeField] private Text heart8;
    [SerializeField] private Text heart9;
    [SerializeField] private Text heart10;

    [Header("Player References")]
    [SerializeField] private Money playerMoney;
    [SerializeField] private PlayerRespawn lives;
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerMovement playerMovement;

    [Header("SFX")]
    [SerializeField] private AudioClip kerching;
    [SerializeField] private AudioClip error;

    void Awake()
    {
        weapon1.color = Color.white;
        weapon2.color = Color.black;
        doubleJump.color = Color.white;
        heart4.color = Color.white;
        heart5.color = Color.black;
        heart6.color = Color.black;
        heart7.color = Color.black;
        heart8.color = Color.black;
        heart9.color = Color.black;
        heart10.color = Color.black;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            shop.SetActive(false);
            fade.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void ExtraLife()
    {
        if(playerMoney.getCurrentMoney() >= 150)
        {
            lives.setExtraLife();
            playerMoney.setPlayerMoney(150);
            SoundManager.instance.PlaySound(kerching);
            feedback.text = "You bought an extra life!";
        }
        else
        {
            SoundManager.instance.PlaySound(error);
            feedback.text = "You can't afford it right now!";
        }
    }

    public void WeaponUpgrade1()
    {
        if(weapon1.color == Color.white)
        {
            if(playerMoney.getCurrentMoney() >= 200)
            {
                playerAttack.setDamageOnUpgrade();
                playerMoney.setPlayerMoney(200);
                SoundManager.instance.PlaySound(kerching);
                feedback.text = "You bought Weapon Upgrade 1!";
                weapon1.color = Color.black;
                weapon2.color = Color.white;
            }
            else
            {
                SoundManager.instance.PlaySound(error);
                feedback.text = "You can't afford it right now!";
            }
        }
        else
        {
            feedback.text = "This item is not available right now";
            SoundManager.instance.PlaySound(error);
        }
    }

    public void WeaponUpgrade2()
    {
        if(weapon2.color == Color.white)
        {
            if(playerMoney.getCurrentMoney() >= 2000)
            {
                playerAttack.setSecondUpgraded();
                playerMoney.setPlayerMoney(2000);
                SoundManager.instance.PlaySound(kerching);
                feedback.text = "You bought Weapon Upgrade 2!";
                weapon2.color = Color.black;
            }
            else
            {
                SoundManager.instance.PlaySound(error);
                feedback.text = "You can't afford it right now!";
            }
        }
        else
        {
            feedback.text = "This item is not available right now";
            SoundManager.instance.PlaySound(error);
        }
    }

    public void DoubleJump()
    {
        if(doubleJump.color == Color.white)
        {
            if(playerMoney.getCurrentMoney() >= 3000)
            {
                playerMovement.unlockDoubleJump();
                playerMoney.setPlayerMoney(3000);
                SoundManager.instance.PlaySound(kerching);
                feedback.text = "You bought the ability to double-jump!";
                doubleJump.color = Color.black;
            }
            else
            {
                SoundManager.instance.PlaySound(error);
                feedback.text = "You can't afford it right now!";
            }
        }
        else
        {
            feedback.text = "This item is not available right now";
            SoundManager.instance.PlaySound(error);
        }
    }

    public void Heart4()
    {
        if(heart4.color == Color.white)
        {
            if(playerMoney.getCurrentMoney() >= 100)
            {
                playerHealth.setStartingHealth(4);
                playerMoney.setPlayerMoney(100);
                SoundManager.instance.PlaySound(kerching);
                feedback.text = "You bought an extra heart!";
                heart4.color = Color.black;
                heart5.color = Color.white;
            }
            else
            {
                SoundManager.instance.PlaySound(error);
                feedback.text = "You can't afford it right now!";
            }
        }
        else
        {
            feedback.text = "This item is not available right now";
            SoundManager.instance.PlaySound(error);
        }
    }

    public void Heart5()
    {
        if(heart5.color == Color.white)
        {
            if(playerMoney.getCurrentMoney() >= 500)
            {
                playerHealth.setStartingHealth(5);
                playerMoney.setPlayerMoney(500);
                SoundManager.instance.PlaySound(kerching);
                feedback.text = "You bought an extra heart!";
                heart5.color = Color.black;
                heart6.color = Color.white;
            }
            else
            {
                SoundManager.instance.PlaySound(error);
                feedback.text = "You can't afford it right now!";
            }
        }
        else
        {
            feedback.text = "This item is not available right now";
            SoundManager.instance.PlaySound(error);
        }
    }

    public void Heart6()
    {
        if(heart6.color == Color.white)
        {
            if(playerMoney.getCurrentMoney() >= 900)
            {
                playerHealth.setStartingHealth(6);
                playerMoney.setPlayerMoney(900);
                SoundManager.instance.PlaySound(kerching);
                feedback.text = "You bought an extra heart!";
                heart6.color = Color.black;
                heart7.color = Color.white;
            }
            else
            {
                SoundManager.instance.PlaySound(error);
                feedback.text = "You can't afford it right now!";
            }
        }
        else
        {
            feedback.text = "This item is not available right now";
            SoundManager.instance.PlaySound(error);
        }
    }

    public void Heart7()
    {
        if(heart7.color == Color.white)
        {
            if(playerMoney.getCurrentMoney() >= 1300)
            {
                playerHealth.setStartingHealth(7);
                playerMoney.setPlayerMoney(1300);
                SoundManager.instance.PlaySound(kerching);
                feedback.text = "You bought an extra heart!";
                heart7.color = Color.black;
                heart8.color = Color.white;
            }
            else
            {
                SoundManager.instance.PlaySound(error);
                feedback.text = "You can't afford it right now!";
            }
        }
        else
        {
            feedback.text = "This item is not available right now";
            SoundManager.instance.PlaySound(error);
        }
    }

    public void Heart8()
    {
        if(heart8.color == Color.white)
        {
            if(playerMoney.getCurrentMoney() >= 1700)
            {
                playerHealth.setStartingHealth(8);
                playerMoney.setPlayerMoney(1700);
                SoundManager.instance.PlaySound(kerching);
                feedback.text = "You bought an extra heart!";
                heart8.color = Color.black;
                heart9.color = Color.white;
            }
            else
            {
                SoundManager.instance.PlaySound(error);
                feedback.text = "You can't afford it right now!";
            }
        }
        else
        {
            feedback.text = "This item is not available right now";
            SoundManager.instance.PlaySound(error);
        }
    }

    public void Heart9()
    {
        if(heart9.color == Color.white)
        {
            if(playerMoney.getCurrentMoney() >= 2100)
            {
                playerHealth.setStartingHealth(9);
                playerMoney.setPlayerMoney(2100);
                SoundManager.instance.PlaySound(kerching);
                feedback.text = "You bought an extra heart!";
                heart9.color = Color.black;
                heart10.color = Color.white;
            }
            else
            {
                SoundManager.instance.PlaySound(error);
                feedback.text = "You can't afford it right now!";
            }
        }
        else
        {
            feedback.text = "This item is not available right now";
            SoundManager.instance.PlaySound(error);
        }
    }

    public void Heart10()
    {
        if(heart10.color == Color.white)
        {
            if(playerMoney.getCurrentMoney() >= 2500)
            {
                playerHealth.setStartingHealth(10);
                playerMoney.setPlayerMoney(2500);
                SoundManager.instance.PlaySound(kerching);
                feedback.text = "You bought an extra heart!";
                heart10.color = Color.black;
            }
            else
            {
                SoundManager.instance.PlaySound(error);
                feedback.text = "You can't afford it right now!";
            }
        }
        else
        {
            feedback.text = "This item is not available right now";
            SoundManager.instance.PlaySound(error);
        }
    }

}
