using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Crop : MonoBehaviour
{
    [Header("Parametrs")]
    public float radius; 
    public float growingTime;
    bool isGrown;

    public float startHieght;
    public float endHieght;

    [Header("Components")]
    public Material cropSliceMaterial;

    [Header("Drop")]
    public GameObject dropPrefab;
    public Vector3 dropPosition;

    CapsuleCollider cc;
    GardenBed gb;

    float growthTime;

    void Start()
    {
        transform.localScale =  new Vector3(transform.localScale.x, 0.2f * transform.localScale.y, transform.localScale.z);
        

        isGrown = false;


        if (startHieght > endHieght)
            endHieght = startHieght;


        cc = GetComponent<CapsuleCollider>();
        cc.enabled = false;


        growthTime = 0;
    }

    void Update()
    {
        growthTime += Time.deltaTime;

        if(growthTime >= growingTime)
        {
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);

            cc.enabled = true;

            isGrown = true;
            enabled = false;
        }
        else
        {
            
        }

        transform.localScale = new Vector3(transform.localScale.x, Mathf.Clamp(growthTime / growingTime * (1 - startHieght) + startHieght, 0, 1), transform.localScale.z);
    }

    public void StartOfGrowth()
    {
        transform.localScale = new Vector3(1, startHieght, 1);
    }

    public void Mowing()
    {
        if (isGrown)
        {
            isGrown = false;
            cc.enabled = false;;

            GameObject[] go = transform.GetChild(0).gameObject.SliceInstantiate(transform.position + new Vector3(0, 0.1f, 0), new Vector3(0, 1, 0), cropSliceMaterial);
            Destroy(go[0]);
            go[1].transform.parent = transform;
            go[1].transform.localPosition = new Vector3(0, 0, 0);


            Destroy(transform.GetChild(0).gameObject);

            Drop();

            StartCoroutine(AfterMowing());
        }
    }


    void Drop()
    {
        GameObject drop = Instantiate(dropPrefab, transform.position + dropPosition, Quaternion.identity);

        drop.GetComponent<Drop>().InitializeDrop();
    }


    IEnumerator AfterMowing()
    {
        growthTime = 0;

        yield return new WaitForSeconds(2);

        gb.NewCrop(transform.localPosition);
        Destroy(gameObject);
    }

    public void SetGardenBed(GardenBed gardenBed)
    {
        gb = gardenBed;
    }
}