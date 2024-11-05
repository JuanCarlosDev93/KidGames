using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    Vector2 mousePos;
    public Vector2 initialPos;
    public bool resetPos = true;
    [SerializeField] private Transform initialParent;


    private void Awake()
    {
        initialParent = transform.parent;
        initialPos = transform.position;
    }
    private void OnMouseDown()
    {
        if (gameManager.canInteract == false) return;
        MoveToMousePos();
    }
    private void OnMouseDrag()
    {
        if (gameManager.canInteract == false) return;
        MoveElement();
    }
    private void OnMouseUp()
    {
        if (gameManager.canInteract == false) return;
        ResetElementPos();
    }

    public void MoveElement()
    {
        
        if (resetPos)
        {
            RemoveParent();

#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

#else

if (Input.touchCount > 0)
        {
            
        Touch touch = Input.GetTouch(0);

            //Move once to finger position
            if (touch.phase == TouchPhase.Began)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);                
            }            
        }

#endif

            transform.position = mousePos;
        }
    }
    public void MoveToMousePos()
    {

        if (resetPos)
        {
            RemoveParent();

#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

#else



if (Input.touchCount > 0)
        {
            
        Touch touch = Input.GetTouch(0);

            //Move once to finger position
            if (touch.phase == TouchPhase.Began)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            
        }

#endif

            transform.position = mousePos;
        }
    }
    void ResetElementPos()
    {
        if (resetPos)
        {
            transform.DOMove(initialPos, 0.2f).OnComplete(() => RestoreParent());
        }
    }
    public void NewElementPos(Transform newPos, Transform objectToMove)
    {
        objectToMove.DOMove(newPos.position, 0.2f);
    }
    public void RemoveParent()
    {
        transform.SetParent(null);
    }
    public void RestoreParent()
    {
        transform.SetParent(initialParent);
    }
    public void SetInitialPos()
    {
        initialPos = transform.position;
    }
}
