using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int currentScore;

    void Awake()
    {
        Instance = this;
    }
    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log($"Score : {currentScore}");
    }

    //public int GetScore()
    //{
        
    //}

}
