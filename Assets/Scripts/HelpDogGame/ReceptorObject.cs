using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using
UnityEngine.Events;
public class ReceptorObject : MonoBehaviour
{

    [SerializeField] private UnityEvent execute;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Correct"))
        {
            Debug.Log("Is Correct item");
            execute.Invoke();
        }

        if (collision.CompareTag("Incorrect"))
        {
            Debug.Log("Is Correct item");
        }
    }
}
