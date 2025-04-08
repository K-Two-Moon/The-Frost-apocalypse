using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpScript : MonoBehaviour
{
    Slider Hp;
    // Start is called before the first frame update
    void Start()
    {
        Hp=Instantiate(Resources.Load<Slider>("HpSlider"),GameObject.Find("Canvas").transform);
        Hp.transform.position=Camera.main.WorldToScreenPoint(transform.position+transform.up*2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
