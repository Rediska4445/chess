using System;

namespace chess
{
	public class Bishop: ChessPiece
	{
		private int pColor;
		private const int type = 3;
		private int[] location;
		private int id;
		public Bishop(int num, int c)
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
			int[][] locations = new int[13][];
			bool endAngle1 = false;
			bool endAngle2 = false;
			bool endAngle3 = false;
			bool endAngle4 = false;
			int j=0;
			for(int i=1; i<=7; i++){
				if(location[0]+i<=7){
					if(location[1]+i<=7){
						if(!endAngle1){
							if(board[location[0]+i,location[1]+i]==null){//is a location on the board that is empty, is valid
								int[] newLoc = {location[0]+i , location[1]+i};
								locations[j] = newLoc;
								j++;
								
							}
							else if(board[location[0]+i,location[1]+i].getPColor()!=pColor){//is an enemy piece on the board, is valid
								int[] newLoc = {location[0]+i , location[1]+i};
								locations[j] = newLoc;
								j++;
								endAngle1=true;
							}
							else if(board[location[0]+i,location[1]+i].getPColor()==pColor){//if you hit your own piece no need to check that direction anymore
								int[] newLoc = {location[0]+i , location[1]+i};
								locations[j] = newLoc;
								j++;
								endAngle1=true;
							}
						}
					}
					if(location[1]-i>=0){
						if(!endAngle2){
							if(board[location[0]+i,location[1]-i]==null){//is a location on the board that is empty, is valid
								int[] newLoc = {location[0]+i , location[1]-i};
								locations[j] = newLoc;
								j++;
							}
							else if(board[location[0]+i,location[1]-i].getPColor()!=pColor){//is an enemy piece on the board, is valid
								int[] newLoc = {location[0]+i , location[1]-i};
								locations[j] = newLoc;
								j++;
								endAngle2=true;
							}
							else if(board[location[0]+i,location[1]-i].getPColor()==pColor){//if you hit your own piece no need to check that direction anymore
								int[] newLoc = {location[0]+i , location[1]-i};
								locations[j] = newLoc;
								j++;
								endAngle2=true;
							}
						}
					}
				}
				if(location[0]-i>=0){
					if(location[1]+i<=7){
						if(!endAngle3){
							if(board[location[0]-i,location[1]+i]==null){//is a location on the board that is empty, is valid
								int[] newLoc = {location[0]-i , location[1]+i};
								locations[j] = newLoc;
								j++;
							}
							else if(board[location[0]-i,location[1]+i].getPColor()!=pColor){//is an enemy piece on the board, is valid
								int[] newLoc = {location[0]-i , location[1]+i};
								locations[j] = newLoc;
								j++;
								endAngle3=true;
							}
							else if(board[location[0]-i,location[1]+i].getPColor()==pColor){//if you hit your own piece no need to check that direction anymore
								int[] newLoc = {location[0]-i , location[1]+i};
								locations[j] = newLoc;
								j++;
								endAngle3=true;
							}
						}
					}
					if(location[1]-i>=0){
						if(!endAngle4){
							if(board[location[0]-i,location[1]-i]==null){//is a location on the board that is empty, is valid
								int[] newLoc = {location[0]-i , location[1]-i};
								locations[j] = newLoc;
								j++;
							}
							else if(board[location[0]-i,location[1]-i].getPColor()!=pColor){//is an enemy piece on the board, is valid
								int[] newLoc = {location[0]-i , location[1]-i};
								locations[j] = newLoc;
								j++;
								endAngle4=true;
							}
							else if(board[location[0]-i,location[1]-i].getPColor()==pColor){//if you hit your own piece no need to check that direction anymore
								int[] newLoc = {location[0]-i , location[1]-i};
								locations[j] = newLoc;
								j++;
								endAngle4=true;
							}
						}
					}
				}
			}
			return locations;
		}

		public void setLocation(int L1, int L2){
			int[] L = {L1, L2};
			location = L;
		}
		
		public int getID(){
			return id;
		}

	}
}
