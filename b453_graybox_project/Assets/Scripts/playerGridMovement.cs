using UnityEngine;
using System.Collections;

public class PlayerGridMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2Int currentGridPosition;
    private bool isMoving = false;

    private void Start()
    {
        currentGridPosition = GridManager.Instance.WorldToGrid(transform.position);
        transform.position = GridManager.Instance.GridToWorld(currentGridPosition);
    }

    private void Update()
    {
        if (isMoving)
        {
            return;
        }

        if (!GameManager.Instance.isPlayerTurn)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector2Int targetGridPos = GridManager.Instance.WorldToGrid(mouseWorldPos);

        Debug.Log(targetGridPos);

        if (!GridManager.Instance.IsInsideGrid(targetGridPos))
            return;

        // Only move ONE grid cell at a time
        if (IsAdjacent(targetGridPos))
        {
            StartCoroutine(MoveToCell(targetGridPos));
        }
    }

    bool IsAdjacent(Vector2Int target)
    {
        int dx = Mathf.Abs(target.x - currentGridPosition.x);
        int dy = Mathf.Abs(target.y - currentGridPosition.y);
        return (dx + dy) == 1;
    }

    IEnumerator MoveToCell(Vector2Int targetGridPos)
    {
        isMoving = true;
        GameManager.Instance.isPlayerTurn = false;

        Vector3 startPos = transform.position;
        Vector3 targetPos = GridManager.Instance.GridToWorld(targetGridPos);

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
        currentGridPosition = targetGridPos;
        isMoving = false;
        GameManager.Instance.incrementActionNum();
        if (GameManager.Instance.numActionsRemaining <= 0)
        {
            GameManager.Instance.EndPlayerTurn();
        }
        else
        {
            GameManager.Instance.isPlayerTurn = true;
        }
     
    }
}
