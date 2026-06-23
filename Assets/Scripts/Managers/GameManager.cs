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
        //Debug.Log($"Peg of colour {peg.Colour} reached the exit!");
    }
}
