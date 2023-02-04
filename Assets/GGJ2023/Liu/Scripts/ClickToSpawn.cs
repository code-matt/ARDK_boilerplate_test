using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSpawn : MonoBehaviour
{
    public GameObject prefabToSpawn;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 spawnPosition = hit.point;
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(prefabToSpawn, spawnPosition, spawnRotation);
            }
        }
    }
}
