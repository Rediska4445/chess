using System;

namespace chess
{
	public class Pawn: ChessPiece
	{
		private int pColor;
		private const int type = 1;
		private int[] location;
		private int id;
		private int firstMove = -1;//set to -1 because it gets updated by set location which is called once at the start of the game for every piece
		//firstmove used for special pawn attack that allows for taking pawns that moved twice on there first move
		public Pawn(int num, int c)
		{
			id = num;
			pColor = c;
		}
		
		public int getPColor(){
			return pColor;
		}
		
		public int[] getLocation(){
			return location;
		}
		
		public int getType(){
			return type;
		}
		
		public int[][] moves(ChessPiece[,] board, int colorSel){
			int[][] locations = new int[4][];
			int i = 0;
			if(pColor==0&&colorSel==0||pColor==1&&colorSel==1){//piece started on the bottom of the screen board
				if(board[location[0], location[1]-1]==null){
					int[] newLoc = {location[0] , location[1]-1};
					locations[i] = newLoc;
					i++;
					if(firstMove==0){//first turn gets to move up 2 if possible
						if(board[location[0], location[1]-2]==null){
							int[] newLoc1 = {location[0] , location[1]-2};
							locations[i] = newLoc1;
							i++;
						}
					}
				}
				if(location[0]!=7){
					if(board[location[0]+1, location[1]-1]!=null){
						if(board[location[0]+1, location[1]-1].getPColor()!=pColor){
							int[] newLoc = {location[0]+1 , location[1]-1};
							locations[i] = newLoc;
							i++;
						}
					}
					if(board[location[0]+1, location[1]-1]==null){//diagonal is null check for an enemy pawn first turn double move
						if(board[location[0]+1,location[1]]!=null){//make sure the piece next to this one isn't null
							if(board[location[0]+1,location[1]].getPColor()!=pColor&&board[location[0]+1,location[1]].getType()==1){//check if the piece next to this one is a pawn
								if(((Pawn)board[location[0]+1,location[1]]).getFirstMove()==1){
									int[] newLoc = {location[0]+1 , location[1]-1};
									locations[i] = newLoc;
									i++;
								}
							}
						}
					}
				}
				if(location[0]!=0){
					if(board[location[0]-1, location[1]-1]!=null){
						if(board[location[0]-1, location[1]-1].getPColor()!=pColor){
							int[] newLoc = {location[0]-1 , location[1]-1};
							locations[i] = newLoc;
							i++;
						}
					}
					if(board[location[0]-1, location[1]-1]==null){//diagonal is null check for an enemy pawn first turn double move
						if(board[location[0]-1,location[1]]!=null){//make sure the piece next to this one isn't null
							if(board[location[0]-1,location[1]].getPColor()!=pColor&&board[location[0]-1,location[1]].getType()==1){//check if the piece next to this one is a pawn
								if(((Pawn)board[location[0]-1,location[1]]).getFirstMove()==1){
									int[] newLoc = {location[0]-1 , location[1]-1};
									locations[i] = newLoc;
									i++;
								}
							}
						}
					}
				}
			}
			else{//piece started on the top of the screen board
				if(board[location[0], location[1]+1]==null){//checks for pieces in front of it
					int[] newLoc = {location[0] , location[1]+1};
					locations[i] = newLoc;
					i++;
					if(firstMove==0){//first turn gets to move up 2 if possible
						if(board[location[0], location[1]+2]==null){
							int[] newLoc1 = {location[0] , location[1]+2};
							locations[i] = newLoc1;
							i++;
						}
					}
				}
				if(location[0]!=7){//make sure the pawn isnt along the edge to avoid out of bounds
					if(board[location[0]+1, location[1]+1]!=null){//checks for pieces to its diagonal to attack
						if(board[location[0]+1, location[1]+1].getPColor()!=pColor){//makes sure the piece is an enemy
							int[] newLoc = {location[0]+1 , location[1]+1};
							locations[i] = newLoc;
							i++;
						}
					}
					if(board[location[0]+1, location[1]+1]==null){//diagonal is null check for an enemy pawn first turn double move
						if(board[location[0]+1,location[1]]!=null){//make sure the piece next to this one isn't null
							if(board[location[0]+1,location[1]].getPColor()!=pColor&&board[location[0]+1,location[1]].getType()==1){//check if the piece next to this one is a pawn
								if(((Pawn)board[location[0]+1,location[1]]).getFirstMove()==1){
									int[] newLoc = {location[0]+1 , location[1]+1};
									locations[i] = newLoc;
									i++;
								}
							}
						}
					}
				}
				if(location[0]!=0){//make sure the pawn isnt along the edge to avoid out of bounds
					if(board[location[0]-1, location[1]+1]!=null){//checks for pieces to its diagonal to attack
						if(board[location[0]-1, location[1]+1].getPColor()!=pColor){//makes sure the piece is an enemy
							int[] newLoc = {location[0]-1 , location[1]+1};
							locations[i] = newLoc;
							i++;
						}
					}
					if(board[location[0]-1, location[1]+1]==null){//diagonal is null check for an enemy pawn first turn double move
						if(board[location[0]-1,location[1]]!=null){//make sure the piece next to this one isn't null
							if(board[location[0]-1,location[1]].getPColor()!=pColor&&board[location[0]-1,location[1]].getType()==1){//check if the piece next to this one is a pawn
								if(((Pawn)board[location[0]-1,location[1]]).getFirstMove()==1){
									int[] newLoc = {location[0]-1 , location[1]+1};
									locations[i] = newLoc;
									i++;
								}
							}
						}
					}
				}
			}
			return locations;
		}
		
		public void setLocation(int L1, int L2){
			firstMove+=1;
			int[] L = {L1, L2};
			location = L;
		}
		
		public int getID(){
			return id;
		}
		
		public int getFirstMove(){
			return firstMove;
		}
		
		public void setFirstMove(int f){//for saving and loading purposes
			firstMove = f;
		}
		
		public void changePieceType(int pType, ChessPiece[] pieces){
			if(pType==2){
				pieces[id] = new Knight(id, pColor);
			}
			if(pType==3){
				pieces[id] = new Bishop(id, pColor);
			}
			if(pType==4){
				pieces[id] = new Rook(id, pColor);
			}
			if(pType==5){
				pieces[id] = new Queen(id, pColor);
			}
		}
	}
}
