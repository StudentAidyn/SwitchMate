using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Sc_ChessTile : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private UnityEngine.UI.Image m_image = null;
    private Sc_ChessPiece m_CP = null;
    private RectTransform m_RT = null;
    public Transform m_position;
    // add position location so it can be loaded to the Chess pieces easier, and don't instantiate CP as a child of the Tile, instantiate possibly as child of parent, AKA chessboard, or just on top layer. :D


    Sc_ChessPiece GetCurrentCP() { return m_CP; }
    Transform GetPosition() { return m_position; }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Sc_Player player = dropped.GetComponent<Sc_Player>();
        player.setParentTile(m_position);
    }

    // Start is called before the first frame update
    void Awake()
    {
       if(!m_image) { m_image = gameObject.GetComponent<UnityEngine.UI.Image>(); }
       m_RT = gameObject.GetComponent<RectTransform>();
    }

    public void InitialiseTile(float _size, Color _color, Sc_ChessPiece _CP = null)
    {
        m_RT.sizeDelta = new Vector2(_size, _size);
        m_position = gameObject.transform;
        m_image.color = _color;
        m_CP = _CP;
        if (m_CP != null)
        {
            m_CP.setParentTile(m_position);
        }
    }
}
