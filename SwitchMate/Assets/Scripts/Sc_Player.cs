using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;


public class Sc_Player : Sc_ChessPiece, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    int m_currentMoves;
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

            case Sc_ChessEnum.Queen:
                Queen(ref _tiles, ref r, pos, ref _CBSize);
                break;
            case Sc_ChessEnum.Bishop:
                Bishop(ref _tiles, ref r, pos, ref _CBSize);
                break;
            case Sc_ChessEnum.Knight:
                Knight(ref _tiles, ref r, pos, ref _CBSize);
                break;
            case Sc_ChessEnum.Rook:
                Rook(ref _tiles, ref r, pos, ref _CBSize);
                break;

            default:
                break;
        }

        return r;
    }

    void Rook(ref List<Sc_ChessTile> _tiles, ref List<Sc_ChessTile> _rooks, Vector2 pos, ref int _CBSize)
    {
        // top check
        for (int y = (int)pos.y + 1; y < _CBSize; y++)
        {
            Sc_ChessTile tile = _tiles[y * _CBSize + (int)pos.x];
            if (tile.getCurrentCP() == null)
            {
                _rooks.Add(tile);
            }
            else
            {
                _rooks.Add(tile);
                break;
            }
        }
        // below check
        for (int y = (int)pos.y - 1; y >= 0; y--)
        {
            Sc_ChessTile tile = _tiles[y * _CBSize + (int)pos.x];
            if (tile.getCurrentCP() == null)
            {
                _rooks.Add(tile);
            }
            else
            {
                _rooks.Add(tile);
                break;
            }
        }
        // left check
        for (int x = (int)pos.x - 1; x >= 0; x--)
        {
            Sc_ChessTile tile = _tiles[(int)pos.y * _CBSize + x];
            if (tile.getCurrentCP() == null)
            {
                _rooks.Add(tile);
            }
            else
            {
                _rooks.Add(tile);
                break;
            }
        }
        // right check
        for (int x = (int)pos.x + 1; x < _CBSize; x++)
        {
            Sc_ChessTile tile = _tiles[(int)pos.y * _CBSize + x];
            if (tile.getCurrentCP() == null)
            {
                _rooks.Add(tile);
            }
            else
            {
                _rooks.Add(tile);
                break;
            }
        }
    }

    void Queen(ref List<Sc_ChessTile> _tiles, ref List<Sc_ChessTile> _Movements, Vector2 pos, ref int _CBSize) {
        // top check
        for (int y = (int)pos.y + 1; y < _CBSize; y++)
        {
            Sc_ChessTile tile = _tiles[y * _CBSize + (int)pos.x];
            if (tile.getCurrentCP() == null)
            {
                _Movements.Add(tile);
            }
            else
            {
                _Movements.Add(tile);
                break;
            }
        }
        // below check
        for (int y = (int)pos.y - 1; y >= 0; y--)
        {
            Sc_ChessTile tile = _tiles[y * _CBSize + (int)pos.x];
            if (tile.getCurrentCP() == null)
            {
                _Movements.Add(tile);
            }
            else
            {
                _Movements.Add(tile);
                break;
            }
        }
        // left check
        for (int x = (int)pos.x - 1; x >= 0; x--)
        {
            Sc_ChessTile tile = _tiles[(int)pos.y * _CBSize + x];
            if (tile.getCurrentCP() == null)
            {
                _Movements.Add(tile);
            }
            else
            {
                _Movements.Add(tile);
                break;
            }
        }
        // right check
        for (int x = (int)pos.x + 1; x < _CBSize; x++)
        {
            Sc_ChessTile tile = _tiles[(int)pos.y * _CBSize + x];
            if (tile.getCurrentCP() == null)
            {
                _Movements.Add(tile);
            }
            else
            {
                _Movements.Add(tile);
                break;
            }
        }

        // --- DIAGONALS ---
        // top right
        for(int x = (int)pos.x + 1, y = (int)pos.y + 1; x < _CBSize && y < _CBSize; x++, y++)
        {
            Sc_ChessTile tile = _tiles[y * _CBSize + x];
            if (tile.getCurrentCP() == null)
            {
                _Movements.Add(tile);
            }
            else
            {
                _Movements.Add(tile);
                break;
            }
        }

        // top left
        for (int x = (int)pos.x - 1, y = (int)pos.y + 1; x >= 0 && y < _CBSize; x--, y++)
        {
            Sc_ChessTile tile = _tiles[y * _CBSize + x];
            if (tile.getCurrentCP() == null)
            {
                _Movements.Add(tile);
            }
            else
            {
                _Movements.Add(tile);
                break;
            }
        }
        // bottom right
        for (int x = (int)pos.x + 1, y = (int)pos.y - 1; x < _CBSize && y >= 0; x++, y--)
        {
            Sc_ChessTile tile = _tiles[y * _CBSize + x];
            if (tile.getCurrentCP() == null)
            {
                _Movements.Add(tile);
            }
            else
            {
                _Movements.Add(tile);
                break;
            }
        }
        // bottom left
        for (int x = (int)pos.x - 1, y = (int)pos.y - 1; x >= 0 && y >= 0; x--, y--)
        {
            Sc_ChessTile tile = _tiles[y * _CBSize + x];
            if (tile.getCurrentCP() == null)
            {
                _Movements.Add(tile);
            }
            else
            {
                _Movements.Add(tile);
                break;
            }
        }
    }
    void Knight(ref List<Sc_ChessTile> _tiles, ref List<Sc_ChessTile> _Movements, Vector2 pos, ref int _CBSize) {

        // Top Right
        int x = (int)pos.x + 1;
        int y = (int)pos.y + 2;
        if(x < _CBSize && y < _CBSize)
        {
            _Movements.Add(_tiles[y * _CBSize + x]);
        }

        // top left
        x = (int)pos.x - 1;
        y = (int)pos.y + 2;
        if (x >= 0 && y < _CBSize)
        {
            _Movements.Add(_tiles[y * _CBSize + x]);
        }

        // left top
        x = (int)pos.x - 2;
        y = (int)pos.y + 1;
        if (x >= 0 && y < _CBSize)
        {
            _Movements.Add(_tiles[y * _CBSize + x]);
        }

        //left bottom
        x = (int)pos.x - 2;
        y = (int)pos.y - 1;
        if (x >= 0 && y >= 0)
        {
            _Movements.Add(_tiles[y * _CBSize + x]);
        }

        // right Top
        x = (int)pos.x + 2;
        y = (int)pos.y + 1;
        if (x < _CBSize && y < _CBSize)
        {
            _Movements.Add(_tiles[y * _CBSize + x]);
        }

        // right bottom
        x = (int)pos.x + 2;
        y = (int)pos.y - 1;
        if (x < _CBSize && y >= 0)
        {
            _Movements.Add(_tiles[y * _CBSize + x]);
        }

        // bottom left
        x = (int)pos.x - 1;
        y = (int)pos.y - 2;
        if (x >= 0 && y >= 0)
        {
            _Movements.Add(_tiles[y * _CBSize + x]);
        }
        // bottom right
        x = (int)pos.x + 1;
        y = (int)pos.y - 2;
        if (x < _CBSize && y >= 0)
        {
            _Movements.Add(_tiles[y * _CBSize + x]);
        }
    }

    void Bishop(ref List<Sc_ChessTile> _tiles, ref List<Sc_ChessTile> _Movements, Vector2 pos, ref int _CBSize) {
        // --- DIAGONALS ---
        // top right
        for (int x = (int)pos.x + 1, y = (int)pos.y + 1; x < _CBSize && y < _CBSize; x++, y++)
        {
            Sc_ChessTile tile = _tiles[y * _CBSize + x];
            if (tile.getCurrentCP() == null)
            {
                _Movements.Add(tile);
            }
            else
            { 
                _Movements.Add(tile);
                break;
            }
        }

        // top left
        for (int x = (int)pos.x - 1, y = (int)pos.y + 1; x >= 0 && y < _CBSize; x--, y++)
        {
            Sc_ChessTile tile = _tiles[y * _CBSize + x];
            if (tile.getCurrentCP() == null)
            {
                _Movements.Add(tile);
            }
            else
            {
                _Movements.Add(tile);
                break;
            }
        }
        // bottom right
        for (int x = (int)pos.x + 1, y = (int)pos.y - 1; x < _CBSize && y >= 0; x++, y--)
        {
            Sc_ChessTile tile = _tiles[y * _CBSize + x];
            if (tile.getCurrentCP() == null)
            {
                _Movements.Add(tile);
            }
            else
            {
                _Movements.Add(tile);
                break;
            }
        }
        // bottom left
        for (int x = (int)pos.x - 1, y = (int)pos.y - 1; x >= 0 && y >= 0; x--, y--)
        {
            Sc_ChessTile tile = _tiles[y * _CBSize + x];
            if (tile.getCurrentCP() == null)
            {
                _Movements.Add(tile);
            }
            else
            {
                _Movements.Add(tile);
                break;
            }
        }
    }
    void Pawn(ref List<Sc_ChessTile> _tiles, ref List<Sc_ChessTile> _rooks, Vector2 pos, ref int _CBSize) { 
        
    }
    void MoveSet()
    {
        
    }
}
