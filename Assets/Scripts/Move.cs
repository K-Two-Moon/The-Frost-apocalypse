using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : PlayerControl
{
    public override void Do(GameObject player)   
    {
        float V=player.GetComponent<Player>().ect.SetMove("V");
        float H=player.GetComponent<Player>().ect.SetMove("H");
         if(V!=0||H!=0)
        {
            player.transform.position+=new Vector3(H,0,V)*Time.deltaTime*5;
        }
    }
}
