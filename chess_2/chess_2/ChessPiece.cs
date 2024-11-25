using System;

namespace chess
{
	
	public interface ChessPiece
	{
		//piece type pawn = 1, knight = 2, bishop = 3, rook = 4, queen = 5, king = 6
		int getPColor();//0 for white 1 for black
		int[] getLocation();
		int getType(); //type of chess piece represented as an integer
		int[][] moves(ChessPiece[,] board, int colorSel);/*determines the way each piece moves
														passing colorSel to know which side of the board pieces started on*/
		void setLocation(int L1, int L2);//sets the location of the piece on the board
		int getID();
	}
}
