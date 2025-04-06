using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tou :PlayerControl
{
    public override void Do(GameObject Player)   
    {
        float V=Player.GetComponent<Player>().ect.SetMove("V");
        float H=Player.GetComponent<Player>().ect.SetMove("H");
         if(V!=0||H!=0)
        {
            Player.GetComponent<Player>().tou.transform.position+=new Vector3(H,0,V)*Time.deltaTime*10;
        }
    }
}
