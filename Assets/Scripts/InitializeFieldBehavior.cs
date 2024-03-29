﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeFieldBehavior : MonoBehaviour
{
    public GameObject BeastPrefab;
    public GameObject PlantPrefab;
    public int beastCount = 20;
    public float beastSpawnRateVariance = 5f;
    public int plantCount = 10;
    public Vector2 fieldSize = new Vector2(50, 50);
    
    void Start()
    {
        InitializeField();
        InitializeBeasts();
        InitializePlants();
    }

    private void InitializeField()
    {
        ScaleGround();
        PlaceWalls();
    }

    private void ScaleGround()
    {
        var targetScale = new Vector3(fieldSize.x / 2 / 5, 1, fieldSize.y / 2 / 5);
        setGameObjectScale("Ground", targetScale);
    }

    private void PlaceWalls()
    {
        setGameObjectPosition("WallDown", new Vector3(0, 0, -fieldSize.y / 2));
        setGameObjectPosition("WallUp", new Vector3(0, 0, fieldSize.y / 2));

        setGameObjectPosition("WallLeft", new Vector3(-fieldSize.x / 2, 0, 0));
        setGameObjectPosition("WallRight", new Vector3(fieldSize.x / 2, 0, 0));

        setGameObjectScale("WallDown", new Vector3(fieldSize.x / 2 / 5, 1, 1));
        setGameObjectScale("WallUp", new Vector3(fieldSize.x / 2 / 5, 1, 1));

        setGameObjectScale("WallLeft", new Vector3(fieldSize.y / 2 / 5, 1, 1));
        setGameObjectScale("WallRight", new Vector3(fieldSize.y / 2 / 5, 1, 1));
    }

    private void InitializeBeasts()
    {
        for (int i = 0; i < beastCount; i++)
        {
            var a = Random.Range(0, beastSpawnRateVariance);
            Invoke("SpawnBeast", a);
        }
    }

    void SpawnBeast()
    {
        var x = Random.Range(-fieldSize.x / 2, fieldSize.x / 2);
        var z = Random.Range(-fieldSize.y / 2, fieldSize.y / 2);
        Instantiate(BeastPrefab, new Vector3(x, 1, z), Quaternion.identity);
    }

    private void InitializePlants()
    {
        for (int i = 0; i < plantCount; i++)
        {
            var x = Random.Range(-fieldSize.x / 2, fieldSize.x / 2);
            var z = Random.Range(-fieldSize.y / 2, fieldSize.y / 2);

            Instantiate(PlantPrefab, new Vector3(x, 1, z), Quaternion.identity);
        }
    }
    private void setGameObjectScale(string objName, Vector3 scale)
    {
        var obj = this.transform.Find(objName).gameObject;
        obj.transform.localScale += new Vector3(scale.x - obj.transform.localScale.x,
                                                scale.y - obj.transform.localScale.y,
                                                scale.z - obj.transform.localScale.z);
    }
    private void setGameObjectPosition(string objName, Vector3 position)
    {
        var obj = this.transform.Find(objName).gameObject;
        obj.transform.position += new Vector3(position.x - obj.transform.localPosition.x,
                                              position.y - obj.transform.localPosition.y,
                                              position.z - obj.transform.localPosition.z);
    }
}
