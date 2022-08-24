using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Coli!");
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Walled!");
            this.gameObject.transform.SetPositionAndRotation(new Vector3(Random.Range(-8, 8), 0.3f, Random.Range(-2.7f, 7.7f)), Quaternion.identity);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
