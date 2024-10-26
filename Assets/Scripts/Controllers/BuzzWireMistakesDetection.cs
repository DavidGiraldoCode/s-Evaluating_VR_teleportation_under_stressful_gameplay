using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzWireMistakesDetection : MonoBehaviour
{
    [SerializeField] private Collider _wirePath;

    private void OnEnable()
    {
        MeshRenderer mesh = _wirePath.gameObject.GetComponent<MeshRenderer>();
        mesh.material.color = Color.white;
    }

    private void OnCollisionEnter(Collider other)
    {
        if (other == _wirePath)
        {
            MeshRenderer mesh = _wirePath.gameObject.GetComponent<MeshRenderer>();
            mesh.material.color = Color.red;
        }
    }

    private void OnCollisionExit(Collider other)
    {

        if (other == _wirePath)
        {
            MeshRenderer mesh = _wirePath.gameObject.GetComponent<MeshRenderer>();
            mesh.material.color = Color.white;
        }
    }
}
