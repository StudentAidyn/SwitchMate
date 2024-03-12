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
    private bool m_isAvailable = false;
    public Vector2 m_position;
    private Color m_baseColour;
    public Color m_highlightColour = Color.yellow;
    // add position location so it can be loaded to the Chess pieces easier, and don't instantiate CP as a child of the Tile, instantiate possibly as child of parent, AKA chessboard, or just on top layer. :D

    // highlights the square by colouring it 
    public void Highlight() { m_image.color = m_highlightColour; m_isAvailable = true; }
    public void BaseColour() { m_image.color = m_baseColour; m_isAvailable = false; }

    public Sc_ChessPiece getCurrentCP() { return m_CP; }
    public void clearCP() { m_CP = null; }
    public Vector2 getPosition() { return m_position; }

    public void OnDrop(PointerEventData eventData)
    {
        if(!m_isAvailable) { return; }
        GameObject dropped = eventData.pointerDrag;
        Sc_Player player = dropped.GetComponent<Sc_Player>();
        bool playerCheck = (player == getCurrentCP());
        if (m_CP != null && !playerCheck) {
            Sc_ChessEnum storage = m_CP.getChessPiece();
            Destroy(m_CP.gameObject);
            player.SetCP(storage, false);
        }
        player.getParent().clearCP();
        player.setParentTile(this);
        m_CP = player;
    }

    // Start is called before the first frame update
    void Awake()
    {
       if(!m_image) { m_image = gameObject.GetComponent<UnityEngine.UI.Image>(); }
       m_RT = gameObject.GetComponent<RectTransform>();
    }

    public void InitialiseTile(int _x, int _y, float _size, Color _color, Sc_ChessPiece _CP = null)
    {
        m_RT.sizeDelta = new Vector2(_size, _size);
        m_position = new Vector2(_x, _y);
        m_baseColour = _color;
        m_image.color = m_baseColour;
        m_CP = _CP;
        if (m_CP != null)
        {
            m_CP.setParentTile(this);
        }
    }
}
