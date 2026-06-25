using UnityEngine;
using System.Collections.Generic;
using System;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }
    private Queue<PegColour> ballQueue = new Queue<PegColour>();
    public int queueSize = 5;
    public bool isDetonateMode = true;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < queueSize; i++)
        {
            ballQueue.Enqueue(GetRandomColour());
        }
    }
    public PegColour GetNextBallColour()
    {
        PegColour nextColour = ballQueue.Dequeue();
        ballQueue.Enqueue(GetRandomColour());

        PegColour[] preview = ballQueue.ToArray();
        Debug.Log($"NEXT: {preview[0]} | THEN: {preview[1]}");

        return nextColour;
    }
    public void ToggleFireMode()
    {
        isDetonateMode = !isDetonateMode;
    }
    public static PegColour GetRandomColour()
    {
        Array colours = Enum.GetValues(typeof(PegColour));
        return (PegColour)colours.GetValue(UnityEngine.Random.Range(0, colours.Length));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
