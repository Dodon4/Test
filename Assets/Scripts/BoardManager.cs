using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;     
    public GameObject tile;      
    public GameObject squarePrefab;
    public int xSize, ySize;     

    [HideInInspector]
    public Node[,] tiles;
    public Node SmallPocket;
    public Node BigPocket;
    public List<GameObject> squares = new List<GameObject>();

    public GameObject victoryPanel;
    public GameObject losePanel;
    void Start()
    {
        instance = GetComponent<BoardManager>();   

        Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;

            CreateBoard(offset.x, offset.y);
            SmallPocket = new Node(GameObject.Find("SmallPocket"), 0);
            BigPocket = new Node(GameObject.Find("BigPocket"), -1);
            SmallPocket.isEmpty = true;
            SmallPocket.square = null;
            BigPocket.isEmpty = true;
            BigPocket.square = null;
            FillBoard();


    }
    private void LateUpdate()
    {
        CheckHorizontal();
        CheckVertical();
    }
    private void CreateBoard(float xOffset, float yOffset)
    {
        tiles = new Node[xSize, ySize];     

        float startX = transform.position.x;     
        float startY = transform.position.y;

        for (int x = 0; x < xSize; x++)
        {      
            for (int y = 0; y < ySize; y++)
            {
                GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
                tiles[x, y] = new Node(newTile, x + y * ySize + 1);
            }
        }
    }
    void FillBoard()
    {
        foreach (Node el in tiles)
        {
            bool Boolean = Random.value > 0.5f;
            if (Boolean || el.id == 4 || el.id == 6)
            {
                GameObject square = Instantiate(squarePrefab, el.tile.transform.position, Quaternion.identity);
                square.GetComponent<Square>().parent = el;
                el.isEmpty = false;
                el.square = square;
                squares.Add(square);
            }
        }
        GameObject sqr = Instantiate(squarePrefab, SmallPocket.tile.transform.position, Quaternion.identity);
        sqr.GetComponent<Square>().parent = SmallPocket;
        SmallPocket.isEmpty = false;
        SmallPocket.square = sqr;
        squares.Add(sqr);
    }
    void CheckVertical()
    {
        for(int x = 0; x < xSize; x++)
        {
            if(!tiles[x,0].isEmpty && !tiles[x, 1].isEmpty && !tiles[x, 2].isEmpty)
            {
                for(int y = 0; y < ySize; y++)
                {
                    tiles[x, y].square.GetComponent<Square>().DestroyAnim();
                    tiles[x, y].isEmpty = true;
                }
            }
        }
    }
    void CheckHorizontal()
    {
        for (int y = 0; y < ySize; y++)
        {
            if (!tiles[0, y].isEmpty && !tiles[1, y].isEmpty && !tiles[2, y].isEmpty)
            {
                if(y == 1)
                {
                    victoryPanel.SetActive(true);
                }
                else
                {
                    losePanel.SetActive(true);
                }
                for (int x = 0; x < xSize; x++)
                {
                    tiles[x, y].square.GetComponent<Square>().DestroyAnim();
                    tiles[x, y].isEmpty = true;
                }
            }
        }
    }

}
