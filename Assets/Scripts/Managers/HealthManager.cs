using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }
    public int maxHP;
    private int currentHP;

    void Awake()
    {
        Instance = this;
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log ($"Health = {currentHP}");
        if (currentHP <= 0)
        {
            OnGameOver();
        }
    }
    public void Heal(int amount)
    {
        currentHP += amount;
        Debug.Log ($"Healed for {amount}. Health = {currentHP}");
    }

    void OnGameOver()
    {
        Debug.Log("Game Over!");
    }

}
