using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sc_Player : Sc_ChessPiece, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData) 
    {
        Debug.Log("BeginDrag");
        m_image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        Debug.Log("Dragged");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // check if currently over the board or an available square. 
        // if so, move player back to square they were just at, 
        // if on an available square move them to that square and adjust tile accordingly.
        transform.position = m_parentTile.position;
        m_image.raycastTarget = true;
        Debug.Log("EndDrag");
    }

    override protected void Awake() {
        base.Awake();
    }
    void MoveSet()
    {
        
    }
}
