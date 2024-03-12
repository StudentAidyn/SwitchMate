using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Sc_Player : Sc_ChessPiece, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Sc_ChessTile getParent() { return m_parentTile; }
    public void OnBeginDrag(PointerEventData eventData) 
    {
        m_image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = m_parentTile.transform.position;
        m_image.raycastTarget = true;
    }

    override protected void Awake() {
        base.Awake();
    }

    public List<Sc_ChessTile> getAvailableMoves(ref List<Sc_ChessTile> _tiles, ref int _CBSize)
    {
        // put all possible tiles within a list, 
        // get current tile position within array and then change all tiles that are available to highlighted.
        // when player is placed reset all tiles, clear list and recalculate.
        List<Sc_ChessTile> r = new List<Sc_ChessTile>();

        // player's current location in the chessboard array
        Vector2 pos = getParent().getPosition();
        switch (m_currentCP)
        {
            case Sc_ChessEnum.Rook:
                // top check
                for(int y = (int)pos.y + 1; y < _CBSize; y++)
                {
                    Sc_ChessTile tile = _tiles[y * _CBSize + (int)pos.x];
                    if (tile.getCurrentCP() == null)
                    {
                        r.Add(tile);
                    }
                    else
                    {
                        r.Add(tile);
                        break;
                    }
                }
                // below check
                for (int y = (int)pos.y - 1; y >= 0; y--)
                {
                    Sc_ChessTile tile = _tiles[y * _CBSize + (int)pos.x];
                    if (tile.getCurrentCP() == null)
                    {
                        r.Add(tile);
                    }
                    else
                    {
                        r.Add(tile);
                        break;
                    }
                }
                // left check
                for (int x = (int)pos.x - 1; x >= 0 ; x--)
                {
                    Sc_ChessTile tile = _tiles[(int)pos.y * _CBSize + x];
                    if (tile.getCurrentCP() == null)
                    {
                        r.Add(tile);
                    }
                    else
                    {
                        r.Add(tile);
                        break;
                    }
                }
                // right check
                for (int x = (int)pos.x + 1; x < _CBSize; x++)
                {
                    Sc_ChessTile tile = _tiles[(int)pos.y * _CBSize + x];
                    if (tile.getCurrentCP() == null)
                    {
                        r.Add(tile);
                    }
                    else
                    {
                        r.Add(tile);
                        break;
                    }
                }
                break;
            default:
                break;
        }

        return r;
    }
    void MoveSet()
    {
        
    }
}
