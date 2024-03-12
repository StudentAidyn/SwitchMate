using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// within Image, Chess Piece has it's raycast Target Disable to not interfere with the players interactions.
public class Sc_ChessPiece : MonoBehaviour {
    protected Sc_ChessEnum m_currentCP = 0;
    protected RectTransform m_RT;
    public Image m_image;
    public Sc_ChessTile m_parentTile;
    bool m_isActive = true;
    
    public Sc_ChessEnum getChessPiece() { return m_currentCP; }
    public void setParentTile(Sc_ChessTile _parent) { m_parentTile = _parent;  }
    virtual protected void Awake()
    {
        m_RT = GetComponent<RectTransform>();
        m_image = gameObject.GetComponent<Image>();
    }

    public void CPInit(Sc_ChessEnum _CPE, float _size, bool _isBlack = true)
    {
        m_RT.sizeDelta = new Vector2(_size, _size);
        SetCP(_CPE, _isBlack);
    }

    // CPE is ChessPieceEnumerator and isBlack declares whether the set piece is black.
    public void SetCP(Sc_ChessEnum _CPE, bool _isBlack = true)
    {
        
        m_currentCP = _CPE;
        m_image.sprite = Sc_ChessSprites.LoadSprite(m_currentCP, _isBlack);
    }
}
    