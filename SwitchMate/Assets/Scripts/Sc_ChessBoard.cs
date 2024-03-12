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

// SINGLETON
public class Sc_ChessBoard : MonoBehaviour
{
    static public int m_CBSize = 8;
    public float m_size = 10f;
    public GameObject m_TilePrefab = null;
    public GameObject m_CPPrefab = null;    // ChessPiece Prefab
    public GameObject m_CPPPrefab = null;   // ChessPiece Player Prefab
    public GameObject m_CPLayer = null;
    public GameObject m_PlayerLayer = null;
    public CanvasScaler m_MainCanvas = null;
    static private Sc_Player m_player;
    
    static List<Sc_ChessTile> m_tiles = new List<Sc_ChessTile>();
    static List<Sc_ChessTile> m_moves = new List<Sc_ChessTile>();

    // Start is called before the first frame update
    void Start()
    {
        // currently builds chess board from start, but preferably in the future it will grab a txt file which then can be used by other people to make their own levels.
        BuildChessBoard(m_CBSize);
        CheckMoveSet();
    }

    private static Sc_ChessBoard _instance = null;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            // There is already a singleton loaded. Destroy ourself!
            Destroy(this.gameObject);
            return;
        }
        // We were first, let this object be the singleton! :)
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }



    // add in editor builder??
    public void BuildChessBoard(int boardSize)
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

    public static void CheckMoveSet()
    {
        // checks, resets and clears the current tiles selected
        if(m_moves.Count != 0 ) { 
            foreach(Sc_ChessTile tile in m_moves)
            {
                tile.BaseColour();
            }
                m_moves.Clear(); 
        }
        m_moves = m_player.getAvailableMoves(ref m_tiles, ref m_CBSize);
        // hightlights all the current tiles the player can move
        foreach (Sc_ChessTile tile in m_moves)
        {
            tile.Highlight();
        }
    }
}
