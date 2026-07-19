using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;

public class ChainManager : MonoBehaviour
{
    public static ChainManager Instance { get; private set; }
    private bool levelComplete = false;
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
        //activeChain.Remove(peg);
        int index = activeChain.IndexOf(peg);
        bool removed = activeChain.Remove(peg);

        if (removed && index >= 0)
        {
            leadProgress -= spacingBetweenPegs;
        }

        //Debug.Log($"RemovePeg called. Success: {removed}. Chain count now: {activeChain.Count}");
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
    public void RecalculateChainPositions()
    {
        for (int i = 0; i < activeChain.Count; i++)
        {
            if (activeChain[i] == null) continue;
            float pegProgress = leadProgress - (i * spacingBetweenPegs);
            if (pegProgress <= 0f) break;
            activeChain[i].transform.position = splineContainer.EvaluatePosition(Mathf.Clamp01(pegProgress));
        }
    }

    public void InsertPeg(Peg newPeg, Peg hitPeg)
    {
        for (int i = 0; i < activeChain.Count; i++)
        {
            if (activeChain[i] == null)
            {
                Debug.Log($"Null entry found at index {i} during InsertPeg!");
                activeChain.RemoveAt(i);
            }
        }
        //float closestDistance = float.MaxValue;
        int insertIndex = activeChain.IndexOf(hitPeg);
        //Debug.Log($"InsertPeg: inserting at index {insertIndex}, chain count before: {activeChain.Count}");


        if (insertIndex == -1)
        {
            activeChain.Add(newPeg);
            return;
        }

        if (insertIndex + 1 < activeChain.Count)
        {
            float distanceToNext = Vector3.Distance(activeChain[insertIndex + 1].transform.position, newPeg.transform.position);
            float distanceToCurrent = Vector3.Distance(activeChain[insertIndex].transform.position, newPeg.transform.position);

            if (distanceToNext < distanceToCurrent)
            {
                insertIndex++;
            }
        }

        activeChain.Insert(insertIndex, newPeg);
        leadProgress += spacingBetweenPegs;
        RecalculateChainPositions();
        CheckForCluster(insertIndex);
    }

    public void CheckForCluster(int insertIndex)
    {
        PegColour targetColour = activeChain[insertIndex].Colour;
        int leftIndex = insertIndex -1;

        while (leftIndex >= 0 && activeChain[leftIndex].Colour == targetColour)
        {
            leftIndex--;
        }
        int rightIndex = insertIndex + 1;

        while (rightIndex < activeChain.Count && activeChain[rightIndex].Colour == targetColour)
        {
            rightIndex++;
        }
        int clusterSize = rightIndex - leftIndex - 1;

        if (clusterSize >= 3)
        {
            List<Peg> pegsToRemove = new List<Peg>();

            for (int i = leftIndex + 1; i < rightIndex; i++)
            {
                pegsToRemove.Add(activeChain[i]);
            }
            foreach (Peg peg in pegsToRemove)
            {
                peg.TakeHit();
            }
        }
        
    }

    void Update()
    {
        if (levelComplete) return;
        //Debug.Log($"Update running. spawnedPegs: {spawnedPegs}, totalPegsToSpawn: {totalPegsToSpawn}");
        if (activeChain.Count > 0)
        {
            leadProgress += chainSpeed * Time.deltaTime;
        }

        for (int i = 0; i < activeChain.Count; i++)
        {
            if (activeChain[i] == null)
            {
                activeChain.RemoveAt(i);
                i--;
                continue;
            }

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
            //Debug.Log($"Spawner active. Timer: {spawnTimer}, Rate: {spawnRate}, Spawned: {spawnedPegs}");
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                SpawnNextPeg();
                spawnTimer = 0f;
            }
        }

        if (spawnedPegs >= totalPegsToSpawn && activeChain.Count == 0)
        {
            levelComplete = true;
            GameManager.Instance.OnLevelWon();
        }
    }
}
