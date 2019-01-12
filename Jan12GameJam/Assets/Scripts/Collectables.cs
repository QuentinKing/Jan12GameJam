using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public GameObject collectable1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)

            Instantiate(collectable1, new Vector3(i * 3, i * 3, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void collected()
    {
        Destroy(collectable1);
    }
}
