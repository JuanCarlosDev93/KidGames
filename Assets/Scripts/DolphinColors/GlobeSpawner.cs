using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeSpawner : MonoBehaviour
{
    [SerializeField] private DolphinColorsGame dolphinColors;
    [SerializeField] private GameManager gameManager;
    public bool activeSpawner;

    [Header("Globe Settings")]
    [SerializeField] private GameObject globePref;
    [SerializeField] private GlobeDataScriptableObject[] globesData;
    [SerializeField] private float globeSpeed;
    [SerializeField] private Color[] globeColor;

    [Header("Spawner Settings")]
    [SerializeField] private Transform[] spawners;
    [SerializeField] private int globeQuantity;
    [SerializeField] private float globeDuration;
    [SerializeField] private float initialDelay; 
    [SerializeField] private float spawnDelay;
    

    public void StartSpawn()
    {
        StartCoroutine(nameof(InfiniteGlobeGenerator));
    }

    IEnumerator InfiniteGlobeGenerator()
    {        
        yield return new WaitForSeconds(initialDelay);
        while (activeSpawner)
        {
            GameObject tempGlobe = ObjectPool.SharedInstance.GetPooledObject();
            if (tempGlobe != null)
            {
                int randomGlobe = Random.Range(0, globesData.Length);
                Transform tempSpawn = spawners[Random.Range(0,spawners.Length)];
                tempGlobe.transform.position = tempSpawn.position;
                tempGlobe.transform.rotation = tempSpawn.rotation;
                tempGlobe.GetComponent<Globe>().SetGlobe(globesData[randomGlobe].objectColor, globesData[randomGlobe].colorAC, Random.Range(2, globeSpeed), globeDuration, 0, dolphinColors.uICounter.transform);
                tempGlobe.GetComponent<Globe>().onClickGlobe.AddListener(dolphinColors.GlobeCounter);
                tempGlobe.SetActive(true);
                tempGlobe.GetComponent<Globe>().canMove = true;
            }
            yield return new WaitForSeconds(spawnDelay);
        }        

    }
    IEnumerator FiniteGlobeGenerator()
    {
        yield return new WaitForSeconds(initialDelay);
        for (int i = 0; i < globeQuantity; i++)
        {
           GameObject tempGlobe = ObjectPool.SharedInstance.GetPooledObject();
           if (tempGlobe != null)
           {
                int randomGlobe = Random.Range(0, globesData.Length);
                Transform tempSpawn = spawners[Random.Range(0, spawners.Length)];
                tempGlobe.transform.position = tempSpawn.position;
                tempGlobe.transform.rotation = tempSpawn.rotation;
                tempGlobe.GetComponent<Globe>().SetGlobe(globesData[randomGlobe].objectColor, globesData[randomGlobe].colorAC, Random.Range(2, globeSpeed), globeDuration, 0, dolphinColors.uICounter.transform);
                tempGlobe.SetActive(true);
                tempGlobe.GetComponent<Globe>().canMove = true;
            }
           yield return new WaitForSeconds(spawnDelay);
        }

    }
}
