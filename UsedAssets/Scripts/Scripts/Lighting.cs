using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public Collider colliderA;
    public Collider colliderB;
    public Collider colliderC;
    public Collider colliderD;
    public Collider colliderE;

    public GameObject BomwiA;
    public GameObject BomwiB;
    public GameObject BomwiC;
    public GameObject BomwiD;
    public GameObject BomwiE;

    public GameObject Light;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LightEn());
    }


    IEnumerator LightEn()
    {
        BomwiA.SetActive(true);
        BomwiB.SetActive(true);
        BomwiC.SetActive(true);
        BomwiD.SetActive(true);
        BomwiE.SetActive(true);

        yield return new WaitForSeconds(5f);

        colliderA.enabled = true;
        colliderB.enabled = true;
        colliderC.enabled = true;
        colliderD.enabled = true;
        colliderE.enabled = true;

        Light.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
        yield return null;
    }
   
}
