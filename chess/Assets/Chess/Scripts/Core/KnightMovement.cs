using Chess.Scripts.Core;
using UnityEngine;

public class KnightMovement : MonoBehaviour,IMovement
{
    private ChessPlayerPlacementHandler cph;
    void Awake()
    {
        cph = GetComponent<ChessPlayerPlacementHandler>();
    }
    public void move(){
        knightMove();
    }

    void knightMove(){
        int row = cph.row;
        int col = cph.column;

        // 
        if(checkValid(row +1,col+2)){
            ChessBoardPlacementHandler.Instance.Highlight(row+1,col+2);
        }
        if(checkValid(row -1,col+2)){
            ChessBoardPlacementHandler.Instance.Highlight(row-1,col+2);
        }

        if(checkValid(row +2,col+1)){
            ChessBoardPlacementHandler.Instance.Highlight(row+2,col+1);
        }
        if(checkValid(row +2,col-1)){
            ChessBoardPlacementHandler.Instance.Highlight(row+2,col-1);
        }

        if(checkValid(row +1,col-2)){
            ChessBoardPlacementHandler.Instance.Highlight(row+1,col-2);
        }
        if(checkValid(row -1,col-2)){
            ChessBoardPlacementHandler.Instance.Highlight(row-1,col-2);
        }

        if(checkValid(row -2,col-1)){
            ChessBoardPlacementHandler.Instance.Highlight(row-2,col-1);
        }
        if(checkValid(row -2,col+1)){
            ChessBoardPlacementHandler.Instance.Highlight(row-2,col+1);
        }
        
    }

    // can also be declared in the singleton class
    bool checkValid(int i,int j){
        if(i<0 || i>7 || j<0 || j>7) return false;// to stay within the (rows and columns)range

        Vector2 tilePos = ChessBoardPlacementHandler.Instance._chessBoard[i,j].transform.position;
        RaycastHit2D hit = Physics2D.Raycast(tilePos,Vector2.zero);
        if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Players")){
        if(hit.collider.gameObject.tag != gameObject.tag) 
        ChessBoardPlacementHandler.Instance.HighlightRed(i,j);
        return false;
        }
        return true;
    }
    
}
