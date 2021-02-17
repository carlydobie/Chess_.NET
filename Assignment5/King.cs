using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assign5
{
    class King : ChessPiece
    {
        private bool firstMove;

        public bool FirstMove
        {
            get { return firstMove; }
            set { firstMove = value; }
        }

        public King(int newTeamNum, int newXPosition, int newYPosition, Button newPiece ) : base(newTeamNum, newXPosition, newYPosition, newPiece)
        {
            firstMove = false;
        }
        public King() : base()
        {
            firstMove = false;
        }

        // Rules for Movement 
        //      1 space in any direction
        //      can do one castle move

        /* Name: ValidMove
         * Input: int endPosition (where the user want the piece to go to)
         * Output: bool (wheather it is a valid position)
         * Purpose: Validates a users move of the current board piece
         * */
        public override bool ValidMove(int endPositionX, int endPositionY, List<ChessPiece> boardPieces)
        {
            int PieceX = Piece.Parent.Location.X;
            int PieceY = Piece.Parent.Location.Y;
            //moving up one space (one panel = 56 wide)
            if (((PieceY + 56 == endPositionY) || (PieceY - 56 == endPositionY)) && (PieceX == endPositionX))
            {
                return true;
            }
            else if (((PieceX + 56 == endPositionX) || (PieceX - 56 == endPositionX)) && (PieceY == endPositionY))
            {
                return true;
            }
            else
                return false;
        }

        public bool CanCastle(List<ChessPiece> boardPieces, Button rook)
        {
            // locations of all empty spaces
            int b = 56;
            int c = 112;
            int d = 168;
            int f = 280;
            int g = 336;

            if (!FirstMove)
            {
                // check to see if there are any friendly pieces in the way.
                foreach (ChessPiece chess in boardPieces)
                {
                    // check to see if on same team
                    if (chess.TeamNum == TeamNum)
                    {
                        if (rook.Parent.Location.X - XPosition < 0)
                        {
                            if (chess.YPosition == YPosition && (chess.XPosition == b || chess.XPosition == c || chess.XPosition == d))
                            {
                                return false;
                            }
                        }
                        else if (chess.YPosition == YPosition && (chess.XPosition == f || chess.XPosition == g))
                        {
                            return false;
                        }
                    }
                }

                //MessageBox.Show("test");

                foreach (ChessPiece chess in boardPieces)
                {
                    if (chess.TeamNum != TeamNum)
                    {
                        // tested works
                        if (chess is Pawn)
                        {
                            if (YPosition == 0)
                            {
                                if (rook.Parent.Location.X - XPosition < 0 && chess.YPosition == YPosition + 56)
                                {
                                    if (chess.XPosition <= XPosition)
                                    {
                                        return false;
                                    }
                                }
                                else if (chess.YPosition == YPosition + 56)
                                {
                                    if (chess.XPosition >= XPosition)
                                    {
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                if (rook.Parent.Location.X - XPosition < 0 && chess.YPosition == YPosition - 56)
                                {
                                    if (chess.XPosition <= XPosition)
                                    {
                                        return false;
                                    }
                                }
                                else if (chess.YPosition == YPosition - 56)
                                {
                                    if (chess.XPosition >= XPosition)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }

                        if (chess is Rook || chess is Queen)
                        {   // left side
                            if (rook.Parent.Location.X - XPosition < 0)
                            {
                                // upper side
                                if (YPosition == 0)
                                {
                                    // upper left
                                    if (chess.XPosition <= XPosition)
                                    {
                                        for (int i = YPosition; i < chess.YPosition; i += 56)
                                        {
                                            foreach (ChessPiece piece in boardPieces)
                                            {
                                                if (piece.XPosition == chess.XPosition && piece.YPosition == i)
                                                {
                                                    return true;
                                                }
                                            }
                                        }
                                        return false;
                                    }
                                }
                                else
                                {
                                    // lower left
                                    if (chess.XPosition <= XPosition)
                                    {
                                        for (int i = YPosition; i > chess.YPosition; i -= 56)
                                        {
                                            foreach (ChessPiece piece in boardPieces)
                                            {
                                                if (piece.XPosition == chess.XPosition && piece.YPosition == i)
                                                {
                                                    return true;
                                                }
                                            }
                                        }
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                if (YPosition == 0)
                                {
                                    if (chess.XPosition >= XPosition)
                                    {
                                        for (int i = YPosition; i < chess.YPosition; i += 56)
                                        {
                                            foreach (ChessPiece piece in boardPieces)
                                            {
                                                if (piece.XPosition == chess.XPosition && piece.YPosition == i)
                                                {
                                                    return true;
                                                }
                                            }
                                        }
                                        return false;
                                    }
                                }
                                else
                                {
                                    if (chess.XPosition >= XPosition)
                                    {
                                        for (int i = YPosition; i > chess.YPosition; i -= 56)
                                        {
                                            foreach (ChessPiece piece in boardPieces)
                                            {
                                                if (piece.XPosition == chess.XPosition && piece.YPosition == i)
                                                {
                                                    return true;
                                                }
                                            }
                                        }
                                        return false;
                                    }
                                }
                            }

                        }

                        if (chess is Bishop || chess is Queen)
                        {

                        }

                        if (chess is Knight)
                        {

                        }
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

    }
}
