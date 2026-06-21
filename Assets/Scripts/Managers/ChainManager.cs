using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;

public class ChainManager : MonoBehaviour
{
    public static ChainManager Instance { get; private set; }
    private List<Peg> activeChain = new List<Peg>();
    public SplineContainer splineContainer;
    public float chainSpeed = 0.1f;

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
    void Start()
    {
        
    }

    void Update()
    {
        for (int i = activeChain.Count - 1; i >= 0; i--)
        {
            Peg peg = activeChain[i];

            if (peg == null)
            {
                activeChain.RemoveAt(i);
                continue;
            }

            peg.pathProgress += chainSpeed * Time.deltaTime;

            if (peg.pathProgress > 1f)
            {
                GameManager.Instance.OnPegReachedExit(peg);
                activeChain.RemoveAt(i);
                Destroy(peg.gameObject);
                continue;
            }

            Vector3 newPosition = splineContainer.EvaluatePosition(peg.pathProgress);
            peg.transform.position = newPosition;
        }
    }
}
