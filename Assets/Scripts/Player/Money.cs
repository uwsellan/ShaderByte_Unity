using UnityEngine;

public class Money : MonoBehaviour
{

    // Use a singleton pattern to restrict the player Money class to one instance in memory
    public static Money instance { get; private set; }
    [SerializeField] private AudioClip collectCoin;
    private float currentMoney = 0;

    private void Awake()
    {
        instance = this;
    }

    public float coinCollected(float _coinValue)
    {
        currentMoney += _coinValue;
        SoundManager.instance.PlaySound(collectCoin);
        return currentMoney;
    }

    public float getCurrentMoney()
    {
        return currentMoney;
    }

    public void setPlayerMoney(float _cost)
    {
        currentMoney -= _cost;
    }

    public void setLoot(float _loot)
    {
        currentMoney += _loot;
    }

}
