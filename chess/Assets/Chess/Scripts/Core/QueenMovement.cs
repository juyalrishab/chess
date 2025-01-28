using System.Collections;
using System.Collections.Generic;
using Chess.Scripts.Core;
using UnityEngine;

public class QueenMovement : MonoBehaviour,IMovement
{
    private ChessPlayerPlacementHandler cph;
    
    // Start is called before the first frame update
    void Awake()
    {
        cph = GetComponent<ChessPlayerPlacementHandler>();
    }
    public void move(){
        rookMove();
        bishopMove();
    }

    void rookMove(){
        // for up /down
        for(int i = cph.row+1;i<8;i++){     
            if(checkValid(i,cph.column)) ChessBoardPlacementHandler.Instance.Highlight(i,cph.column);
            else break;
        }

        for(int i = cph.row-1;i>=0;i--){      
            if(checkValid(i,cph.column)) ChessBoardPlacementHandler.Instance.Highlight(i,cph.column);
            else break;
            
        }

        // left/right
        for(int i = cph.column+1;i<8;i++){      
            if(checkValid(cph.row,i)) ChessBoardPlacementHandler.Instance.Highlight(cph.row,i);
            else break;
        }

        for(int i = cph.column-1;i>=0;i--){
            if(checkValid(cph.row,i)) ChessBoardPlacementHandler.Instance.Highlight(cph.row,i);
            else break;
        }
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
            if(i<0 || i>7 || j<0 || j>7) return false;

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
