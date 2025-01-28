using System.Collections;
using System.Collections.Generic;
using Chess.Scripts.Core;
using UnityEngine;

public class PawnMovement : MonoBehaviour, IMovement
{
    private ChessPlayerPlacementHandler cph;
  
    void Awake()
    {
        cph = GetComponent<ChessPlayerPlacementHandler>();
    }

    public void move(){
        pawnMove();
    }
    void pawnMove(){
        if(gameObject.CompareTag("Black")){
            pawnUp();  
            pawnUpAttack();
        }
        else{
            pawnDown();
            panDownAttack();
        }
    }
    void pawnUp(){
        int maxStep=cph.row+1; //getting row position of the pawn adding 1 to start highlight the area after next row 
        if(cph.row==1)maxStep++; 
        
         for(int i = cph.row+1;i<=maxStep;i++){   
            if(i>7) return;   
            Vector2 tilePos = ChessBoardPlacementHandler.Instance._chessBoard[i,cph.column].transform.position;
            RaycastHit2D hit = Physics2D.Raycast(tilePos,Vector2.zero);
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Players")){
            return;
            }
            ChessBoardPlacementHandler.Instance.Highlight(i,cph.column);
        }
    }
    void pawnDown(){
        int maxStep=cph.row-1;
        if(cph.row==6)maxStep--;
        for(int i = cph.row-1;i>=maxStep;i--){  
            if(i<0) return;    
            Vector2 tilePos = ChessBoardPlacementHandler.Instance._chessBoard[i,cph.column].transform.position;
            RaycastHit2D hit = Physics2D.Raycast(tilePos,Vector2.zero);
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Players")){
            return;
            }
            ChessBoardPlacementHandler.Instance.Highlight(i,cph.column);
        }

    }
    void pawnUpAttack(){
        if(checkEnemy(cph.row+1,cph.column+1)){
            ChessBoardPlacementHandler.Instance.HighlightRed(cph.row+1,cph.column+1);
        }
        if(checkEnemy(cph.row+1,cph.column-1)){
            ChessBoardPlacementHandler.Instance.HighlightRed(cph.row+1,cph.column-1);
        }
    }
    void panDownAttack(){
        if(checkEnemy(cph.row-1,cph.column+1)){
            ChessBoardPlacementHandler.Instance.HighlightRed(cph.row-1,cph.column+1);
        }
        if(checkEnemy(cph.row-1,cph.column-1)){
            ChessBoardPlacementHandler.Instance.HighlightRed(cph.row-1,cph.column-1);
        }
    }
    bool checkEnemy(int i,int j){
        // we can also declare this function in the instance script
        if(i<0 || i>7 || j<0 || j>7) return false;
        Vector2 tilePos = ChessBoardPlacementHandler.Instance._chessBoard[i,j].transform.position;
            RaycastHit2D hit = Physics2D.Raycast(tilePos,Vector2.zero);
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Players") ){
                if(gameObject.tag != hit.collider.gameObject.tag) return true;
            }
            return false;
    }
}
