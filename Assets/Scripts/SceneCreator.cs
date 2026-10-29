using UnityEngine;

public class SceneCreator : MonoBehaviour
{
    public GameObject BoardCellPrefab;
    public GameObject PlayerPiecePrefab;

    float initial_x = -3.5f;
    float initial_y = -3.5f;

    public GameObject[,] gameBoard = new GameObject[8, 8];
    public GameObject cellOccupiedByPlayerInstance;

    void Awake()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int q = 0; q < 8; q++)
            {
                GameObject currCell = Instantiate(BoardCellPrefab, new Vector2(initial_x + i, initial_y + q), Quaternion.identity);
                currCell.GetComponent<CellData>().xIndex = i;
                currCell.GetComponent<CellData>().yIndex = q;
                gameBoard[i, q] = currCell;
                if ((q % 2 == 0 && i % 2 == 0) || (q % 2 == 1 && i % 2 == 1))
                {
                    gameBoard[i, q].GetComponent<SpriteRenderer>().color = Color.white;
                    currCell.GetComponent<CellData>().color = Color.white;
                }
                else
                {
                    currCell.GetComponent<CellData>().color = new Color(0.21f, 0.21f, 0.21f);
                }
            }
        }
        cellOccupiedByPlayerInstance = SpawnAtRandomPosition(PlayerPiecePrefab);
    }

    void Update()
    {
        
    }
    GameObject SpawnAtRandomPosition(GameObject obj)
    {
        //If all cells are full, runs forever
        int xId;
        int yId;
        do
        {
            xId = UnityEngine.Random.Range(0, 8);
            yId = UnityEngine.Random.Range(0, 8);
        } while (gameBoard[xId, yId].GetComponent<CellData>().occupyingObject != null);

        Vector2 position = gameBoard[xId, yId].transform.position;
        GameObject player = Instantiate(obj, position, Quaternion.identity);
        gameBoard[xId, yId].GetComponent<CellData>().occupyingObject = player;
        return gameBoard[xId, yId];
    }
}
