using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMovement : MonoBehaviour
{
    public float interval;
    private float startTime;
    private bool canMove = true;
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        interval = Board.blockMovingIntervalStatic;
        if (!canMove)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                int x = Mathf.RoundToInt(transform.GetChild(i).transform.position.x);
                int y = Mathf.RoundToInt(transform.GetChild(i).transform.position.y);
                Board.board[x, y] = true;
                GameObject.Find("Game Manager").GetComponent<spawnObjects>().isTimeForSpawn = true;
                Destroy(this);
            }
        }
        if (Time.time - startTime > interval)
        {
            if(!Board.isValid(NextPosGetter(new Vector3(0, -1, 0))))  // IsValid takes the next position and checks the availabilty.
            {
                canMove = false;
                return;
            }
            transform.position += new Vector3 (0, -1, 0);
            startTime = Time.time;
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!Board.isValid(NextPosGetter(new Vector3(-1, 0, 0)))) { return; }
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!Board.isValid(NextPosGetter(new Vector3(1, 0, 0)))) { return; }
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!Board.isValid(NextPosGetter(new Vector3(0, -1, 0)))) { canMove = false; return; }
            MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Board.isValid(NextRotGetter())) { return; }
            Turn();
        }

    }

    private void Turn()
    {
        transform.eulerAngles += new Vector3(0, 0, -90f);
    }

    private void MoveDown()
    {
        transform.position += new Vector3(0, -1, 0);
    }

    private void MoveRight()
    {
        transform.position += new Vector3(1, 0, 0);
    }

    private void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);
    }

    public List<Vector3> NextPosGetter(Vector3 nextPos)
    {
        List<Vector3> result = new List<Vector3>();
        result.Clear();
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            Vector3 pos;
            pos = gameObject.transform.GetChild(i).transform.position;
            pos += nextPos;
            result.Add(pos);
        }
        return result;
    }
    public List<Vector3> NextRotGetter()
    {
        List<Vector3> result = new List<Vector3>();
        result.Clear();
        Vector3 pivotPos = transform.position;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Vector3 pos = gameObject.transform.GetChild(i).transform.position;
            pos -= pivotPos;
            pos = new Vector3(pos.y, -pos.x, 0);
            pos += pivotPos;
            result.Add(pos);
        }
        return result;
    }
}
