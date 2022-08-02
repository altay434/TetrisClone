using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Board : MonoBehaviour
{
    private const int GRIDSIZE_X = 10;
    private const int GRIDSIZE_Y = 25;
    [Header("Game Settings")]
    public float blockMovingInterval;
    public static float blockMovingIntervalStatic;
    public static bool[,] board = new bool[GRIDSIZE_X, GRIDSIZE_Y];
    private int score = 0;
    
    private void Start()
    {
        ClearBoard();
    }
    private void Update()
    {
        GameObject.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + score.ToString();
        blockMovingIntervalStatic = blockMovingInterval;
        if (!CheckForFinish()) { return; }
        FinishGame();
    }
    public static bool isValid(List<Vector3> nextPos) // Checks the availability of the boxes
    {
        foreach(Vector3 position in nextPos)
        {
            int x = Mathf.RoundToInt(position.x);
            int y = Mathf.RoundToInt(position.y);
            if(x < 0 || x >= GRIDSIZE_X)
            {
                return false;
            }
            if (y < 0 || y >= GRIDSIZE_Y)
            {
                return false;
            }
            if (board[x, y])
            {
                return false;
            }
        }
        return true;
    }
    public bool IsRowFull(int index)     
    {
        for(int i = 0; i < GRIDSIZE_X; i++)
        {
            if(!board[i, index])
            {
                return false;
            }
        }
        return true;
    }
    public void ClearFullRows()  
    {
        
        for(int i = 0; i < GRIDSIZE_Y; i++)
        {
            if (IsRowFull(i))
            {
                score += 100;
                foreach (GameObject item in GameObject.Find("Game Manager").GetComponent<spawnObjects>().objects)
                {
                    for(int j = 0; j < item.transform.childCount; j++)
                    {
                        // Destroy the gameobjects
                        if (Mathf.RoundToInt(item.transform.GetChild(j).transform.position.y) == i)
                        {
                            Destroy(item.transform.GetChild(j).gameObject);  
                        }
                        // Move blocks to down
                        for (int k = i+1; k < GRIDSIZE_Y ; k++)
                        {
                            if (Mathf.RoundToInt(item.transform.GetChild(j).transform.position.y) == k)
                            {
                                item.transform.GetChild(j).gameObject.transform.position += new Vector3(0, -1, 0);  
                            }
                        }
                    }
                    
                }
                // Change the Data
                for(int j = 0; j < GRIDSIZE_X; j++)
                {
                    board[j, i] = false;
                }
                for(int k = i+1; k < GRIDSIZE_Y; k++)
                {
                    for(int l = 0; l < GRIDSIZE_X; l++)
                    {
                        board[l,k - 1] = board[l,k];
                    }
                }
                ClearFullRows();
                return;
            }
        }
    }
    private void FinishGame()
    {
        ClearBoard();
        SceneManager.LoadScene("FinishMenu");
    }

    private static void ClearBoard()
    {
        for (int i = 0; i < GRIDSIZE_X; i++)
        {
            for (int j = 0; j < GRIDSIZE_Y; j++)
            {
                board[i, j] = false;
            }
        }
    }

    private bool CheckForFinish()
    {
        for(int i = 0; i < GRIDSIZE_X; i++)
        {
            if(board[i, 18])
            {
                return true;
            }
        }
        return false;
    }
}
