using System;

namespace chess
{
	public class King: ChessPiece
	{
		private int pColor;
		private const int type = 6;
		private int[] location;
		private int id;
		private int firstMove = -1;//set to -1 because it gets updated by set location which is called once at the start of the game for every piece
		//firstMove is used for special case of castling move
		public King(int num, int c)
		{
			id = num;
			pColor = c;
		}

		public int getPColor()
		{
			return pColor;
		}

		public int[] getLocation()
		{
			return location;
		}

		public int getType()
		{
			return type;
		}

		public int[][] moves(ChessPiece[,] board, int colorSel){//kingLoc only necessary so that the other pieces know if their move is safe to do
			int[][] locations = new int[8][];
			int j = 0;
			//location[1] is up and down
			if(location[0]+1<=7){//edge check
				if(board[location[0]+1,location[1]]==null){
					int[] newLoc = {location[0]+1,location[1]};
					if(!isCheck(newLoc, board, colorSel)){//can't move into check and can't move passed check when castling
						locations[j] = newLoc;
						j++;
						if(board[7,location[1]]!=null){
							if(board[7,location[1]].getType()==4){
								if(firstMove==0&&((Rook)board[7,location[1]]).getFirstMove()==0){//check if the king and rook have not been moved
									if(board[location[0]+2,location[1]]==null&&board[7,location[1]]!=null){//check for possible castling move
										int[] loc = {location[0]+2,location[1]};
										if(board[7,location[1]].getType()==4){//check if the piece is a rook, color doesn't matter since firstMove takes care of if it is an enemy piece
										   if(!isCheck(loc, board, colorSel)&&!isCheck(location, board, colorSel)){//check if the location will be check and if the king is already in check
												locations[j] = loc;
												j++;
											}
										}
									}
								}
							}
						}
					}
				}
				else if(board[location[0]+1,location[1]].getPColor()!=pColor){//enemy piece still valid as long as its not guarded by another piece
					int[] newLoc = {location[0]+1,location[1]};
					if(!isCheck(newLoc, board, colorSel)){//can't move into check
						locations[j] = newLoc;
						j++;
					}
				}
			}
			if(location[0]-1>=0){//edge check
				if(board[location[0]-1,location[1]]==null){
					int[] newLoc = {location[0]-1,location[1]};
					if(!isCheck(newLoc, board, colorSel)){//can't move into check and can't move passed check when castling
						locations[j] = newLoc;
						j++;
						if(board[0,location[1]]!=null){
							if(board[0,location[1]].getType()==4){
								if(firstMove==0&&((Rook)board[0,location[1]]).getFirstMove()==0){//check if the king and rook have not been moved
									if(board[location[0]-2,location[1]]==null&&board[0,location[1]]!=null){//check for possible castling move
										int[] loc = {location[0]-2,location[1]};
										if(board[0,location[1]].getType()==4){//check if the piece is a rook, color doesn't matter since firstMove takes care of if it is an enemy piece
										   if(!isCheck(loc, board, colorSel)&&!isCheck(location, board, colorSel)){//check if the location will be check and if the king is already in check
												locations[j] = loc;
												j++;
											}
										}
									}
								}
							}
						}
					}
				}
				else if(board[location[0]-1,location[1]].getPColor()!=pColor){//enemy piece still valid as long as its not guarded by another piece
					int[] newLoc = {location[0]-1,location[1]};
					if(!isCheck(newLoc, board, colorSel)){//can't move into check
						locations[j] = newLoc;
						j++;
					}
				}
			}
			if(location[1]+1<=7){//edge check
				if(board[location[0],location[1]+1]==null){
					int[] newLoc = {location[0],location[1]+1};
					if(!isCheck(newLoc, board, colorSel)){//can't move into check
						locations[j] = newLoc;
						j++;
					}
				}
				else if(board[location[0],location[1]+1].getPColor()!=pColor){//enemy piece still valid as long as its not guarded by another piece
					int[] newLoc = {location[0],location[1]+1};
					if(!isCheck(newLoc, board, colorSel)){//can't move into check
						locations[j] = newLoc;
						j++;
					}
				}
				
				if(location[0]-1>=0){
					if(board[location[0]-1,location[1]+1]==null){
						int[] newLoc = {location[0]-1,location[1]+1};
						if(!isCheck(newLoc, board, colorSel)){//can't move into check
							locations[j] = newLoc;
							j++;
						}
					}
					else if(board[location[0]-1,location[1]+1].getPColor()!=pColor){//enemy piece still valid as long as its not guarded by another piece
						int[] newLoc = {location[0]-1,location[1]+1};
						if(!isCheck(newLoc, board, colorSel)){//can't move into check
							locations[j] = newLoc;
							j++;
						}
					}
				}
				
				if(location[0]+1<=7){
					if(board[location[0]+1,location[1]+1]==null){
						int[] newLoc = {location[0]+1,location[1]+1};
						if(!isCheck(newLoc, board, colorSel)){//can't move into check
							locations[j] = newLoc;
							j++;
						}
					}
					else if(board[location[0]+1,location[1]+1].getPColor()!=pColor){//enemy piece still valid as long as its not guarded by another piece
						int[] newLoc = {location[0]+1,location[1]+1};
						if(!isCheck(newLoc, board, colorSel)){//can't move into check
							locations[j] = newLoc;
							j++;
						}
					}
				}
			}
			if(location[1]-1>=0){//edge check
				if(board[location[0],location[1]-1]==null){
					int[] newLoc = {location[0],location[1]-1};
					if(!isCheck(newLoc, board, colorSel)){//can't move into check
						locations[j] = newLoc;
						j++;
					}
				}
				else if(board[location[0],location[1]-1].getPColor()!=pColor){//enemy piece still valid as long as its not guarded by another piece
					int[] newLoc = {location[0],location[1]-1};
					if(!isCheck(newLoc, board, colorSel)){//can't move into check
						locations[j] = newLoc;
						j++;
					}
				}
				
				if(location[0]-1>=0){
					if(board[location[0]-1,location[1]-1]==null){
						int[] newLoc = {location[0]-1,location[1]-1};
						if(!isCheck(newLoc, board, colorSel)){//can't move into check
							locations[j] = newLoc;
							j++;
						}
					}
					else if(board[location[0]-1,location[1]-1].getPColor()!=pColor){//enemy piece still valid as long as its not guarded by another piece
						int[] newLoc = {location[0]-1,location[1]-1};
						if(!isCheck(newLoc, board, colorSel)){//can't move into check
							locations[j] = newLoc;
							j++;
						}
					}
				}
				
				if(location[0]+1<=7){
					if(board[location[0]+1,location[1]-1]==null){
						int[] newLoc = {location[0]+1,location[1]-1};
						if(!isCheck(newLoc, board, colorSel)){//can't move into check
							locations[j] = newLoc;
							j++;
						}
					}
					else if(board[location[0]+1,location[1]-1].getPColor()!=pColor){//enemy piece still valid as long as its not guarded by another piece
						int[] newLoc = {location[0]+1,location[1]-1};
						if(!isCheck(newLoc, board, colorSel)){//can't move into check
							locations[j] = newLoc;
							j++;
						}
					}
				}
			}
			return locations;
		}

