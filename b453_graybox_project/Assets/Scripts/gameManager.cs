using NUnit.Framework.Interfaces;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public int numActionsTotal;
    public bool isPlayerTurn;
    public int numActionsRemaining;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        isPlayerTurn = true;
        numActionsRemaining = numActionsTotal;
    }

    public void incrementActionNum()
    {
        numActionsRemaining--;
    }

    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        StartCoroutine(HandleEnemyTurn());
    }

    System.Collections.IEnumerator HandleEnemyTurn()
    {
        // Placeholder for enemies
        Debug.Log("Wait your turn!");
        yield return new WaitForSeconds(2f);

        isPlayerTurn = true;
        numActionsRemaining = numActionsTotal;
        Debug.Log("Your turn!");
    }
}
