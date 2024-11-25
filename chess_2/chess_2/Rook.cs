using System;

namespace chess
{
	public class Rook: ChessPiece
	{
		private int pColor;
		private const int type = 4;
		private int[] location;
		private int id;
		private int firstMove = -1;//set to -1 because it gets updated by set location which is called once at the start of the game for every piece
		//firstMove is used for special move of castling
		public Rook(int num, int c)
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

		public int[][] moves(ChessPiece[,] board, int colorSel){
			int[][] locations = new int[14][];
			int j = 0;
			bool endSide1 = false;
			bool endSide2 = false;
			bool endSide3 = false;
			bool endSide4 = false;
			for(int i=1; i<=7; i++){
				if(location[0]+i<=7){
					if(!endSide1){
						if(board[location[0]+i,location[1]]==null){//is a location on the board that is empty, is valid
							int[] newLoc = {location[0]+i , location[1]};
							locations[j] = newLoc;
							j++;
						}
						else if(board[location[0]+i,location[1]].getPColor()!=pColor){//is an enemy piece on the board, is valid, stop checking this way
							int[] newLoc = {location[0]+i , location[1]};
							locations[j] = newLoc;
							j++;
							endSide1=true;
						}
						else if(board[location[0]+i,location[1]].getPColor()==pColor){//one of your pieces stop checking this way
							int[] newLoc = {location[0]+i , location[1]};
							locations[j] = newLoc;
							j++;
							endSide1=true;
						}
					}
				}
				if(location[0]-i>=0){
					if(!endSide2){
						if(board[location[0]-i,location[1]]==null){//is a location on the board that is empty, is valid, stop checking this way
							int[] newLoc = {location[0]-i , location[1]};
							locations[j] = newLoc;
							j++;
						}
						else if(board[location[0]-i,location[1]].getPColor()!=pColor){//is an enemy piece on the board, is valid
							int[] newLoc = {location[0]-i , location[1]};
							locations[j] = newLoc;
							j++;
							endSide2=true;
						}
						else if(board[location[0]-i,location[1]].getPColor()==pColor){//one of your pieces stop checking this way
							int[] newLoc = {location[0]-i , location[1]};
							locations[j] = newLoc;
							j++;
							endSide2=true;
						}
					}
				}
				if(location[1]+i<=7){
					if(!endSide3){
						if(board[location[0],location[1]+i]==null){//is a location on the board that is empty, is valid
							int[] newLoc = {location[0] , location[1]+i};
							locations[j] = newLoc;
							j++;
						}
						else if(board[location[0],location[1]+i].getPColor()!=pColor){//is an enemy piece on the board, is valid, stop checking this way
							int[] newLoc = {location[0] , location[1]+i};
							locations[j] = newLoc;
							j++;
							endSide3=true;
						}
						else if(board[location[0],location[1]+i].getPColor()==pColor){//one of your pieces stop checking this way
							int[] newLoc = {location[0] , location[1]+i};
							locations[j] = newLoc;
							j++;
							endSide3=true;
						}
					}
				}
				if(location[1]-i>=0){
					if(!endSide4){
						if(board[location[0],location[1]-i]==null){//is a location on the board that is empty, is valid
							int[] newLoc = {location[0] , location[1]-i};
							locations[j] = newLoc;
							j++;
						}
						else if(board[location[0],location[1]-i].getPColor()!=pColor){//is an enemy piece on the board, is valid, stop checking this way
							int[] newLoc = {location[0] , location[1]-i};
							locations[j] = newLoc;
							j++;
							endSide4=true;
						}
						else if(board[location[0],location[1]-i].getPColor()==pColor){//one of your pieces stop checking this way
							int[] newLoc = {location[0] , location[1]-i};
							locations[j] = newLoc;
							j++;
							endSide4=true;
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
	}
}
