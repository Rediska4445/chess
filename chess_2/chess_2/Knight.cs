using System;

namespace chess
{
	public class Knight: ChessPiece
	{
		private int pColor;
		private const int type = 2;
		private int[] location;
		private int id;
		public Knight(int num, int c)
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
			int[][] locations = new int[8][];
			int i = 0;
			//set of location[0]
			int L1 = location[0]+1;
			int L2 = location[0]+2;
			int L3 = location[0]-1;
			int L4 = location[0]-2;
			
			//set of location[1]
			int L5 = location[1]+1;
			int L6 = location[1]+2;
			int L7 = location[1]-1;
			int L8 = location[1]-2;
			if(L1<=7){//checks if the location will be off the board same for the first set of nested if statements
				if(L6<=7){
					int[] newLoc = {L1 , L6};
					locations[i] = newLoc;
					i++;
				}
				if(L8>=0){
					int[] newLoc = {L1 , L8};
					locations[i] = newLoc;
					i++;
				}
			}
			if(L2<=7){
				if(L5<=7){
					int[] newLoc = {L2 , L5};
					locations[i] = newLoc;
					i++;
				}
				if(L7>=0){
					int[] newLoc = {L2 , L7};
					locations[i] = newLoc;
					i++;
				}
			}
			if(L3>=0){
				if(L6<=7){
					int[] newLoc = {L3 , L6};
					locations[i] = newLoc;
					i++;
				}
				if(L8>=0){
					int[] newLoc = {L3 , L8};
					locations[i] = newLoc;
					i++;
				}
			}
			if(L4>=0){
				if(L5<=7){
					int[] newLoc = {L4 , L5};
					locations[i] = newLoc;
					i++;
				}
				if(L7>=0){
					int[] newLoc = {L4 , L7};
					locations[i] = newLoc;
					i++;
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
