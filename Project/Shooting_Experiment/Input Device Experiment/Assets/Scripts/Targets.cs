using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    // public StartExp exp;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.tag == "Target") {//shot hit, increasing accuracy
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            Debug.Log("Hit target!");
            StartExp.setHit(StartExp.getHit() + 1);
        }
        else if (collision.gameObject.tag == "Wall") {//shot missed
            Destroy(this.gameObject);
            Debug.Log("Missed Target!");
            StartExp.setMissed(StartExp.getMissed() + 1);
        }
    }

}
