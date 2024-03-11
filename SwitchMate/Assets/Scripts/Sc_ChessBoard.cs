using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 0. King
// 1. Queen
// 2. Bishop
// 3. Knight
// 4. Rook
// 5. Pawn

public class Sc_ChessBoard : MonoBehaviour
{
    public int m_CBSize = 8;
    public float m_size = 10f;
    public GameObject m_TilePrefab = null;
    public GameObject m_CPPrefab = null;    // ChessPiece Prefab
    public GameObject m_CPPPrefab = null;   // ChessPiece Player Prefab
    public GameObject m_CPLayer = null;

    List<Sc_ChessTile> m_tiles = new List<Sc_ChessTile>();

    // Start is called before the first frame update
    void Start()
    {
        BuildChessBoard(m_CBSize);

    }

    // add in editor builder??
    void BuildChessBoard(int boardSize)
    {
        // Clear the current tiles
        if (m_tiles.Count != 0) {
            foreach(Sc_ChessTile tile in m_tiles)
            {
                Destroy(tile.gameObject);
            }
            m_tiles.Clear();
        }

        m_CBSize = boardSize;
        float offset = (m_CBSize * m_size) * 0.5f - (m_size);
        for (int i = 0; i < m_CBSize - 1; i++)
        {
            for (int j = 0; j < m_CBSize - 1; j++)
            {
                GameObject tileObject = Instantiate(m_TilePrefab, transform.position + new Vector3(i * m_size - offset, j * m_size - offset, 0), Quaternion.identity, this.transform);
                Sc_ChessTile tile = tileObject.GetComponent<Sc_ChessTile>();
                if(!tileObject.GetComponent<Sc_ChessTile>()) { Debug.Log("FAILED TO GRAB CHESS TILE"); }
                int even = (i + j) % 2;
                Color color = (even == 1) ? Color.black : Color.white;

                Sc_ChessPiece CP = null;
                if (i == 0)
                {
                    GameObject CPObject = Instantiate(m_CPPrefab, tile.gameObject.transform.position, Quaternion.identity, m_CPLayer.transform);
                    CP = CPObject.GetComponent<Sc_ChessPiece>();
                    CP.CPInit(Sc_ChessEnum.Rook, m_size);
                }
                else if( i == 1 && j == 4)
                {
                    GameObject CPObject = Instantiate(m_CPPPrefab, tile.gameObject.transform.position, Quaternion.identity, m_CPLayer.transform);
                    CP = CPObject.GetComponent<Sc_ChessPiece>();
                    CP.CPInit(Sc_ChessEnum.Bishop, m_size, false);
                }
                tile.InitialiseTile(m_size, color, CP);
                m_tiles.Add(tile.GetComponent<Sc_ChessTile>());


            }
        }
    }


    void SetPlayer()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
