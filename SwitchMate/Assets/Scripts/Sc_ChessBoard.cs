using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject m_PlayerLayer = null;
    public CanvasScaler m_MainCanvas = null;
    private Sc_Player m_player;

    List<Sc_ChessTile> m_tiles = new List<Sc_ChessTile>();

    // Start is called before the first frame update
    void Start()
    {
        // currently builds chess board from start, but preferably in the future it will grab a txt file which then can be used by other people to make their own levels.
        BuildChessBoard(m_CBSize);
        CheckMoveSet();
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

        // Chess Piece loader.
        Sc_ChessEnum CPE = 0;
        float canvasRatio = (m_MainCanvas.referenceResolution.y > m_MainCanvas.referenceResolution.x) ? m_MainCanvas.referenceResolution.x / m_MainCanvas.referenceResolution.y : m_MainCanvas.referenceResolution.y / m_MainCanvas.referenceResolution.x;
        //canvasRatio -= 0.1f;

        float adjustmentRatio = Screen.width / m_MainCanvas.referenceResolution.x;
        m_CBSize = boardSize;
        float offset = (m_CBSize * m_size) * (adjustmentRatio) * 0.5f;
        for (int i = 0; i < m_CBSize; i++)
        {
            for (int j = 0; j < m_CBSize; j++)
            {
                GameObject tileObject = Instantiate(m_TilePrefab, transform.position + new Vector3(j * m_size * adjustmentRatio - offset, i * m_size * adjustmentRatio - offset, 0), Quaternion.identity, this.transform);
                Sc_ChessTile tile = tileObject.GetComponent<Sc_ChessTile>();
                if(!tileObject.GetComponent<Sc_ChessTile>()) { Debug.Log("FAILED TO GRAB CHESS TILE"); }
                int even = (i + j) % 2;
                Color color = (even == 1) ? Color.black : Color.white;

                Sc_ChessPiece CP = null;
                if (i == 1 && j == 4)
                {


                    GameObject CPObject = Instantiate(m_CPPPrefab, tile.gameObject.transform.position, Quaternion.identity, m_PlayerLayer.transform);
                    CP = CPObject.GetComponent<Sc_Player>();
                    CP.CPInit(Sc_ChessEnum.Rook, m_size, false);
                    m_player = CP.gameObject.GetComponent<Sc_Player>();
                }
                else if (i == 0)
                {
                    GameObject CPObject = Instantiate(m_CPPrefab, tile.gameObject.transform.position, Quaternion.identity, m_CPLayer.transform);
                    CP = CPObject.GetComponent<Sc_ChessPiece>();
                    CPE++;
                    if (CPE > Sc_ChessEnum.Pawn)
                    {
                        CPE = 0;
                    }
                    CP.CPInit(CPE, m_size);
                }
                tile.InitialiseTile(j, i, m_size, color, CP);
                m_tiles.Add(tile.GetComponent<Sc_ChessTile>());


            }
        }
    }

    void CheckMoveSet()
    {
        //List<Sc_ChessTile> moves = new List<Sc_ChessTile>();
        //moves = m_player.getAvailableMoves(ref m_tiles, ref m_CBSize);
        //m_player.getAvailableMoves(ref m_tiles, ref m_CBSize);
        foreach (Sc_ChessTile tile in m_player.getAvailableMoves(ref m_tiles, ref m_CBSize))
        {
            Debug.Log("GETTING TILES");
            tile.Highlight();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (m_player)
        {
            Vector2 pos = m_player.getParent().getPosition();
            //m_tiles[pos.x, pos.y].
        }

        // put all possible tiles within a list, 
        // get current tile position within array and then change all tiles that are available to highlighted.
        // when player is placed reset all tiles, clear list and recalculate.
    }


}
