using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void OnPegReachedExit(Peg peg)
    {
        HealthManager.Instance.TakeDamage(peg.damageValue);
        //Debug.Log($"Peg of colour {peg.Colour} reached the exit!");
    }
    
    public void OnLevelWon()
    {
        Debug.Log("Level Won!");
    }
}
