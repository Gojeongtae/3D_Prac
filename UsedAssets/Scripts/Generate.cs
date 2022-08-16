using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public GameObject Heart;
    public GameObject Star;
    public GameObject Money;
    public GameObject Clock;

    public Transform HeartPos;
    public Transform StartPos;
    public Transform MoneyPos;
    public Transform ClockPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShotHeart");
    }

    // Update is called once per frame
    void Update()
    {
       


    }

    IEnumerator ShotHeart()
    {
        GameObject instantHeart = Instantiate(Heart, HeartPos.position, HeartPos.rotation);
        Rigidbody bulletRigid = instantHeart.GetComponent<Rigidbody>();
        bulletRigid.velocity = HeartPos.forward * 5;
        
        yield return null;

    }
}

