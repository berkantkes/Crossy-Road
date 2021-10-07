using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnobjects = new List<GameObject>();
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float minSeperationTime;
    [SerializeField] private float maxSeperationTime;
    [SerializeField] private bool isRightSide;

    

    private void Start()
    {
        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSeperationTime, maxSeperationTime));
            GameObject go = Instantiate(spawnobjects[Random.Range(0,spawnobjects.Count)], spawnPos.position, Quaternion.identity) as GameObject;
            if (!isRightSide)
            {
                go.transform.Rotate(new Vector3(0, 180, 0));
            }
            
        }  
    }
}
