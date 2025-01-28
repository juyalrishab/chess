using System;
using UnityEngine;

namespace Chess.Scripts.Core {
    public class ChessPlayerPlacementHandler : MonoBehaviour {
        [SerializeField] public int row, column;
        public bool selected = false; //chek if the peice is selected
        public IMovement peiceType;

        private void Start() {
            ChessBoardPlacementHandler.currentTurn=0;
            peiceType = GetComponent<IMovement>();
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        }
        private void Update() {
            if(InputManager.Clicked) {
                if(!selected){
                    selectPlayer();
                }
                else{
                    tileTransform();
                }      
            }
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
            
        }
        void selectPlayer(){
            selected = false;
            // Using Raycast to get the iformation of the game object 
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(InputManager.ClickedPosistion);
            RaycastHit2D hit= Physics2D.Raycast(mousePos,Vector2.zero);

            if (hit.collider != null){
                GameObject clickedPlayer = hit.collider.gameObject;

                if(ChessBoardPlacementHandler.currentTurn==0){
                    if(this.gameObject == clickedPlayer && this.gameObject.CompareTag("White")){
                    // To check where can piece move
                    this.peiceType.move();

                    selected = true;
                    }
                }
                else {
                    if(this.gameObject == clickedPlayer && this.gameObject.CompareTag("Black")){
                    selected = true;

                    // To check where can piece move
                    peiceType.move();
                    }
                    
                    }
            
                }
            }
        void tileTransform(){
            selected = false;
            
            ChessBoardPlacementHandler.Instance.ClearHighlights();
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(InputManager.ClickedPosistion);
            RaycastHit2D hit= Physics2D.Raycast(mousePos,Vector3.zero);

            if (hit.collider!= null)
            {
                GameObject clickedPlayer = hit.collider.gameObject;

                if(clickedPlayer.layer == LayerMask.NameToLayer("Tiles")&& 
                ChessBoardPlacementHandler.Instance.CheckForChild(clickedPlayer))//if the tile have highlighted prefab only there it can move 
                {
                // setTheTransform
                (row,column)= ChessBoardPlacementHandler.Instance.GetTileIndex(clickedPlayer);

                // setTurn
                ChessBoardPlacementHandler.currentTurn = (ChessBoardPlacementHandler.currentTurn+1)%2;            
                }
                if(clickedPlayer.layer == LayerMask.NameToLayer("Players") && gameObject.tag != clickedPlayer.tag){
                int R=clickedPlayer.GetComponent<ChessPlayerPlacementHandler>().row;
                int C=clickedPlayer.GetComponent<ChessPlayerPlacementHandler>().column;

                // can only move if the tile have Red Highlighted prefab
                if(canAttack(R,C))
                {
                (row,column)= (R,C); 

                ChessBoardPlacementHandler.currentTurn = (ChessBoardPlacementHandler.currentTurn+1)%2; // so player turn can be 0 and 1(white and black)

                clickedPlayer.SetActive(false);
                }
                }

            }
        
        }

        //if the tile have highlight prefab only then it can move  
        public bool canAttack(int i ,int j){
            GameObject target =  ChessBoardPlacementHandler.Instance._chessBoard[i,j];
            return ChessBoardPlacementHandler.Instance.CheckForChild(target);
        }
    }


}