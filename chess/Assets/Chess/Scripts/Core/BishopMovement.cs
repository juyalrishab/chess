using Chess.Scripts.Core;
using UnityEngine;

public class BishopMovement : MonoBehaviour,IMovement
{
    private ChessPlayerPlacementHandler cph;
    
    void Awake()
    {
        cph = GetComponent<ChessPlayerPlacementHandler>();
    }
    public void move(){
        bishopMove();
    }
    void bishopMove(){

        // upLeft
        int j = cph.column;
        for(int i = cph.row+1;i<8;i++){ 
            if(checkValid(i,--j)) ChessBoardPlacementHandler.Instance.Highlight(i,j);
            else break;

        }

        // upRight
        j = cph.column;
        for(int i = cph.row+1;i<8;i++){ 
            if(checkValid(i,++j)) ChessBoardPlacementHandler.Instance.Highlight(i,j);
            else break;     

        }

        // downRight
        j=cph.column;
        for(int i = cph.row-1;i>=0;i--){ 
            if(checkValid(i,++j)) ChessBoardPlacementHandler.Instance.Highlight(i,j);
            else break;  

        }

        // downLeft
        j=cph.column;
        for(int i = cph.row-1;i>=0;i--){   
            if(checkValid(i,--j)) ChessBoardPlacementHandler.Instance.Highlight(i,j);
            else break;
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
