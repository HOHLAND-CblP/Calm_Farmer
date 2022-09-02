using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : MonoBehaviour
{
    [Header("Garden Bed Parametrs")]
    public float growthTime;

    [Header("Prefabs")]
    public GameObject wheatPrefab;

    [Header("Izy Slice")]
    public Material wheatMaterial;

    float[] borderCoordinates;

    void Start()
    {
        borderCoordinates = new float[4] { -transform.localScale.x / 2, -transform.localScale.y / 2, transform.localScale.x / 2, transform.localScale.y / 2 };

        SpawnWheatAtStart();
    }


    void Update()
    {
        
    }

    void SpawnWheatAtStart()
    {
        float radiusWheat = wheatPrefab.GetComponent<Crop>().radius;

        Vector2Int countWheat_Axe = new Vector2Int((int)(transform.localScale.x / radiusWheat), (int)(transform.localScale.z / radiusWheat));


        for (int i = 0; i < countWheat_Axe.x; i++)
            for (int j = 0; j < countWheat_Axe.y; j++)
            {
                GameObject crop = Instantiate(wheatPrefab);
                crop.transform.SetParent(transform);


                crop.transform.localPosition = new Vector3((i * radiusWheat + radiusWheat / 2) / (countWheat_Axe.x * radiusWheat) - 0.5f, 0.1f, (j * radiusWheat + radiusWheat / 2) / (countWheat_Axe.y * radiusWheat)-0.5f);
                crop.GetComponent<Crop>().SetGardenBed(this);
            }
    }


    public void NewCrop(Vector3 position)
    {
        GameObject crop = Instantiate(wheatPrefab);
        crop.transform.SetParent(transform);


        crop.transform.localPosition = position;
        crop.GetComponent<Crop>().SetGardenBed(this);
    }
}