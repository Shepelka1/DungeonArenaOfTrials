using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Inactive,
    Active,
    Selection
}

public class SceneManager : MonoBehaviour
{
    public GameObject[,] gameBoard = new GameObject[8, 8];
    public GameObject cellOccupiedByPlayerInstance;
    public List<IDice> playerDice = new List<IDice>();
    public IDice selectedDie;

    public GameState gameState = GameState.Active;
    public UnityEvent stateSetSelection = new UnityEvent();
    public UnityEvent stateSetActive = new UnityEvent();
    void Start()
    {
        gameBoard = gameObject.GetComponent<SceneCreator>().gameBoard;
        cellOccupiedByPlayerInstance = gameObject.GetComponent<SceneCreator>().cellOccupiedByPlayerInstance;

        IDice d2 = new D2Dice();
        playerDice.Add(d2);
        IDice d6 = new D6Dice();
        playerDice.Add(d6);
        selectedDie = d6;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stateSetActive.Invoke();
        }
    }

    void HandleMouseClick()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider == null)
            return;

        GameObject hitObject = hit.collider.gameObject;
        if (hitObject.CompareTag("Player"))
        {
            stateSetSelection.Invoke();

        }
        else if (gameState == GameState.Selection && hitObject.CompareTag("BoardCell"))
        {
            if (!CheckCellReachable(cellOccupiedByPlayerInstance, hitObject))
                return;
            stateSetActive.Invoke();
            bool isMoved = MoveFromTo(cellOccupiedByPlayerInstance, hitObject);
            if (!isMoved)
                return;
             cellOccupiedByPlayerInstance = hitObject;
        }
    }

    private bool MoveFromTo(GameObject fromCell, GameObject toCell)
    {
        CellData toData = toCell.GetComponent<CellData>();
        CellData fromData = fromCell.GetComponent<CellData>();
        if (toData.occupyingObject != null)
            return false;
        if (fromData.occupyingObject == null)
            throw new Exception($"Tried to move from an empty cell at {fromData.xIndex};{fromData.yIndex}");
        GameObject moveObject = fromData.occupyingObject;
        toData.occupyingObject = moveObject;
        fromData.occupyingObject = null;
        moveObject.transform.position = toCell.transform.position;
        return true;
    }

    bool CheckCellReachable(GameObject fromCell, GameObject toCell)
    {
        CellData toData = toCell.GetComponent<CellData>();
        CellData fromData = fromCell.GetComponent<CellData>();
        foreach (int[] coords in selectedDie.MoveOffsets)
        {
            if (fromData.xIndex + coords[0] == toData.xIndex && fromData.yIndex + coords[1] == toData.yIndex)
                return true;
        }
        return false;
    }
}