using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_ChessSprites : MonoBehaviour
{

    public List<SpriteContainer> sprites = new List<SpriteContainer>();

    private static Sc_ChessSprites _instance = null;

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


    public static Sprite LoadSprite(Sc_ChessEnum _pieceID, bool _isBlack)
    {
        foreach(SpriteContainer sprite in _instance.sprites)
        {
            if(sprite._pieceID == _pieceID)
            {
                return (_isBlack) ? sprite._black : sprite._white;
            } 
        }
        return null;
    }
}

[System.Serializable]
public struct SpriteContainer  
{
    public Sprite _black;
    public Sprite _white;
    public Sc_ChessEnum _pieceID;
}
