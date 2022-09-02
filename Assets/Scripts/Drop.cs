using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [Header("Parametrs")]
    public float amplitude;
    public int speedFlying;


    int state;

    Transform targetTransform;
    Vector3 targetPoint;


    public void InitializeDrop()
    {
        state = 0;

    }

    void Update()
    {
        switch (state)
        {
            case 0:
                transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time / 5, amplitude) + 0.2f, transform.position.z);
                break;
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, transform.position, speedFlying * Time.deltaTime);
                
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Character>().TakeDrop(gameObject);
        }
    }
}
