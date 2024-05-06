using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scrShieldManager : MonoBehaviour
{
    public int Charge; //the charge of shield. 4 fish = 1 shield
    public TextMeshProUGUI ChargeText;
    public GameObject Shield;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChargeText.text = "Shield Charge: %" + Charge;
        if (Charge >= 50 && GameObject.FindGameObjectWithTag("shield") == null)
        {
            Instantiate(Shield, GameObject.FindGameObjectWithTag("Player").transform.position, new Quaternion());
            Charge = 0;
        }
    }
}
