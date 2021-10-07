using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainSpawner : MonoBehaviour
{
    [SerializeField] private int minDistanceFromPlayer;
    public Vector3 currentPosition = new Vector3(0,0,0);
    private List<GameObject> currentTerrains = new List<GameObject>();

    [SerializeField] private int firstTerrainCount;
    [SerializeField] private int maxTerrainCount;
    //[SerializeField] private int behindTerrainCount;
    [SerializeField] private List<GameObject> terrains = new List<GameObject>();
    [SerializeField] private Transform terrainHolder;
    
    private void Start()
    {
        for(int i =0; i < firstTerrainCount; i++)
        {
            GameObject firstTerrains = Instantiate(terrains[0], currentPosition, Quaternion.identity, terrainHolder);
            currentTerrains.Add(firstTerrains);
            currentPosition.x++;
        }

        for(int k =0; k < (maxTerrainCount-firstTerrainCount) ; k++)
        {
            SpawnTerrain(new Vector3(0,0,0));
        }
        
    }
    public void SpawnTerrain(Vector3 playerPos)
    {
        if(currentPosition.x - playerPos.x < minDistanceFromPlayer)
        {
            GameObject terrain = Instantiate(terrains[Random.Range(0, terrains.Count)], currentPosition, Quaternion.identity, terrainHolder);
            currentPosition.x++;
            currentTerrains.Add(terrain);

            if (currentTerrains.Count > (maxTerrainCount))
            {
                Destroy(currentTerrains[0]);
                currentTerrains.RemoveAt(0);
            } 
        }
    }
}
