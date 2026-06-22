using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;

public class ChainManager : MonoBehaviour
{
    public static ChainManager Instance { get; private set; }
    private List<Peg> activeChain = new List<Peg>();
    public SplineContainer splineContainer;
    public float chainSpeed = 0.1f;
    public float spacingBetweenPegs = 0.05f;
    public float spawnRate = 1f;
    public int totalPegsToSpawn = 10;
    public GameObject pegPrefab;

    [SerializeField] private float leadProgress;
    [SerializeField] private float spawnedPegs;
    private float spawnTimer;

    void Awake()
    {
        Instance = this;
    }
    public void RegisterPeg(Peg peg)
    {
        activeChain.Add(peg);
    }
    public void RemovePeg(Peg peg)
    {
        activeChain.Remove(peg);
    }
    public int GetChainCount()
    {
        return activeChain.Count;
    }
    void SpawnNextPeg()
    {
        //Debug.Log("SpawnNextPeg called!");
        int newIndex = activeChain.Count;
        float spawnProgress = leadProgress - (newIndex * spacingBetweenPegs);
        Vector3 spawnPosition = splineContainer.EvaluatePosition(Mathf.Clamp01(spawnProgress));
        GameObject peg = Instantiate(pegPrefab, spawnPosition, Quaternion.identity);
        Peg pegComponent = peg.GetComponent<Peg>();
        PegColour nextColour = BallManager.GetRandomColour();
        pegComponent.SetColour(nextColour);
        RegisterPeg(pegComponent);
        spawnedPegs++;
    }

    void Update()
    {
        //Debug.Log($"Update running. spawnedPegs: {spawnedPegs}, totalPegsToSpawn: {totalPegsToSpawn}");
        if (activeChain.Count > 0)
        {
            leadProgress += chainSpeed * Time.deltaTime;
        }

        for (int i = 0; i < activeChain.Count; i++)
        {
            float pegProgress = leadProgress - (i * spacingBetweenPegs);

            if (pegProgress <= 0f)
            {
                break;
            }

            Vector3 newPosition = splineContainer.EvaluatePosition(pegProgress);
            activeChain[i].transform.position = newPosition;

            if (pegProgress >= 1f)
            {
                Peg pegToRemove = activeChain[i];
                GameManager.Instance.OnPegReachedExit(pegToRemove);
                activeChain.RemoveAt(i);
                Destroy(pegToRemove.gameObject);
                leadProgress -= spacingBetweenPegs;
                i--;
                continue;
            }
        }

        if (spawnedPegs < totalPegsToSpawn)
        {
            Debug.Log($"Spawner active. Timer: {spawnTimer}, Rate: {spawnRate}, Spawned: {spawnedPegs}");
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                SpawnNextPeg();
                spawnTimer = 0f;
            }
        }
    }
}