		public void setLocation(int L1, int L2)
		{
			int[] L = {L1, L2};
			firstMove++;
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
		
		public bool isCheck(int[] loc, ChessPiece[,] board, int colorSel){//check if a move is going to be check or it if the king is in check, call after every turn
			int[][] pieceMoves;
			int k = 0;
			for(int i=0; i<8; i++){
				for(int j=0; j<8; j++){
					if(board[i,j]!=null){//nested so that we don't call a function on a null piece
						if(board[i,j].getPColor()!=pColor){//enemy piece
							if(board[i,j].getType()==1){//pawn forward move doesn't count as an attack so it won't cause check unless it's a diagonal move
								if(loc[0]==i+1&&loc[1]==j+1&&board[i,j].getPColor()!=colorSel){
									return true;
								}
								if(loc[0]==i-1&&loc[1]==j+1&&board[i,j].getPColor()!=colorSel){
									return true;
								}
								if(loc[0]==i+1&&loc[1]==j-1&&board[i,j].getPColor()==colorSel){
									return true;
								}
								if(loc[0]==i-1&&loc[1]==j-1&&board[i,j].getPColor()==colorSel){
									return true;
								}
							}
							else if(board[i,j].getType()==6){//checking if enemy king is guarding this location
								if(loc[0]==i+1){
									if(loc[1]==j){
										return true;
									}
									if(loc[1]==j+1){
										return true;
									}
									if(loc[1]==j-1){
										return true;
									}
								}
								if(loc[0]==i){
									if(loc[1]==j+1){
										return true;
									}
									if(loc[1]==j-1){
										return true;
									}
								}
								if(loc[0]==i-1){
									if(loc[1]==j){
										return true;
									}
									if(loc[1]==j+1){
										return true;
									}
									if(loc[1]==j-1){
										return true;
									}
								}
							}
							else{
								pieceMoves = board[i,j].moves(board, colorSel);
								k=0;
								while(k<pieceMoves.Length&&pieceMoves[k]!=null){//make sure it stays in bounds and isn't null
									if(pieceMoves[k][0]==loc[0]&&pieceMoves[k][1]==loc[1]){//if moves of the enemy piece contains the location in question only works if that location is empty or enemy piece so need more checking
										return true;
									}
									k++;
								}
							}
						}
					}
				}
			}
			return false;
		}
		
		public bool isMoveSafe(int colorSel, ChessPiece[] pcs, int pNum, int[] moveLoc){//returns whether a move is safe or not
			//if this list is empty it is checkmate
			Board b;
			ChessPiece[] p = new ChessPiece[32];
			for(int i=0; i<pcs.Length; i++){//create a set of new pieces to be passed to the new board for checking
				if(pcs[i].getType()==1){
					p[i] = new Pawn(i,pcs[i].getPColor());
					p[i].setLocation(pcs[i].getLocation()[0], pcs[i].getLocation()[1]);
				}
				if(pcs[i].getType()==2){
					p[i] = new Knight(i,pcs[i].getPColor());
					p[i].setLocation(pcs[i].getLocation()[0], pcs[i].getLocation()[1]);
				}
				if(pcs[i].getType()==3){
					p[i] = new Bishop(i,pcs[i].getPColor());
					p[i].setLocation(pcs[i].getLocation()[0], pcs[i].getLocation()[1]);
				}
				if(pcs[i].getType()==4){
					p[i] = new Rook(i,pcs[i].getPColor());
					p[i].setLocation(pcs[i].getLocation()[0], pcs[i].getLocation()[1]);
				}
				if(pcs[i].getType()==5){
					p[i] = new Queen(i,pcs[i].getPColor());
					p[i].setLocation(pcs[i].getLocation()[0], pcs[i].getLocation()[1]);
				}
				if(pcs[i].getType()==6){
					p[i] = new King(i,pcs[i].getPColor());
					p[i].setLocation(pcs[i].getLocation()[0], pcs[i].getLocation()[1]);
				}
				if(p[i].getLocation()[0]==moveLoc[0]&&p[i].getLocation()[1]==moveLoc[1]&&p[i].getType()!=6){//no piece can hold the same location
					p[i].setLocation(-1, -1);
				}
			}
			p[pNum].setLocation(moveLoc[0], moveLoc[1]);
			b = new Board(colorSel, p);//create a new board with the new move location to check if it puts the king in check
			int[] kingLoc = b.findKing(pcs[pNum].getPColor());//the kings location in the new board
			if(((King)b.getBoard()[kingLoc[0],kingLoc[1]]).isCheck(kingLoc,b.getBoard(),colorSel)){//if the move is check return false
				return false;
			}
			else{//if the move is safe return true
				return true;
			}
		}
		
		
		public bool isCheckMate(int colorSel, ChessPiece[] pcs, ChessPiece[,] board, int c){//can be used for both checkmate and stalemate
			int[][] pMoves;//the moves for the piece being checked
			for(int i=0; i<pcs.Length; i++){
				if(pcs[i].getPColor()==c){
					if(pcs[i].getLocation()[0]!=-1){
						pMoves = pcs[i].moves(board, colorSel);
						for(int j=0; j<pMoves.Length; j++){
							if(pMoves[j]!=null){
								if(isMoveSafe(colorSel, pcs, i, pMoves[j])){//if there is a safe move to be made it returns false
									return false;
								}
							}
						}
					}
				}
			}
			return true;//return true if no safe moves to be made
		}
		
	}
}
