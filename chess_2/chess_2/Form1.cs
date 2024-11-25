/*
 * User: Kyle Fadley
 * Date: 7/10/2019
 * Time: 10:56 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using chess;
using System.Reflection;
using chess_2.Properties;

namespace chess_2
{
    public partial class Form1 : Form
    {
        private Button[,] boardButtons = new Button[8, 8];
        private Board game;
        private int turn;//turn starts with white
        private int colorSel;
        private int pawnSel = 0;
        private ChessPiece selectedPiece = null;//the most recently clicked piece
        private int[][] moves = null;//a list of the moves that the most recently clicked piece can make
        public Form1()
        {
            InitializeComponent();
            this.Height = 500;
            this.Width = 500;
            mainMenu();
        }

        private void initializeGame()
        {//call to set up the gameboard
            ChessPiece[,] gameBoard = game.getBoard();
            for (int i = 0; i < 8; i++)
            {//maps the gameboard to buttons for the ui
                for (int j = 0; j < 8; j++)
                {
                    boardButtons[i, j] = new Button();
                    boardButtons[i, j].Location = new Point(i * 100, j * 100);
                    boardButtons[i, j].Size = new Size(100, 100);
                    boardButtons[i, j].Paint += button_Paint;
                    boardButtons[i, j].Enabled = false;
                    if (gameBoard[i, j] != null)
                    {
                        if (gameBoard[i, j].getPColor() == 0)
                        {
                            if (gameBoard[i, j].getType() == 1)
                            {
                                boardButtons[i, j].Image = Resources.pawn__1_;
                                boardButtons[i, j].ForeColor = Color.White;
                                if (turn == 0)
                                {
                                    boardButtons[i, j].Enabled = true;
                                }
                            }
                            if (gameBoard[i, j].getType() == 2)
                            {
                                boardButtons[i, j].Image = Resources.horse__1_;
                                boardButtons[i, j].ForeColor = Color.White;
                                if (turn == 0)
                                {
                                    boardButtons[i, j].Enabled = true;
                                }
                            }
                            if (gameBoard[i, j].getType() == 3)
                            {
                                boardButtons[i, j].Image = Resources.elephant__1_;
                                boardButtons[i, j].ForeColor = Color.White;
                                if (turn == 0)
                                {
                                    boardButtons[i, j].Enabled = true;
                                }
                            }
                            if (gameBoard[i, j].getType() == 4)
                            {
                                boardButtons[i, j].Image = Resources.rook__1_;
                                boardButtons[i, j].ForeColor = Color.White;
                                if (turn == 0)
                                {
                                    boardButtons[i, j].Enabled = true;
                                }
                            }
                            if (gameBoard[i, j].getType() == 5)
                            {
                                boardButtons[i, j].Image = Resources.queen__1_;
                                boardButtons[i, j].ForeColor = Color.White;
                                if (turn == 0)
                                {
                                    boardButtons[i, j].Enabled = true;
                                }
                            }
                            if (gameBoard[i, j].getType() == 6)
                            {
                                boardButtons[i, j].Image = Resources.king__1_;
                                boardButtons[i, j].ForeColor = Color.White;
                                if (turn == 0)
                                {
                                    boardButtons[i, j].Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            if (gameBoard[i, j].getType() == 1)
                            {
                                boardButtons[i, j].Image = Resources.pawn;
                                if (turn == 1)
                                {
                                    boardButtons[i, j].Enabled = true;
                                }
                            }
                            if (gameBoard[i, j].getType() == 2)
                            {
                                boardButtons[i, j].Image = Resources.horse;
                                if (turn == 1)
                                {
                                    boardButtons[i, j].Enabled = true;
                                }
                            }
                            if (gameBoard[i, j].getType() == 3)
                            {
                                boardButtons[i, j].Image = Resources.elephant;
                                if (turn == 1)
                                {
                                    boardButtons[i, j].Enabled = true;
                                }
                            }
                            if (gameBoard[i, j].getType() == 4)
                            {
                                boardButtons[i, j].Image = Resources.rook;
                                if (turn == 1)
                                {
                                    boardButtons[i, j].Enabled = true;
                                }
                            }
                            if (gameBoard[i, j].getType() == 5)
                            {
                                boardButtons[i, j].Image = Resources.queen;
                                if (turn == 1)
                                {
                                    boardButtons[i, j].Enabled = true;
                                }
                            }
                            if (gameBoard[i, j].getType() == 6)
                            {
                                boardButtons[i, j].Image = Resources.king;
                                if (turn == 1)
                                {
                                    boardButtons[i, j].Enabled = true;
                                }
                            }
                        }
                    }
                    if ((i + j) % 2 == 0)
                    {
                        boardButtons[i, j].BackColor = Color.Gray;
                    }
                    else
                    {
                        boardButtons[i, j].BackColor = Color.DarkGray;
                    }
                    boardButtons[i, j].Click += button_Click;
                    Controls.Add(boardButtons[i, j]);
                }
            }
            Button saveBtn = new Button();
            saveBtn.Click += saveButton_Click;
            saveBtn.Text = "Save Game";
            saveBtn.Location = new Point(350, 420);
            Controls.Add(saveBtn);
            checkGameState();
        }

        private void changeTurn()
        {//handles all the turn change necessities

            /*call moves of pieces whose turn it is and null out any moves that are of a piece that is the same color
			check for moves that are not null if there are any enable the button for that piece otherwise don't
			find a way to only enable moves that get you out of check when you are in check*/

            turn++;
            turn = turn % 2;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boardButtons[i, j].Enabled = false;//disable all buttons to reset for next person's turn
                    if (game.getBoard()[i, j] != null)
                    {//can't call a function on a null object
                        if (game.getBoard()[i, j].getPColor() == turn)
                        {//enable any buttons that have a current turn
                            boardButtons[i, j].Enabled = true;
                        }
                    }
                }
            }
            checkGameState();
        }

        private int pieceSelect()
        {//piece selection menu for the pawn reaching end of board
            var popupForm = new Form();
            popupForm.ControlBox = false;//you must select a piece to change to

            Label label = new Label();
            label.Text = "Select a Piece";
            label.Location = new Point(20, 0);
            RadioButton kn = new RadioButton();
            kn.Text = "конь";
            kn.CheckedChanged += radioButton_checkedChanged;
            kn.Location = new Point(10, 20);

            RadioButton bi = new RadioButton();
            bi.Text = "офицер";
            bi.CheckedChanged += radioButton_checkedChanged;
            bi.Location = new Point(10, 40);

            RadioButton ro = new RadioButton();
            ro.Text = "ладья";
            ro.CheckedChanged += radioButton_checkedChanged;
            ro.Location = new Point(10, 60);

            RadioButton qu = new RadioButton();
            qu.Text = "королева";
            qu.CheckedChanged += radioButton_checkedChanged;
            qu.Location = new Point(10, 80);

            popupForm.Controls.Add(label);
            popupForm.Controls.Add(kn);
            popupForm.Controls.Add(bi);
            popupForm.Controls.Add(ro);
            popupForm.Controls.Add(qu);
            popupForm.ShowDialog();
            int selection = pawnSel;
            pawnSel = 0;
            return selection;
        }


        private void checkGameState()
        {//checks if the game is over via checkmate or stalemate
            int[] kingLoc = game.findKing(turn);
            if (((King)game.getBoard()[kingLoc[0], kingLoc[1]]).isCheck(kingLoc, game.getBoard(), colorSel))
            {
                if (((King)game.getBoard()[kingLoc[0], kingLoc[1]]).isCheckMate(colorSel, game.getPieces(), game.getBoard(), turn))
                {//king is in check and no moves can be made "checkmate"
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            boardButtons[i, j].Enabled = false;//game is over disable all board buttons
                        }
                    }
                    Label gameOverLabel = new Label();
                    gameOverLabel.Size = new System.Drawing.Size(250, 13);
                    gameOverLabel.Text = "шах и мат";
                    if (turn == 0)
                    {//if it was whites turn next and white is checkmated black wins
                        gameOverLabel.Text += "чёрные победили";
                    }
                    else
                    {//the opposite is true so white wins
                        gameOverLabel.Text += "белые победили";
                    }
                    gameOverLabel.Location = new Point(150, 420);
                    Controls.Add(gameOverLabel);//display that the one side has been checkmated and tell who the winner is
                }
            }
            else
            {
                if (((King)game.getBoard()[kingLoc[0], kingLoc[1]]).isCheckMate(colorSel, game.getPieces(), game.getBoard(), turn))
                {//king is not in check but no moves can be made "stalemate"
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            boardButtons[i, j].Enabled = false;//game is over disable all board buttons
                        }
                    }
                    Label gameOverLabel = new Label();
                    gameOverLabel.Size = new System.Drawing.Size(250, 13);
                    gameOverLabel.Text = "мат";
                    gameOverLabel.Location = new Point(150, 420);
                    Controls.Add(gameOverLabel);//display that the game is a stalemate
                }
            }
        }

        private void radioButton_checkedChanged(object sender, System.EventArgs e)
        {
            if (((RadioButton)sender).Text == "конь")
            {
                pawnSel = 2;
                ((Form)((RadioButton)sender).GetContainerControl()).Close();
            }
            if (((RadioButton)sender).Text == "офицер")
            {
                pawnSel = 3;
                ((Form)((RadioButton)sender).GetContainerControl()).Close();
            }
            if (((RadioButton)sender).Text == "ладья")
            {
                pawnSel = 4;
                ((Form)((RadioButton)sender).GetContainerControl()).Close();
            }
            if (((RadioButton)sender).Text == "королева")
            {
                pawnSel = 5;
                ((Form)((RadioButton)sender).GetContainerControl()).Close();
            }
        }

        private void button_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Button btn = (Button)sender;
            string buttonText = "";
            dynamic drawBrush = new SolidBrush(btn.ForeColor);
            dynamic sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            buttonText = btn.Text;
            e.Graphics.DrawString(buttonText, btn.Font, drawBrush, e.ClipRectangle, sf);
            drawBrush.Dispose();
            sf.Dispose();

        }

        private void button_Click(object sender,
        System.EventArgs e)
        {
            for (int i1 = 0; i1 < 8; i1++)
            {
                for (int j1 = 0; j1 < 8; j1++)
                {
                    if ((i1 + j1) % 2 == 0)
                    {
                        boardButtons[i1, j1].BackColor = Color.Gray;
                    }
                    else
                    {
                        boardButtons[i1, j1].BackColor = Color.DarkGray;
                    }
                }
            }

            int i = -1;
            int j = -1;
            int z = 0;
            //handles finding the current button location numbers
            Button button = (Button)sender;
            Color saveColor = button.BackColor;
            while (i < 7)
            {//finding which button sent to know which piece it corresponds to on the board
                i++;
                j = 0;
                while (j < 8)
                {
                    if (button.GetHashCode() == boardButtons[i, j].GetHashCode())
                    {//breaks out of the loop once the button is found
                        break;
                    }
                    j++;
                }
                if (j == 8)
                {//to save from out of bounds issues while searching
                    j = 7;
                }
                if (button.GetHashCode() == boardButtons[i, j].GetHashCode())
                {//breaks out of the loop once the button is found
                    break;
                }
            }

            //handles the movement of pieces on the board
            if (game.getBoard()[i, j] != null)
            {//a piece is selected on the board and not a blank space
                ChessPiece curPiece = game.getBoard()[i, j];//the piece that was clicked
                z = 0;
                if (moves == null || selectedPiece == null)
                {//handles a weird case where selectedPiece somehow ends up null 
                 //and moves isn't null and handles first piece clicked for the persons turn
                    moves = null;//sets to null in weird case that moves isn't null and selectedPiece is null
                    moves = curPiece.moves(game.getBoard(), colorSel);
                    while (z < moves.Length && moves[z] != null)
                    {//once the possible moves are found set those locations to be clickable
                        if (game.getBoard()[moves[z][0], moves[z][1]] == null)
                        {
                            if (((King)game.getBoard()[game.findKing(curPiece.getPColor())[0], game.findKing(curPiece.getPColor())[1]]).isMoveSafe(colorSel, game.getPieces(), curPiece.getID(), moves[z]))
                            {
                                boardButtons[moves[z][0], moves[z][1]].Enabled = true;
                                saveColor = boardButtons[moves[z][0], moves[z][1]].BackColor;
                                boardButtons[moves[z][0], moves[z][1]].BackColor = Color.Gold;
                            }
                            selectedPiece = curPiece;
                            z++;
                        }
                        else if (game.getBoard()[moves[z][0], moves[z][1]].getPColor() != curPiece.getPColor())
                        {
                            if (((King)game.getBoard()[game.findKing(curPiece.getPColor())[0], game.findKing(curPiece.getPColor())[1]]).isMoveSafe(colorSel, game.getPieces(), curPiece.getID(), moves[z]))
                            {
                                boardButtons[moves[z][0], moves[z][1]].Enabled = true;
                                saveColor = boardButtons[moves[z][0], moves[z][1]].BackColor;
                                boardButtons[moves[z][0], moves[z][1]].BackColor = Color.Gold;
                            }
                            selectedPiece = curPiece;
                            z++;
                        }
                        else
                        {//null out any moves that are of the same color of the piece being moved, those are in the moves list for the isCheck function
                            moves[z] = null;
                            z++;
                        }
                    }
                }
                else if (selectedPiece.getPColor() == curPiece.getPColor())
                {//handles if the next click is another one of the same persons turn
                    while (z < moves.Length)
                    {//set old moves list to unclickable
                        if (moves[z] != null)
                        {
                            boardButtons[moves[z][0], moves[z][1]].Enabled = false;
                        }
                        z++;
                    }
                    moves = null;
                    z = 0;
                    moves = curPiece.moves(game.getBoard(), colorSel);
                    while (z < moves.Length && moves[z] != null)
                    {//once the possible moves are found set those locations to be clickable
                        if (game.getBoard()[moves[z][0], moves[z][1]] == null)
                        {
                            if (((King)game.getBoard()[game.findKing(curPiece.getPColor())[0], game.findKing(curPiece.getPColor())[1]]).isMoveSafe(colorSel, game.getPieces(), curPiece.getID(), moves[z]))
                            {
                                boardButtons[moves[z][0], moves[z][1]].Enabled = true;
                                boardButtons[moves[z][0], moves[z][1]].BackColor = Color.Gold;
                            }
                            selectedPiece = curPiece;
                            z++;
                        }
                        else if (game.getBoard()[moves[z][0], moves[z][1]].getPColor() != curPiece.getPColor())
                        {//only allow movement to locations that aren't your own pieces
                            if (((King)game.getBoard()[game.findKing(curPiece.getPColor())[0], game.findKing(curPiece.getPColor())[1]]).isMoveSafe(colorSel, game.getPieces(), curPiece.getID(), moves[z]))
                            {
                                boardButtons[moves[z][0], moves[z][1]].Enabled = true;
                                boardButtons[moves[z][0], moves[z][1]].BackColor = Color.Gold;
                            }
                            selectedPiece = curPiece;
                            z++;
                        }
                        else
                        {//null out any moves that are of the same color of the piece being moved, those are in the moves list for the isCheck function
                            moves[z] = null;
                            z++;
                        }
                    }
                }
                else
                {//enemy piece was selected after you selected a piece to move
                    z = 0;
                    while (z < moves.Length)
                    {//set old moves list to unclickable since your piece is moving
                        if (moves[z] != null)
                        {
                            boardButtons[moves[z][0], moves[z][1]].Enabled = false;
                        }
                        z++;
                    }
                    //set new location of selected piece to correct display
                    if (selectedPiece.getType() == 1)
                    {
                        if (selectedPiece.getPColor() == 0)
                        {
                            boardButtons[i, j].Image = Resources.pawn__1_;
                            boardButtons[i, j].ForeColor = Color.White;
                        }
                        else
                        {
                            boardButtons[i, j].Image = Resources.pawn;
                            boardButtons[i, j].ForeColor = Color.Black;
                        }
                        boardButtons[i, j].Enabled = true;
                        if (j == 0 || j == 7)
                        {//set to new piece type since you reached the end of the board with a pawn
                            int k = selectedPiece.getID();
                            int m = selectedPiece.getLocation()[0];
                            int n = selectedPiece.getLocation()[1];
                            int selection = pieceSelect();//select new piece
                            ((Pawn)selectedPiece).changePieceType(selection, game.getPieces());
                            game.getBoard()[m, n] = game.getPieces()[k];
                            selectedPiece = game.getPieces()[k];
                            selectedPiece.setLocation(m, n);
                        }
                    }
                    if (selectedPiece.getType() == 2)
                    {
                        if (selectedPiece.getPColor() == 0)
                        {
                            boardButtons[i, j].Image = Resources.horse__1_;
                            boardButtons[i, j].ForeColor = Color.White;
                        }
                        else
                        {
                            boardButtons[i, j].Image = Resources.horse;
                            boardButtons[i, j].ForeColor = Color.Black;
                        }
                        boardButtons[i, j].Enabled = true;
                    }
                    if (selectedPiece.getType() == 3)
                    {
                        if (selectedPiece.getPColor() == 0)
                        {
                            boardButtons[i, j].Image = Resources.elephant__1_;
                            boardButtons[i, j].ForeColor = Color.White;
                        }
                        else
                        {
                            boardButtons[i, j].Image = Resources.elephant;
                            boardButtons[i, j].ForeColor = Color.Black;
                        }
                        boardButtons[i, j].Enabled = true;
                    }
                    if (selectedPiece.getType() == 4)
                    {
                        if (selectedPiece.getPColor() == 0)
                        {
                            boardButtons[i, j].Image = Resources.rook__1_;
                            boardButtons[i, j].ForeColor = Color.White;
                        }
                        else
                        {
                            boardButtons[i, j].Image = Resources.rook;
                            boardButtons[i, j].ForeColor = Color.Black;
                        }
                        boardButtons[i, j].Enabled = true;
                    }
                    if (selectedPiece.getType() == 5)
                    {
                        if (selectedPiece.getPColor() == 0)
                        {
                            boardButtons[i, j].Image = Resources.queen__1_;
                            boardButtons[i, j].ForeColor = Color.White;
                        }
                        else
                        {
                            boardButtons[i, j].Image = Resources.queen;
                            boardButtons[i, j].ForeColor = Color.Black;
                        }
                        boardButtons[i, j].Enabled = true;
                    }
                    if (selectedPiece.getType() == 6)
                    {
                        if (selectedPiece.getPColor() == 0)
                        {
                            boardButtons[i, j].Image = Resources.king__1_;
                            boardButtons[i, j].ForeColor = Color.White;
                        }
                        else
                        {
                            boardButtons[i, j].Image = Resources.king;
                            boardButtons[i, j].ForeColor = Color.Black;
                        }
                        boardButtons[i, j].Enabled = true;
                    }

                    //set all necessary things to unclickable and null and the piece to its new location
                    boardButtons[selectedPiece.getLocation()[0], selectedPiece.getLocation()[1]].Enabled = false;
                    boardButtons[selectedPiece.getLocation()[0], selectedPiece.getLocation()[1]].Text = "";
                    boardButtons[selectedPiece.getLocation()[0], selectedPiece.getLocation()[1]].Image = null;
                    moves = null;
                    game.getBoard()[i, j] = selectedPiece;
                    game.getBoard()[selectedPiece.getLocation()[0], selectedPiece.getLocation()[1]] = null;
                    selectedPiece.setLocation(i, j);
                    selectedPiece = null;
                    for (int r = 0; r < game.getPieces().Length; r++)
                    {
                        if (game.getPieces()[r].getLocation()[0] == -1)
                        {//this piece isn't on the board don't bother checking it

                        }
                        else if (game.getBoard()[game.getPieces()[r].getLocation()[0], game.getPieces()[r].getLocation()[1]] == null)
                        {
                            game.getPieces()[r].setLocation(-1, -1);//the piece is no longer on the board, set its location to off the board
                        }
                        else if (game.getBoard()[game.getPieces()[r].getLocation()[0], game.getPieces()[r].getLocation()[1]].getID() != game.getPieces()[r].getID())
                        {//check if the piece is no longer on the board
                            game.getPieces()[r].setLocation(-1, -1);//the piece is no longer on the board, set its location to off the board
                        }
                        //this is for the save and load features
                    }
                    changeTurn();
                }
            }
            else
            {//a blank space is selected on the board 'only occurs if there is a piece currently selected for movement'
                z = 0;
                while (z < moves.Length && moves[z] != null)
                {//set old moves list to unclickable since your piece is moving
                    boardButtons[moves[z][0], moves[z][1]].Enabled = false;
                    z++;
                }

                //set new location of selected piece to correct display mostly for color of piece and handles some special case moves
                if (selectedPiece.getType() == 1)
                {
                    if (selectedPiece.getLocation()[0] != i && game.getBoard()[i, j] == null)
                    {//if the pawn is making a diagonal move into an empty space this is a special move taking an enemy pawn off its first turn move
                     //set the enemy pawns location to null and empty
                        game.getBoard()[i, selectedPiece.getLocation()[1]] = null;
                        boardButtons[i, selectedPiece.getLocation()[1]].Text = "";
                        boardButtons[i, selectedPiece.getLocation()[1]].Image = null;
                    }
                    if (selectedPiece.getPColor() == 0)
                    {
                        boardButtons[i, j].Image = Resources.pawn__1_;
                        boardButtons[i, j].ForeColor = Color.White;
                    }
                    else
                    {
                        boardButtons[i, j].Image = Resources.pawn;
                        boardButtons[i, j].ForeColor = Color.Black;
                    }
                    boardButtons[i, j].Enabled = true;
                    if (j == 0 || j == 7)
                    {//set to new piece type since you reached the end of the board with a pawn
                        int k = selectedPiece.getID();
                        int m = selectedPiece.getLocation()[0];
                        int n = selectedPiece.getLocation()[1];
                        int selection = pieceSelect();
                        ((Pawn)selectedPiece).changePieceType(selection, game.getPieces());
                        game.getBoard()[m, n] = game.getPieces()[k];
                        selectedPiece = game.getPieces()[k];
                        selectedPiece.setLocation(m, n);
                    }
                }
                if (selectedPiece.getType() == 2)
                {
                    if (selectedPiece.getPColor() == 0)
                    {
                        boardButtons[i, j].Image = Resources.horse__1_;
                        boardButtons[i, j].ForeColor = Color.White;
                    }
                    else
                    {
                        boardButtons[i, j].Image = Resources.horse;
                        boardButtons[i, j].ForeColor = Color.Black;
                    }
                    boardButtons[i, j].Enabled = true;
                }
                if (selectedPiece.getType() == 3)
                {
                    if (selectedPiece.getPColor() == 0)
                    {
                        boardButtons[i, j].Image = Resources.elephant__1_;
                        boardButtons[i, j].ForeColor = Color.White;
                    }
                    else
                    {
                        boardButtons[i, j].Image = Resources.elephant;
                        boardButtons[i, j].ForeColor = Color.Black;
                    }
                    boardButtons[i, j].Enabled = true;
                }
                if (selectedPiece.getType() == 4)
                {
                    if (selectedPiece.getPColor() == 0)
                    {
                        boardButtons[i, j].Image = Resources.rook__1_;
                        boardButtons[i, j].ForeColor = Color.White;
                    }
                    else
                    {
                        boardButtons[i, j].Image = Resources.rook;
                        boardButtons[i, j].ForeColor = Color.Black;
                    }
                    boardButtons[i, j].Enabled = true;
                }
                if (selectedPiece.getType() == 5)
                {
                    if (selectedPiece.getPColor() == 0)
                    {
                        boardButtons[i, j].Image = Resources.queen__1_;
                        boardButtons[i, j].ForeColor = Color.White;
                    }
                    else
                    {
                        boardButtons[i, j].Image = Resources.queen;
                        boardButtons[i, j].ForeColor = Color.Black;
                    }
                    boardButtons[i, j].Enabled = true;
                }
                if (selectedPiece.getType() == 6)
                {
                    if (i - selectedPiece.getLocation()[0] > 1)
                    {//castle move
                     //sets the new location for the rook
                        boardButtons[i - 1, j].Image = Resources.rook;
                        boardButtons[i - 1, j].Enabled = true;
                        game.getBoard()[7, j].setLocation(i - 1, j);
                        game.getBoard()[i - 1, j] = game.getBoard()[7, j];
                        game.getBoard()[7, j] = null;
                        boardButtons[7, j].Text = "";
                        boardButtons[7, j].Image = null;
                        boardButtons[7, j].Enabled = false;
                    }
                    if (selectedPiece.getLocation()[0] - i > 1)
                    {//castle move
                     //sets the new location for the rook
                        boardButtons[i + 1, j].Image = Resources.rook;
                        boardButtons[i + 1, j].Enabled = true;
                        game.getBoard()[0, j].setLocation(i + 1, j);
                        game.getBoard()[i + 1, j] = game.getBoard()[0, j];
                        game.getBoard()[0, j] = null;
                        boardButtons[0, j].Image = null;
                        boardButtons[0, j].Enabled = false;
                    }
                    if (selectedPiece.getPColor() == 0)
                    {
                        boardButtons[i, j].Image = Resources.horse__1_;
                        boardButtons[i, j].ForeColor = Color.White;
                    }
                    else
                    {
                        boardButtons[i, j].Image = Resources.horse;
                        boardButtons[i, j].ForeColor = Color.Black;
                    }
                    boardButtons[i, j].Enabled = true;
                }

                //set all necessary things to unclickable and null and the piece to its new location
                boardButtons[selectedPiece.getLocation()[0], selectedPiece.getLocation()[1]].Enabled = false;
                boardButtons[selectedPiece.getLocation()[0], selectedPiece.getLocation()[1]].Text = "";
                boardButtons[selectedPiece.getLocation()[0], selectedPiece.getLocation()[1]].Image = null;
                moves = null;
                game.getBoard()[i, j] = selectedPiece;
                game.getBoard()[selectedPiece.getLocation()[0], selectedPiece.getLocation()[1]] = null;
                selectedPiece.setLocation(i, j);
                selectedPiece = null;
                for (int r = 0; r < game.getPieces().Length; r++)
                {
                    if (game.getPieces()[r].getLocation()[0] == -1)
                    {//this piece isn't on the board don't bother checking it

                    }
                    else if (game.getBoard()[game.getPieces()[r].getLocation()[0], game.getPieces()[r].getLocation()[1]] == null)
                    {
                        game.getPieces()[r].setLocation(-1, -1);//the piece is no longer on the board, set its location to off the board
                    }
                    else if (game.getBoard()[game.getPieces()[r].getLocation()[0], game.getPieces()[r].getLocation()[1]].getID() != game.getPieces()[r].getID())
                    {//check if the piece is no longer on the board
                        game.getPieces()[r].setLocation(-1, -1);//the piece is no longer on the board, set its location to off the board
                    }
                    //this is for the save and load features
                }

                changeTurn();
            }
        }

        private void newGameButton_Click(object sender,
        System.EventArgs e)
        {//handles button click for a new game
            Controls.Clear();
            Label label = new Label();
            label.Text = "Select a Color";
            label.Location = new Point(20, 10);

            Button white = new Button();
            white.Text = "white";
            white.Location = new Point(10, 40);
            white.Click += whiteButton_Click;

            Button black = new Button();
            black.Text = "Black";
            black.Location = new Point(10, 80);
            black.Click += blackButton_Click;

            Controls.Add(label);
            Controls.Add(white);
            Controls.Add(black);
        }

        private void loadGameButton_Click(object sender,
        System.EventArgs e)
        {
            load();
        }

        private void whiteButton_Click(object sender,
        System.EventArgs e)
        {//if white is clicked upon new game selection set colorSel to 0
            colorSel = 0;
            Controls.Clear();
            game = new Board(colorSel);
            turn = 0;
            initializeGame();
        }

        private void blackButton_Click(object sender,
        System.EventArgs e)
        {//if black is clicked upon new game selection set colorSel to 1
            colorSel = 1;
            Controls.Clear();
            game = new Board(colorSel);
            turn = 0;
            initializeGame();
        }

        private void saveButton_Click(object sender,
        System.EventArgs e)
        {
            save();
        }



        //end event handlers

        public void save()
        {//saves the data of the current game for loading later
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var fileDir = Path.Combine(projectFolder, @"SaveData");
            (new FileInfo(fileDir)).Directory.Create();
            var file = Path.Combine(projectFolder, @"SaveData\save.txt");
            String saveData = "";
            for (int i = 0; i < game.getPieces().Length; i++)
            {//starts with piece type of the first piece then color and then the location numbers
                saveData += game.getPieces()[i].getType().ToString();
                saveData += game.getPieces()[i].getPColor().ToString();
                saveData += game.getPieces()[i].getLocation()[0].ToString();
                saveData += game.getPieces()[i].getLocation()[1].ToString();
                if (game.getPieces()[i].getType() == 1)
                {
                    saveData += ((Pawn)game.getPieces()[i]).getFirstMove();
                }
                if (game.getPieces()[i].getType() == 4)
                {
                    saveData += ((Rook)game.getPieces()[i]).getFirstMove();
                }
                if (game.getPieces()[i].getType() == 6)
                {
                    saveData += ((King)game.getPieces()[i]).getFirstMove();
                }
            }
            saveData += ":";//finally adds in the color selected when the game was first started separated with a : so it doesn't attempt to create a new piece
            saveData += colorSel.ToString();
            saveData += turn;
            File.WriteAllText(file, saveData);
        }


        public void load()
        {//used to load a previously saved game
            Controls.Clear();
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"SaveData\save.txt");
            Char[] loadData = File.ReadAllText(file).ToCharArray();
            int i = 0;
            int j = 0;
            ChessPiece[] pcs = new ChessPiece[32];
            while (i < loadData.Length)
            {//starts from the first char in the save file which is the type of the first piece in the list
             //creates new pieces from the data in the save file and adds them to a list of chesspieces to be passed to the board creation
                if (loadData[i] == '1')
                {
                    i++;
                    pcs[j] = new Pawn(j, loadData[i] - 48);
                    i++;
                    if (loadData[i] == '-')
                    {
                        pcs[j].setLocation(-1, -1);
                        i += 4;
                    }
                    else
                    {
                        pcs[j].setLocation(loadData[i] - 48, loadData[i + 1] - 48);
                        i += 2;
                    }
                    if (loadData[i] != '0')
                    {
                        ((Pawn)pcs[j]).setFirstMove(loadData[i] - 48);
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                    j++;
                }
                if (loadData[i] == '2')
                {
                    i++;
                    pcs[j] = new Knight(j, loadData[i] - 48);
                    i++;
                    if (loadData[i] == '-')
                    {
                        pcs[j].setLocation(-1, -1);
                        i += 4;
                    }
                    else
                    {
                        pcs[j].setLocation(loadData[i] - 48, loadData[i + 1] - 48);
                        i += 2;
                    }
                    j++;
                }
                if (loadData[i] == '3')
                {
                    i++;
                    pcs[j] = new Bishop(j, loadData[i] - 48);
                    i++;
                    if (loadData[i] == '-')
                    {
                        pcs[j].setLocation(-1, -1);
                        i += 4;
                    }
                    else
                    {
                        pcs[j].setLocation(loadData[i] - 48, loadData[i + 1] - 48);
                        i += 2;
                    }
                    j++;
                }
                if (loadData[i] == '4')
                {
                    i++;
                    pcs[j] = new Rook(j, loadData[i] - 48);
                    i++;
                    if (loadData[i] == '-')
                    {
                        pcs[j].setLocation(-1, -1);
                        i += 4;
                    }
                    else
                    {
                        pcs[j].setLocation(loadData[i] - 48, loadData[i + 1] - 48);
                        i += 2;
                    }
                    if (loadData[i] != '0')
                    {
                        ((Rook)pcs[j]).setFirstMove(loadData[i] - 48);
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                    j++;
                }
                if (loadData[i] == '5')
                {
                    i++;
                    pcs[j] = new Queen(j, loadData[i] - 48);
                    i++;
                    if (loadData[i] == '-')
                    {
                        pcs[j].setLocation(-1, -1);
                        i += 4;
                    }
                    else
                    {
                        pcs[j].setLocation(loadData[i] - 48, loadData[i + 1] - 48);
                        i += 2;
                    }
                    j++;
                }
                if (loadData[i] == '6')
                {
                    i++;
                    pcs[j] = new King(j, loadData[i] - 48);
                    i++;
                    if (loadData[i] == '-')
                    {
                        pcs[j].setLocation(-1, -1);
                        i += 4;
                    }
                    else
                    {
                        pcs[j].setLocation(loadData[i] - 48, loadData[i + 1] - 48);
                        i += 2;
                    }
                    if (loadData[i] != '0')
                    {
                        ((King)pcs[j]).setFirstMove(loadData[i] - 48);
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                    j++;
                }
                if (loadData[i] == ':')
                {//: means the next char is the color selection
                    i++;
                    colorSel = ((int)loadData[i] - 48);
                    i++;
                    turn = loadData[i] - 48;
                    break;
                }
            }

            game = new Board(colorSel, pcs);
            initializeGame();
        }

        public void mainMenu()
        {//sets up main menu for selecting if you want to play a new game or load the saved game
            Button newGameBtn = new Button();
            newGameBtn.Click += newGameButton_Click;
            newGameBtn.Text = "New Game";
            newGameBtn.Location = new Point(10, 20);

            Button loadGameBtn = new Button();
            loadGameBtn.Click += loadGameButton_Click;
            loadGameBtn.Text = "Load Game";
            loadGameBtn.Location = new Point(10, 50);
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"SaveData\save.txt");
            if (File.Exists(file))
            {//checks if the save file exists
                loadGameBtn.Enabled = true;
            }
            else
            {
                loadGameBtn.Enabled = false;
            }


            Controls.Add(newGameBtn);
            Controls.Add(loadGameBtn);
        }

    }
}
