using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObjects : MonoBehaviour
{
    public GameObject[] blockPrefabs;
    
    [HideInInspector] public List<GameObject> objects;
    [HideInInspector] public bool isTimeForSpawn;
    
    private void Start()
    {
        isTimeForSpawn = true;
    }
    private void Update()
    {
        if (isTimeForSpawn)
        {
            SpawnObject();
        }
    }
    public void SpawnObject()
    {
        isTimeForSpawn = false;
        this.GetComponent<Board>().ClearFullRows();
        GameObject createdObject = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)], new Vector3(5f,20f,0), Quaternion.identity);
        objects.Add(createdObject);
    }
}
