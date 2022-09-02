using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
    [Header("Character Parametrs")]
    [SerializeField] float speed;
    bool canMove;
    float moveH, moveV;

    int moneyCount;
    
    [Space]
    public int maxDropCount;
    int curDropCount;
    
    [Header("Character Components")]
    public Animator anim;
    public CharacterAnimRequest CAR;

    [Space]
    public Joystick joystick;

    [Space]
    public GameObject scythe;

    [Header("Text Components")]
    public TextMeshProUGUI textDropCount;
    public TextMeshProUGUI textMoneyCount;


    //cache
    Camera mainCam;


    void Start()
    {
        canMove = true;

        moneyCount = 0;

        curDropCount = 0;
        textDropCount.text = curDropCount.ToString() + "/" + maxDropCount.ToString();


        mainCam = Camera.main;

        CAR.endMow.AddListener(EndMow);
        CAR.mowIsReady.AddListener(scythe.GetComponent<Scythe>().MowIsReady);
    }

    void Update()
    {
        if (canMove)
        {                
            Vector3 move = (Vector3.forward * moveV + Vector3.right * moveH);
            if (move.magnitude > 1)
                move.Normalize();

            anim.SetFloat("Speed", move.magnitude);

            
            if (move != new Vector3(0,0,0))
                transform.rotation = Quaternion.LookRotation(move);


            transform.Translate(Vector3.forward * speed * move.magnitude * Time.deltaTime);
        }
    }


    public void SetDirection(Vector2 direction)
    {
        moveH = direction.x;
        moveV = direction.y;
    }


    public void EndMow()
    {
        anim.SetBool("Mow", false);
        canMove = true;

        scythe.GetComponent<Scythe>().MowIsntReady();
        scythe.SetActive(false);
    }


    public void TakeDrop(GameObject drop)
    {
        if (curDropCount < maxDropCount)
        {
            curDropCount++;
            textDropCount.text = curDropCount.ToString() + "/" + maxDropCount.ToString();

            Destroy(drop);
        }
        else
        {
        }
    }



    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Crop"))
        {
            anim.SetBool("Mow", true);
            canMove = false;
            scythe.SetActive(true);
        }

        if (col.CompareTag("House"))
        {

            moneyCount += curDropCount * 15;
            textMoneyCount.text = moneyCount.ToString();

            curDropCount = 0;
            textDropCount.text = curDropCount.ToString() + "/" + maxDropCount.ToString();
        }
    }
}