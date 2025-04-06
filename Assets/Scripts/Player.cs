using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform toucam;
    public Transform camtrs;
    public EctJoy ect;
    public GameObject tou;
    public PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl=new Move();
        
    }

    // Update is called once per frame
    void Update()
    {
        playerControl.Do(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="tou")
        {
            Camera.main.transform.position=toucam.position;
            playerControl=new Tou();
            GameObject TouRound=Instantiate(Resources.Load<GameObject>("TouRound"));
            TouRound.transform.position=new Vector3(-12,0,16);
            tou=TouRound;
        }
    }
}
