using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class E_Spawner : MonoBehaviour
{
    public float minDistTengu;
    public float minDistFrog;
    public float minDistLantern;
    public float minSpawnRange;
    public float maxSpawnRange;
    public int numberOfEnemies;
    public int minChanceSpawn;
    public int maxChanceSpawn;
    public int spawnIfLessThan;
    public float spawnVelocity;
    public float spawnVelVar;
    public float alturaSapo;
    public float minAltTengu;
    public float maxAltTengu;
    public float minAltLantern;
    public float maxAltLantern;
    public float lanternMaxHeight;
    private Transform tengu;
    private Transform frog;
    private Transform lantern;
    private Transform container;
    private GameObject camera;
    private int nMobsSpawned;
    private int forLoop;
    public List<GameObject> enemies = new List<GameObject>();
    private int tenguN;
    private int frogN;
    private int lanternN;
    public float minTenguVel;
    public float maxTenguVel;
    private float TenguVelIncrease;
    public float TenguVelVar;
    private bool firstTime;

    private void Start()
    {
        tengu = transform.Find("Tengu");
        frog = transform.Find("Frog");
        lantern = transform.Find("Lantern");
        container = transform.Find("SpawnContainer");
        camera = GameObject.Find("Camera");
        forLoop = 1;
        StartCoroutine("SpawnMobs");
    }
    
    private IEnumerator SpawnMobs()
    {
        if (!firstTime)
        {
            yield return new WaitForSeconds(3);
            firstTime = true;
        }
        for (int i = 0; i < forLoop; i++)
        {
            int spawnMob = Random.Range(minChanceSpawn, maxChanceSpawn);
            if (spawnMob > spawnIfLessThan/forLoop * forLoop)
            {
                spawnVelocity -= spawnVelocity/spawnVelVar;
                yield return new WaitForSeconds(spawnVelocity);
                forLoop = 1;
                StartCoroutine("SpawnMobs");
                yield break;
            }
            int mobNumb = Random.Range(0, numberOfEnemies);
            float spawnRange = Random.Range(minSpawnRange*forLoop, maxSpawnRange*forLoop);
            if (mobNumb > 0 && mobNumb <= 4)
            {
                var posY = Random.Range(minAltTengu, maxAltTengu);
                
                foreach (var go in enemies)
                {
                    var enemyInfo = go.GetComponent<E_Geral>();
                    if (enemyInfo.name == "Tengu")
                    {
                        var distance = new Vector2(go.transform.position.x, go.transform.position.y) -
                                       new Vector2(camera.transform.position.x + spawnRange, posY);
                        if (distance.magnitude < minDistTengu)
                        {
                            spawnVelocity -= spawnVelocity/spawnVelVar;
                            yield return new WaitForSeconds(spawnVelocity);
                            forLoop = 1;
                            StartCoroutine("SpawnMobs");
                            yield break;
                        }
                    }
                }
                
                var newTengu = Instantiate(tengu, container);
                
                var newTenguInfo = newTengu.gameObject.GetComponent<E_Geral>();
                newTenguInfo.name = "Tengu";
                newTenguInfo.number = tenguN;
                tenguN += 1;

                var newTenguMov = newTengu.gameObject.GetComponent<E_Tengu>();
                var newTenguVelocity = Random.Range(minTenguVel, maxTenguVel) + TenguVelIncrease;

                newTenguMov.velocity = newTenguVelocity;
                
                newTengu.transform.position = new Vector2(camera.transform.position.x + spawnRange, posY);

                enemies.Add(newTengu.gameObject);
                
                newTengu.gameObject.SetActive(true);
                
                forLoop += 1;
                TenguVelIncrease += TenguVelVar;
            }
            else if(mobNumb > 4 && mobNumb <= 8)
            {
                foreach (var go in enemies)
                {
                    var enemyInfo = go.GetComponent<E_Geral>();
                    if (enemyInfo.name == "Frog")
                    {
                        var distance = new Vector2(go.transform.position.x, go.transform.position.y) -
                                       new Vector2(camera.transform.position.x + spawnRange, alturaSapo);
                        if (distance.magnitude < minDistFrog)
                        {
                            spawnVelocity -= spawnVelocity/spawnVelVar;
                            yield return new WaitForSeconds(spawnVelocity);
                            forLoop = 1;
                            StartCoroutine("SpawnMobs");
                            yield break;
                        }
                    }
                }
                var newFrog = Instantiate(frog, container);
                
                var newFrogInfo = newFrog.gameObject.GetComponent<E_Geral>();
                newFrogInfo.name = "Frog";
                newFrogInfo.number = frogN;
                frogN += 1;
                
                newFrog.transform.position = new Vector2(camera.transform.position.x + spawnRange, alturaSapo);
                enemies.Add(newFrog.gameObject);
                newFrog.gameObject.SetActive(true);
                forLoop += 1;
            }
            else
            {
                var posY = Random.Range(minAltLantern, maxAltLantern);
                foreach (Transform go in container)
                {
                    var enemyInfo = go.GetComponent<E_Geral>();
                    if (enemyInfo.name == "Lantern")
                    {
                        var distance = new Vector2(go.transform.position.x, go.transform.position.y) -
                                       new Vector2(camera.transform.position.x + spawnRange, posY);
                        if (distance.magnitude < minDistLantern)
                        {
                            spawnVelocity -= spawnVelocity/spawnVelVar;
                            yield return new WaitForSeconds(spawnVelocity);
                            forLoop = 1;
                            StartCoroutine("SpawnMobs");
                            yield break;
                        }
                    }
                }
                
                var newLantern = Instantiate(lantern, container);
                
                var newLanternInfo = newLantern.gameObject.GetComponent<E_Geral>();
                newLanternInfo.name = "Lantern";
                newLanternInfo.number = lanternN;
                lanternN += 1;
                
                var newLanternMov = newLantern.GetComponent<E_Baloon>();
                newLanternMov.m_Height = -posY + lanternMaxHeight;
                
                newLantern.transform.position = new Vector2(camera.transform.position.x + spawnRange, posY);
                newLantern.gameObject.SetActive(true);
                forLoop += 1;
            }
        
            yield return new WaitForSeconds(spawnVelocity);
            StartCoroutine("SpawnMobs");
        }   
    }
}
