using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    SceneManager sceneManager;
    void Start()
    {
        sceneManager = gameObject.GetComponent<SceneManager>();
        sceneManager.stateSetSelection.AddListener(StateSetSelectionHandler);
        sceneManager.stateSetActive.AddListener(StateSetActiveHandler);
    }

    void Update()
    {
        
    }

    void StateSetSelectionHandler()
    {
        sceneManager.gameState = GameState.Selection;
        CellData playerCell = sceneManager.cellOccupiedByPlayerInstance.GetComponent<CellData>();
        int coordX;
        int coordY;
        foreach (int[] coords in sceneManager.selectedDie.MoveOffsets)
        {
            coordX = playerCell.xIndex + coords[0];
            coordY = playerCell.yIndex + coords[1];
            if (!IsValidCoordinates(coordX, coordY))
                continue;
            CellData currCellData = sceneManager.gameBoard[coordX, coordY].GetComponent<CellData>();
            if (!currCellData.isHighlighted)
            {
                currCellData.isHighlighted = true;
                sceneManager.gameBoard[coordX, coordY].GetComponent<SpriteRenderer>().color = 0.5f * currCellData.color + 0.5f * new Color(0f, 1f, 0f);
            }
        }
    }
    void StateSetActiveHandler()
    {
        sceneManager.gameState = GameState.Active;
        CellData playerCell = sceneManager.cellOccupiedByPlayerInstance.GetComponent<CellData>();
        int coordX;
        int coordY;
        foreach (int[] coords in sceneManager.selectedDie.MoveOffsets)
        {
            coordX = playerCell.xIndex + coords[0];
            coordY = playerCell.yIndex + coords[1];
            if (!IsValidCoordinates(coordX, coordY))
                continue;
            CellData currCellData = sceneManager.gameBoard[coordX, coordY].GetComponent<CellData>();
            if (currCellData.isHighlighted)
            {
                currCellData.isHighlighted = false;
                sceneManager.gameBoard[coordX, coordY].GetComponent<SpriteRenderer>().color = currCellData.color;
            }
        }
    }

    bool IsValidCoordinates(int coordX, int coordY)
    {
        if (coordX < 0 || coordX >= sceneManager.gameBoard.GetLength(0))
            return false;
        if (coordY < 0 || coordY >= sceneManager.gameBoard.GetLength(1))
            return false;
        return true;
    }
}
