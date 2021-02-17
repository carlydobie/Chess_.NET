using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assign5
{ 
    /* TO DO LIST
     * 
     * validate moves (Pices all move correctly. we need to figure out if a piece jumps another piece. only the knight can jump other pieces)
     * implement the check phase (check happens when an opposing team will be able to take the king piece on their next turn)
     * implement the castle phase (recommend looking at a video about how to do this)
     * 
     * */

    ///* RULES FOR MOVEMENT
    // *  King: 1 space in any direction
    // *      King can Castle
    // *  Queen: infinate spaces in any direction
    // *  Knight: ^ 2 > 1 or ^ 2 < 1 or ^ 1 > 2 or ^ 1 < 2 or the complement of those
    // *  Bishop: infinate spaces in a diagonal direction (upper left, upper right, bottom left, bottom right)
    // *  Rook: infinate spaces ^ or v or < or >
    // *  Pawn: 2 ^ (First Move Only) or 1 ^ or 1 diagonal (upper left or upper right) if attacking
    // *      Pawn might be able to attack from behind
    // */
    public partial class Form1 : System.Windows.Forms.Form 
    {
        readonly List<ChessPiece> boardPieces = new List<ChessPiece>(); //list of all pieces on the board
        int playersTurn = 1;
        readonly int Black_Team = 1;
        readonly int White_Team = 2;
        bool attackMove = false;
        bool castle = false;
        Button underAttackPiece;
        Button SelectedPiece;
        Button CastlePiece;
        Panel ChosenLocation; //location user wants to move their piece to
        Color OriginalPanelColor; //the original color of the panel (position on board) before it was highlighted green
        int blackWins;
        int whiteWins;

        //load the piece images onto the buttons
        readonly Image blkRook = Image.FromFile("../../Game Pieces 2/Chess_rdt60.png");
        readonly Image whtRook = Image.FromFile("../../Game Pieces 2/Chess_rlt60.png");
        readonly Image blkKnight = Image.FromFile("../../Game Pieces 2/Chess_ndt60.png");
        readonly Image whtKnight = Image.FromFile("../../Game Pieces 2/Chess_nlt60.png");
        readonly Image blkBishop = Image.FromFile("../../Game Pieces 2/Chess_bdt60.png");
        readonly Image whtBishop = Image.FromFile("../../Game Pieces 2/Chess_blt60.png");
        readonly Image blkPawn = Image.FromFile("../../Game Pieces 2/Chess_pdt60.png");
        readonly Image whtPawn = Image.FromFile("../../Game Pieces 2/Chess_plt60.png");
        readonly Image blkKing = Image.FromFile("../../Game Pieces 2/Chess_kdt60.png");
        readonly Image whtKing = Image.FromFile("../../Game Pieces 2/Chess_klt60.png");
        readonly Image blkQueen = Image.FromFile("../../Game Pieces 2/Chess_qdt60.png");
        readonly Image whtQueen = Image.FromFile("../../Game Pieces 2/Chess_qlt60.png");

        //create chesspiece objects for each chess piece button
        //black pieces
        ChessPiece Black_Rook1;
        ChessPiece Black_Knight1;
        ChessPiece Black_Bishop1;
        ChessPiece Black_King;
        ChessPiece Black_Queen;
        ChessPiece Black_Bishop2;
        ChessPiece Black_Knight2;
        ChessPiece Black_Rook2;
        ChessPiece Black_Pawn1;
        ChessPiece Black_Pawn2;
        ChessPiece Black_Pawn3;
        ChessPiece Black_Pawn4;
        ChessPiece Black_Pawn5;
        ChessPiece Black_Pawn6;
        ChessPiece Black_Pawn7;
        ChessPiece Black_Pawn8;

        //white pieces
        ChessPiece White_Rook1;
        ChessPiece White_Knight1;
        ChessPiece White_Bishop1;
        ChessPiece White_King;
        ChessPiece White_Queen;
        ChessPiece White_Bishop2;
        ChessPiece White_Knight2;
        ChessPiece White_Rook2;
        ChessPiece White_Pawn1;
        ChessPiece White_Pawn2;
        ChessPiece White_Pawn3;
        ChessPiece White_Pawn4;
        ChessPiece White_Pawn5;
        ChessPiece White_Pawn6;
        ChessPiece White_Pawn7;
        ChessPiece White_Pawn8;
        
        public Form1()
        {
            InitializeComponent();
        }

        public void NewGame(Object sender, EventArgs e)
        {
            blackWins = 0;
            whiteWins = 0;
            Black_label.Text = Convert.ToString(blackWins);
            White_label.Text = Convert.ToString(whiteWins);
            ChessBoard.Enabled = true;
            Reset_button.Visible = true;
            Turn_label.Visible = true;
            Move_button.Visible = true;
            Start_button.Visible = false;
            attackMove = false;

            Log_richTextBox.Text = "";
            //indicate whose turn it is
            if (White_checkBox.Checked)
            {
                Turn_label.Text = "It's Player 2's (White) Turn\n";
            }
            else
            {
                Turn_label.Text = "It's Player 1's (Black) Turn\n";
            }
            //add pieces to the list boardPieces and initialize them
            //black pieces
            boardPieces.Add(Black_Rook1 = new Rook(Black_Team, a1.Location.X, a1.Location.Y, blk_rook1));
            blk_rook1.Image = blkRook; 
            boardPieces.Add(Black_Knight1 = new Knight(Black_Team, b1.Location.X, b1.Location.Y, blk_knight1));
            blk_knight1.Image = blkKnight;
            boardPieces.Add(Black_Bishop1 = new Bishop(Black_Team, c1.Location.X, c1.Location.Y, blk_bishop1));
            blk_bishop1.Image = blkBishop;
            boardPieces.Add(Black_Queen = new Queen(Black_Team, d1.Location.X, c1.Location.Y, blk_queen));
            blk_queen.Image = blkQueen;
            boardPieces.Add(Black_King = new King(Black_Team, e1.Location.X, e1.Location.Y, blk_king));
            blk_king.Image = blkKing;
            boardPieces.Add(Black_Bishop2 = new Bishop(Black_Team, f1.Location.X, f1.Location.Y, blk_bishop2));
            blk_bishop2.Image = blkBishop;
            boardPieces.Add(Black_Knight2 = new Knight(Black_Team, g1.Location.X, g1.Location.Y, blk_knight2));
            blk_knight2.Image = blkKnight;
            boardPieces.Add(Black_Rook2 = new Rook(Black_Team, h1.Location.X, h1.Location.Y, blk_rook2));
            blk_rook2.Image = blkRook;
            boardPieces.Add(Black_Pawn1 = new Pawn(Black_Team, a2.Location.X, a2.Location.Y, blk_pawn1));
            blk_pawn1.Image = blkPawn;
            boardPieces.Add(Black_Pawn2 = new Pawn(Black_Team, b2.Location.X, b2.Location.Y, blk_pawn2));
            blk_pawn2.Image = blkPawn;
            boardPieces.Add(Black_Pawn3 = new Pawn(Black_Team, c2.Location.X, c2.Location.Y, blk_pawn3));
            blk_pawn3.Image = blkPawn;
            boardPieces.Add(Black_Pawn4 = new Pawn(Black_Team, d2.Location.X, d2.Location.Y, blk_pawn4));
            blk_pawn4.Image = blkPawn;
            boardPieces.Add(Black_Pawn5 = new Pawn(Black_Team, e2.Location.X, e2.Location.Y, blk_pawn5));
            blk_pawn5.Image = blkPawn;
            boardPieces.Add(Black_Pawn6 = new Pawn(Black_Team, f2.Location.X, f2.Location.Y, blk_pawn6));
            blk_pawn6.Image = blkPawn;
            boardPieces.Add(Black_Pawn7 = new Pawn(Black_Team, g2.Location.X, g2.Location.Y, blk_pawn7));
            blk_pawn7.Image = blkPawn;
            boardPieces.Add(Black_Pawn8 = new Pawn(Black_Team, h2.Location.X, h2.Location.Y, blk_pawn8));
            blk_pawn8.Image = blkPawn;

            //white pieces
            boardPieces.Add(White_Rook1 = new Rook(White_Team, a8.Location.X, a8.Location.Y, wht_rook1));
            wht_rook1.Image = whtRook;
            boardPieces.Add(White_Knight1 = new Knight(White_Team, b8.Location.X, b8.Location.Y, wht_knight1));
            wht_knight1.Image = whtKnight;
            boardPieces.Add(White_Bishop1 = new Bishop(White_Team, c8.Location.X, c8.Location.Y, wht_bishop1));
            wht_bishop1.Image = whtBishop;
            boardPieces.Add(White_Queen = new Queen(White_Team, d8.Location.X, d8.Location.Y, wht_queen));
            wht_queen.Image = whtQueen;
            boardPieces.Add(White_King = new King(White_Team, e8.Location.X, e8.Location.Y, wht_king));
            wht_king.Image = whtKing;
            boardPieces.Add(White_Bishop2 = new Bishop(White_Team, f8.Location.X, f8.Location.Y, wht_bishop2));
            wht_bishop2.Image = whtBishop;
            boardPieces.Add(White_Knight2 = new Knight(White_Team, g8.Location.X, g8.Location.Y, wht_knight2));
            wht_knight2.Image = whtKnight;
            boardPieces.Add(White_Rook2 = new Rook(White_Team, h8.Location.X, h8.Location.Y, wht_rook2));
            wht_rook2.Image = whtRook;
            boardPieces.Add(White_Pawn1 = new Pawn(White_Team, a7.Location.X, a7.Location.Y, wht_pawn1));
            wht_pawn1.Image = whtPawn;
            boardPieces.Add(White_Pawn2 = new Pawn(White_Team, b7.Location.X, b7.Location.Y, wht_pawn2));
            wht_pawn2.Image = whtPawn;
            boardPieces.Add(White_Pawn3 = new Pawn(White_Team, c7.Location.X, c7.Location.Y, wht_pawn3));
            wht_pawn3.Image = whtPawn;
            boardPieces.Add(White_Pawn4 = new Pawn(White_Team, d7.Location.X, d7.Location.Y, wht_pawn4));
            wht_pawn4.Image = whtPawn;
            boardPieces.Add(White_Pawn5 = new Pawn(White_Team, e7.Location.X, e7.Location.Y, wht_pawn5));
            wht_pawn5.Image = whtPawn;
            boardPieces.Add(White_Pawn6 = new Pawn(White_Team, f7.Location.X, f7.Location.Y, wht_pawn6));
            wht_pawn6.Image = whtPawn;
            boardPieces.Add(White_Pawn7 = new Pawn(White_Team, g7.Location.X, g7.Location.Y, wht_pawn7));
            wht_pawn7.Image = whtPawn;
            boardPieces.Add(White_Pawn8 = new Pawn(White_Team, h7.Location.X, h7.Location.Y, wht_pawn8));
            wht_pawn8.Image = whtPawn;
        }

        public void GameReset() 
        {
            attackMove = false;

            //return pieces to their starting locations
            //black pieces
            blk_rook1.Visible = true;
            blk_rook1.Enabled = true;
            blk_rook1.Parent = a1;
            blk_knight1.Visible = true;
            blk_knight1.Enabled = true;
            blk_knight1.Parent = b1;
            blk_bishop1.Visible = true;
            blk_bishop1.Enabled = true;
            blk_bishop1.Parent = c1;
            blk_queen.Visible = true;
            blk_queen.Enabled = true;
            blk_queen.Parent = d1;
            blk_king.Visible = true;
            blk_king.Enabled = true;
            blk_king.Parent = e1;
            blk_bishop2.Visible = true;
            blk_bishop2.Enabled = true;
            blk_bishop2.Parent = f1;
            blk_knight2.Visible = true;
            blk_knight2.Enabled = true;
            blk_knight2.Parent = g1;
            blk_rook2.Visible = true;
            blk_rook2.Enabled = true;
            blk_rook2.Parent = h1;
            blk_pawn1.Visible = true;
            blk_pawn1.Enabled = true;
            blk_pawn1.Parent = a2;
            blk_pawn2.Visible = true;
            blk_pawn2.Enabled = true;
            blk_pawn2.Parent = b2;
            blk_pawn3.Visible = true;
            blk_pawn3.Enabled = true;
            blk_pawn3.Parent = c2;
            blk_pawn4.Visible = true;
            blk_pawn4.Enabled = true;
            blk_pawn4.Parent = d2;
            blk_pawn5.Visible = true;
            blk_pawn5.Enabled = true;
            blk_pawn5.Parent = e2;
            blk_pawn6.Visible = true;
            blk_pawn6.Enabled = true;
            blk_pawn6.Parent = f2;
            blk_pawn7.Visible = true;
            blk_pawn7.Enabled = true;
            blk_pawn7.Parent = g2;
            blk_pawn8.Visible = true;
            blk_pawn8.Enabled = true;
            blk_pawn8.Parent = h2;

            //white pieces
            wht_rook1.Visible = true;
            wht_rook1.Enabled = true;
            wht_rook1.Parent = a8;
            wht_knight1.Visible = true;
            wht_knight1.Enabled = true;
            wht_knight1.Parent = b8;
            wht_bishop1.Visible = true;
            wht_bishop1.Enabled = true;
            wht_bishop1.Parent = c8;
            wht_queen.Visible = true;
            wht_queen.Enabled = true;
            wht_queen.Parent = d8;
            wht_king.Visible = true;
            wht_king.Enabled = true;
            wht_king.Parent = e8;
            wht_bishop2.Visible = true;
            wht_bishop2.Enabled = true;
            wht_bishop2.Parent = f8;
            wht_knight2.Visible = true;
            wht_knight2.Enabled = true;
            wht_knight2.Parent = g8;
            wht_rook2.Visible = true;
            wht_rook2.Enabled = true;
            wht_rook2.Parent = h8;
            wht_pawn1.Visible = true;
            wht_pawn1.Enabled = true;
            wht_pawn1.Parent = a7;
            wht_pawn2.Visible = true;
            wht_pawn2.Enabled = true;
            wht_pawn2.Parent = b7;
            wht_pawn3.Visible = true;
            wht_pawn3.Enabled = true;
            wht_pawn3.Parent = c7;
            wht_pawn4.Visible = true;
            wht_pawn4.Enabled = true;
            wht_pawn4.Parent = d7;
            wht_pawn5.Visible = true;
            wht_pawn5.Enabled = true;
            wht_pawn5.Parent = e7;
            wht_pawn6.Visible = true;
            wht_pawn6.Enabled = true;
            wht_pawn6.Parent = f7;
            wht_pawn7.Visible = true;
            wht_pawn7.Enabled = true;
            wht_pawn7.Parent = g7;
            wht_pawn8.Visible = true;
            wht_pawn8.Enabled = true;
            wht_pawn8.Parent = h7;

            if (Black_checkBox.Checked) //black team goes first
            {
                playersTurn = 1;
                Turn_label.Text = "Its Player 1 (Black) Turn!";
            }
            else //white team goes first
            {
                playersTurn = 2;
                Turn_label.Text = "Its Player 2 (White) Turn!";
            }

            //clear move log
            Log_richTextBox.Text = "";

            // reset the pieces that were selected
            if (SelectedPiece != null)
            {
                SelectedPiece.BackColor = Color.Transparent;
                SelectedPiece = null;
            }
            if (ChosenLocation != null)
            {
                ChosenLocation.BackColor = OriginalPanelColor;
                ChosenLocation = null;
            }
            if (underAttackPiece != null)
            {
                underAttackPiece.BackColor = Color.Transparent;
                underAttackPiece = null;
                attackMove = false;
            }
            if (CastlePiece != null)
            {
                CastlePiece.BackColor = Color.Transparent;
                CastlePiece = null;
                castle = false;
            }

            //update x and y position member variables
            foreach (ChessPiece piece in boardPieces)
            {
                UpdatePiece(piece);
                if (piece is Pawn)
                {
                    (piece as Pawn).FirstMove = false;
                }
            }
        }
        public void ResetBoard(Object sender, EventArgs e) //when reset button is pressed
        {
            GameReset();
        }

        public void UpdateBoard() //update board after each move
        {            
            foreach (ChessPiece piece in boardPieces)
            {
                UpdatePiece(piece); //update piece on board
            }

            //indicate whose turn it is
            if (playersTurn == 2) //team black currently
            {
                playersTurn = 1; //team white turn now
                Turn_label.Text = "Its Player 1 (Black) Turn!";
            }
            else if (playersTurn == 1) //team white currently
            {
                playersTurn = 2; //team black turn now
                Turn_label.Text = "Its Player 2 (White) Turn!";
            }
            else //error
            {
                Turn_label.Text = "Error with Game (Num of Players)";
            }
        }

        void UpdatePiece(ChessPiece piece) //set location of button to where it has been moved to
        {
            piece.XPosition = piece.Piece.Parent.Location.X;
            piece.YPosition = piece.Piece.Parent.Location.Y;
        }

        private void Black_checkBox_CheckedChanged(object sender, EventArgs e) //when black is checked
        {
            if (Black_checkBox.Checked)
            {
                White_checkBox.Checked = false; //uncheck white checkbox
                playersTurn = 1;
            }
        }

        private void White_checkBox_CheckedChanged(object sender, EventArgs e) //when white is checked
        {
            if (White_checkBox.Checked)
            {
                Black_checkBox.Checked = false; //uncheck black checkbox
                playersTurn = 2;
            }
        }

        private void Piece_Click(object sender, EventArgs e) //a chess piece has been selected
        {
            if (SelectedPiece != null && ((sender as Button).BackColor != Color.LightGreen && TeamNum(sender as Button) == playersTurn))
            {
                // check for castle
                if (Convert.ToString((sender as Button).Tag) == "Rook")
                {
                    if (Convert.ToString((SelectedPiece as Button).Tag) == "King")
                    {
                        CastlePiece = sender as Button;
                        (sender as Button).BackColor = Color.LightGreen;
                        ChosenLocation = CastlePiece.Parent as Panel;
                        OriginalPanelColor = ChosenLocation.BackColor;
                        castle = true;
                    }
                    else
                        return;
                }
                // if not castle return
                else
                    return; //if a piece was already selected and the user isn't unselecting it, do nothing
            }
            else
            {
                if ((sender as Button).BackColor == Color.LightGreen) //if the player selects a piece they chose
                {
                    if(ChosenLocation == null && underAttackPiece == null && CastlePiece == null)
                    {
                        (sender as Button).BackColor = Color.Transparent; //unselect that piece
                        SelectedPiece = null;
                    }
                    if (ChosenLocation != null && underAttackPiece == null && CastlePiece == null) //if a location was chosen also, unchose it
                    {
                        ChosenLocation.BackColor = OriginalPanelColor; //return chosen panel back to its original color
                        ChosenLocation = null; //no location is chose
                        (sender as Button).BackColor = Color.Transparent; //unselect that piece
                        SelectedPiece.BackColor = Color.Transparent;
                        SelectedPiece = null;
                    }
                    else if(underAttackPiece != null && attackMove && CastlePiece == null)
                    {
                        (sender as Button).BackColor = Color.Transparent; //unselect that piece
                        underAttackPiece.BackColor = Color.Transparent;
                        underAttackPiece = null;
                        attackMove = false;
                        if (TeamNum(sender as Button) == playersTurn)
                        {
                            SelectedPiece = null;
                        }
                    }
                    else if(CastlePiece != null)
                    {
                        (sender as Button).BackColor = Color.Transparent; //unselect that piece
                        CastlePiece.BackColor = Color.Transparent;
                        CastlePiece = null;
                        castle = false;
                        if (Convert.ToString((sender as Button).Tag) == "King")
                        {
                            SelectedPiece = null;
                        }
                    }
                }
                else if (CastlePiece == null) //otherwise note the user's selection and change the background color
                {
                    // if player is not attack another piece
                    if (TeamNum(sender as Button) == playersTurn)
                    {
                        SelectedPiece = sender as Button; //set the button pressed as the selected piece
                        (sender as Button).BackColor = Color.LightGreen; //change the button's background color
                    }
                    // if a player is attacking a piece
                    else if(SelectedPiece != null && underAttackPiece == null)
                    {
                        underAttackPiece = sender as Button; //set the button pressed as the under attack piece
                        attackMove = true;
                        (sender as Button).BackColor = Color.LightGreen; //change the button's background color
                        ChosenLocation = underAttackPiece.Parent as Panel;
                        OriginalPanelColor = ChosenLocation.BackColor;
                    }
                }
            }

            int TeamNum(Button chessPiece) //what team each piece belongs to
            {
                switch (Convert.ToString(chessPiece.Name))
                {
                    //black pieces
                    case "blk_rook1":
                        return Black_Team;
                    case "blk_knight1":
                        return Black_Team;
                    case "blk_bishop1":
                        return Black_Team;
                    case "blk_queen":
                        return Black_Team;
                    case "blk_king":
                        return Black_Team;
                    case "blk_bishop2":
                        return Black_Team;
                    case "blk_knight2":
                        return Black_Team;
                    case "blk_rook2":
                        return Black_Team;
                    case "blk_pawn1":
                        return Black_Team;
                    case "blk_pawn2":
                        return Black_Team;
                    case "blk_pawn3":
                        return Black_Team;
                    case "blk_pawn4":
                        return Black_Team;
                    case "blk_pawn5":
                        return Black_Team;
                    case "blk_pawn6":
                        return Black_Team;
                    case "blk_pawn7":
                        return Black_Team;
                    case "blk_pawn8":
                        return Black_Team;

                    //white pieces
                    case "wht_rook1":
                        return White_Team;
                    case "wht_knight1":
                        return White_Team;
                    case "wht_bishop1":
                        return White_Team;
                    case "wht_queen":
                        return White_Team;
                    case "wht_king":
                        return White_Team;
                    case "wht_bishop2":
                        return White_Team;
                    case "wht_knight2":
                        return White_Team;
                    case "wht_rook2":
                        return White_Team;
                    case "wht_pawn1":
                        return White_Team;
                    case "wht_pawn2":
                        return White_Team;
                    case "wht_pawn3":
                        return White_Team;
                    case "wht_pawn4":
                        return White_Team;
                    case "wht_pawn5":
                        return White_Team;
                    case "wht_pawn6":
                        return White_Team;
                    case "wht_pawn7":
                        return White_Team;
                    case "wht_pawn8":
                        return White_Team;
                    default:
                        return 0;
                }
            }
        }

        private void Panel_Click(object sender, EventArgs e) //when a location to move on board is clicked
        {
            if ((SelectedPiece == null) || ((ChosenLocation != null) && ((sender as Panel).BackColor != Color.LightGreen))) 
            {
                return; //if a piece hasn't been chosen yet or if a location was already chosen and isn't being unchosen, do nothing
            }
            else
            {
                if ((sender as Panel).BackColor == Color.LightGreen) //unselect panel
                {
                    ChosenLocation = null; //no chosen location
                    (sender as Panel).BackColor = OriginalPanelColor; //return panel back to its original color
                }
                else //no panel is selected
                {
                    underAttackPiece = null;
                    attackMove = false;
                    OriginalPanelColor = (sender as Panel).BackColor; //take note of it's initial color
                    ChosenLocation = sender as Panel; //set location to panel that was clicked 
                    (sender as Panel).BackColor = Color.LightGreen; //set the panel's background color 
                }
            }
        }

        private void Movepiece() //moves a selected chess piece when move is valid
        {
            //Parent = panel object that holds the piece (button object)
            if (attackMove)
            {
                foreach(ChessPiece chessPiece in boardPieces)
                {
                    if(chessPiece.Piece.Name == underAttackPiece.Name)
                    {
                        underAttackPiece.Enabled = false;
                        underAttackPiece.Visible = false;
                        attackMove = false;
                        SelectedPiece.Parent = ChosenLocation; //move piece
                        underAttackPiece.BackColor = Color.Transparent;
                        if(chessPiece is King)
                        {
                            if (playersTurn == Black_Team)
                            {
                                blackWins++;
                                Log_label.Text += "\nBlack Team Wins. Resetting Board.";
                                Black_label.Text = Convert.ToString(blackWins);
                                GameReset();
                            }
                            else
                            {
                                whiteWins++;
                                Log_label.Text += "\nWhite Team Wins. Resetting Board.";
                                White_label.Text = Convert.ToString(whiteWins);
                                GameReset();
                            }
                        }
                        return;
                    }
                }
            }
            else if (castle)
            {
                if(playersTurn == Black_Team)
                {
                    if(blk_king.Parent.Location.X - CastlePiece.Parent.Location.X  < 0)
                    {
                        SelectedPiece.Parent = g1;
                        CastlePiece.Parent = e1;
                    }
                    else
                    {
                        SelectedPiece.Parent = c1;
                        CastlePiece.Parent = e1;
                    }
                }
                else
                {
                    if (wht_king.Parent.Location.X - CastlePiece.Parent.Location.X < 0)
                    {
                        SelectedPiece.Parent = g8;
                        CastlePiece.Parent = e8;
                    }
                    else
                    {
                        SelectedPiece.Parent = c8;
                        CastlePiece.Parent = e8;
                    }
                }
                CastlePiece.BackColor = Color.Transparent;
            }
            else
            {
                // write in log message that a player moved their piece
                if (playersTurn == 1)
                {
                    Log_richTextBox.Text += "Player 1 Moved ";
                }
                else
                {
                    Log_richTextBox.Text += "Player 2 Moved ";
                }
                //where the piece has been moved to
                Log_richTextBox.Text += SelectedPiece.Tag.ToString() + " to ";
                Log_richTextBox.Text += ChosenLocation.Name + "\n";
                SelectedPiece.Parent = ChosenLocation; //move piece
                attackMove = false;
            }
            //reset state of button (piece) and panel
            ChosenLocation.BackColor = OriginalPanelColor;
            SelectedPiece.BackColor = Color.Transparent;
            
            //update the board with the new location
            UpdateBoard();
            
            //reset selected piece and location variables
            SelectedPiece = null;
            ChosenLocation = null;
            underAttackPiece = null;
            CastlePiece = null;
        }

        private void Move_Click(object sender, EventArgs e) //when the move button is clicked
        {
            King check = new King();

            if (SelectedPiece == null) //can't move if no chesspiece has been selected
            {
                Log_richTextBox.Text += "No chesspiece selected.\n";
                return; //return to form
            }
            if (ChosenLocation == null) //can't move if no location has been chosen
            {
                Log_richTextBox.Text += "No board location selected.\n";
                return; //return to form
            }
            else //piece is selected and location chosen
            {
                foreach(ChessPiece chess in boardPieces)
                {
                    if(chess is King)
                    {
                        if (chess.TeamNum != playersTurn)
                        {
                            check = chess as King;
                            break;
                        }
                    }
                }

                foreach (ChessPiece chesspiece in boardPieces) //find chesspiece for selected button
                {
                    if (chesspiece.Piece.Name == SelectedPiece.Name)
                    {
                        //*******************************************************************************
                        //ONLY WORKS FOR SOME OF THE PIECES; SEEMS TO HAVE AN ISSUE WITH THE 3 PAWNS
                        //IN THE MIDDLE FOR EACH TEAM
                        //*******************************************************************************
                        //    if (!(chesspiece is Knight)) //for pieces that aren't knights
                        //    {
                        //        foreach (ChessPiece intheway in boardPieces)
                        //        {
                        //            for (int i = chesspiece.Piece.Location.Y + 56; i < ChosenLocation.Location.Y; i += 56)
                        //            {
                        //                for (int j = chesspiece.Piece.Location.X + 56; j < ChosenLocation.Location.; j += 56)
                        //                {
                        //                    if (intheway.Piece.Parent.Location.Y == j && intheway.Piece.Parent.Location.X == i)
                        //                    {
                        //                        //tell user that move was invalid
                        //                        Log_richTextBox.Text += "Invalid move for ";
                        //                        if (playersTurn == 1)
                        //                        {
                        //                            Log_richTextBox.Text += "Black ";
                        //                        }
                        //                        else
                        //                        {
                        //                            Log_richTextBox.Text += "White ";
                        //                        }
                        //                        Log_richTextBox.Text += SelectedPiece.Tag + ".\n";

                        //                        //reset state of button (piece) and panel
                        //                        ChosenLocation.BackColor = OriginalPanelColor;
                        //                        ChosenLocation = null;
                        //                        if (attackMove)
                        //                        {
                        //                            underAttackPiece.BackColor = Color.Transparent;
                        //                            underAttackPiece = null;
                        //                            attackMove = false;
                        //                        }
                        //                        return; //return to form
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }

                        if (chesspiece is Pawn) //check move validity for pawn pieces
                        {
                            if ((chesspiece as Pawn).ValidMove(ChosenLocation.Location.X, ChosenLocation.Location.Y, attackMove))
                            {
                                Movepiece(); //if the move is valid, move the piece
                                (chesspiece as Pawn).FirstMove = true;
                                if ((chesspiece as Pawn).ValidMove(check.XPosition, check.YPosition, true))
                                    return; //return to form
                                return;
                            }
                            else //move is not valid
                            {
                                //tell user that move was invalid
                                Log_richTextBox.Text += "Invalid move for ";
                                if (playersTurn == 1)
                                {
                                    Log_richTextBox.Text += "Black ";
                                }
                                else
                                {
                                    Log_richTextBox.Text += "White ";
                                }
                                Log_richTextBox.Text += SelectedPiece.Tag + ".\n";

                                //reset state of button (piece) and panel
                                ChosenLocation.BackColor = OriginalPanelColor;
                                ChosenLocation = null;
                                if (attackMove)
                                {
                                    underAttackPiece.BackColor = Color.Transparent;
                                    underAttackPiece = null;
                                    attackMove = false;
                                }
                                return; //return to form
                            }
                        }
                        else if (chesspiece is King && castle) //selected piece is a king and it's a castle move
                        {
                            /*  king can move two spaces towards the selected rook. rook then occupies space where king was
                             *      cannot castle if king has previously moved
                             *      cannot castle to side of a rook that has previously moved
                             *      cannot castle where a king would move through a check
                             *      cannot castle during a check
                             *      cannot castle until spaces between king and rook are empty
                             * 
                             * */
                            if (castle)
                            {
                                if ((chesspiece as King).CanCastle(boardPieces, CastlePiece) == true)
                                {
                                    (chesspiece as King).FirstMove = true;
                                    Movepiece();
                                    return;
                                }
                            }
                        }
                        else //all other pieces run their method decided at runtime 
                        {
                            if (chesspiece.ValidMove(ChosenLocation.Location.X, ChosenLocation.Location.Y, boardPieces))
                            {
                                Movepiece(); //if the move is valid, move the piece
                                if (chesspiece.ValidMove(check.XPosition, check.YPosition, boardPieces)) //if in check position
                                {
                                    //let players know king is checked
                                    if(playersTurn == Black_Team)
                                    {
                                        Log_richTextBox.Text += "\n\n Black Team Checks White King\n";
                                        return;
                                    }
                                    else
                                    {
                                        Log_richTextBox.Text += "\n\n Whtie Team Checks Black King\n";
                                        return;
                                    }
                                }
                                return; //return to form
                            }
                            else //move is not valid
                            {
                                //let players know the move was invalid
                                Log_richTextBox.Text += "Invalid move for ";
                                if (playersTurn == 1)
                                {
                                    Log_richTextBox.Text += "Black ";
                                }
                                else
                                {
                                    Log_richTextBox.Text += "White ";
                                }
                                Log_richTextBox.Text += SelectedPiece.Tag + ".\n";

                                //reset state of button (piece) and panel
                                ChosenLocation.BackColor = OriginalPanelColor;
                                ChosenLocation = null;
                                if (attackMove)
                                {
                                    underAttackPiece.BackColor = Color.Transparent;
                                    underAttackPiece = null;
                                    attackMove = false;
                                }
                                return; //return to form
                            }
                        }
                    }
                }
                //return;
            }
        }
    }
}