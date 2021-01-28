using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_RoofSpawn : MonoBehaviour
{
    public float limitToSpawnNewFloor;
    public Vector2 worldSize;
    public int respiro;
    private Transform go_Floor;
    private List<Transform> l_Floor = new List<Transform>();
    private Vector2 screenSizePixels;
    private Vector2 floorSizePixels;
    private SpriteRenderer _spriteRendererFloor;
    private int nFloors;
    private Transform floorContainer;
    private Vector2 worldSpriteSize;
    private bool spawnChecked;
    private float y_pos;
    private float listMaxLenght;
    private GameObject camera;
    private float initLimitToSpawn;

    private void Start()
    {
        y_pos = worldSize.y / 2;
        initLimitToSpawn = limitToSpawnNewFloor;
        floorContainer = transform.Find("RoofContainer");
        go_Floor = transform.Find("Roof");
        camera = GameObject.Find("Camera");
        _spriteRendererFloor = go_Floor.GetComponent<SpriteRenderer>();
        screenSizePixels = new Vector2(Screen.width, Screen.height);
        floorSizePixels = GetSizeOfObjectInPixel(_spriteRendererFloor);
        var initPos = new Vector3(-worldSize.x / 2, y_pos);
        listMaxLenght = (screenSizePixels.x / floorSizePixels.x + respiro)*2;
        GeneratePlainWorld(screenSizePixels, floorSizePixels, initPos);
        InvokeRepeating("CheckCameraDistance", 0 , 0.5f);
    }

    private Vector2 GetSizeOfObjectInPixel(SpriteRenderer spriteRenderer)
    {
        Vector2 spriteSize = spriteRenderer.sprite.rect.size;
        Vector2 localSpriteSize = spriteSize / spriteRenderer.sprite.pixelsPerUnit;
        Vector3 worldSize = new Vector3(localSpriteSize.x, localSpriteSize.y, 0);
        worldSize.x *= transform.lossyScale.x;
        worldSize.y *= transform.lossyScale.y;
        worldSpriteSize = localSpriteSize;

        Vector3 screen_size = 0.5f * worldSize / Camera.main.orthographicSize;
        screen_size.y *= Camera.main.aspect;

        Vector3 in_pixels = new Vector3(screen_size.x * Camera.main.pixelWidth, screen_size.y * Camera.main.pixelHeight, 0) * 0.5f;
        return new Vector2(in_pixels.x, in_pixels.y);
    }

    private void GeneratePlainWorld(Vector2 screenSize, Vector2 objectSize, Vector2 initialPos)
    {
        float floorsNumber = screenSize.x / objectSize.x + respiro;
        for (int i = 0; i < floorsNumber; i++)
        {
            var newFloor = Instantiate(go_Floor, floorContainer, true);
            newFloor.name = "Roof --- " + nFloors;
            newFloor.transform.position = initialPos + new Vector2(go_Floor.lossyScale.x * worldSpriteSize.x, 0) * i;
            newFloor.gameObject.SetActive(true);
            l_Floor.Add(newFloor);
            if (l_Floor.Count > listMaxLenght)
            {
                Destroy(l_Floor[0].gameObject);
                l_Floor.Remove(l_Floor[0]);
            }
            nFloors += 1;
        }

        spawnChecked = true;
        limitToSpawnNewFloor = camera.transform.position.x + initLimitToSpawn;
    }

    private void Update()
    {
        if(spawnChecked)
        {
            CheckLastPosition(l_Floor.Count - 1);
        }
    }

    private void CheckLastPosition(int nFloor)
    {
        var floorObj = l_Floor[nFloor];
        if (floorObj.position.x < limitToSpawnNewFloor)
        {
            var initPos = new Vector2(floorObj.position.x, y_pos);
            spawnChecked = false;
            GeneratePlainWorld(screenSizePixels/2, floorSizePixels/2, initPos);
        }
    }
    
    private void CheckCameraDistance()
    {
        limitToSpawnNewFloor = camera.transform.position.x + initLimitToSpawn;
    }
}
