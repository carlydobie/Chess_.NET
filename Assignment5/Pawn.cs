using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assign5
{
    class Pawn : ChessPiece
    {
        private bool firstMove;
        public Pawn(int newTeamNum, int newXPosition, int newYPosition, Button newPiece ) : base(newTeamNum, newXPosition, newYPosition, newPiece)
        {
            firstMove = false;
        }

        public bool FirstMove
        {
            get { return firstMove; }
            set { firstMove = value; }
        }
        // Rules for Movement 
        //       2 ^ (First Move Only)(all pawns) or 1 ^ or 1 diagonal (diagonal left or diagonal right) if attacking

        /* Name: ValidMove
         * Input: int endPosition (where the user want the piece to go to), bool attack (wheather the pawn is attacking or not)
         * Output: bool (wheather it is a valid position)
         * Purpose: Validates a users move of the current board piece
         * */
        public bool ValidMove(int endPositionX, int endPositionY, bool attack) 
        {
            int PieceX = Piece.Parent.Location.X;
            int PieceY = Piece.Parent.Location.Y;

            //moving up one space (one panel = 56 wide)
            if (!attack && (XPosition == endPositionX) && (((YPosition + 56 == endPositionY) && TeamNum == 2) || ((YPosition - 56 == endPositionY) && TeamNum == 1)))
            {
                FirstMove = true;
                return true;
            }
            //moving up 2 spaces on first move
            else if (!attack && (XPosition == endPositionX) && (((YPosition + 112 == endPositionY) && TeamNum == 2) || ((YPosition - 112 == endPositionY) && TeamNum == 1)))
            {
                FirstMove = true;
                return true;
            }

            //moving diagonlly if attacking
            else if (attack && ((XPosition + 56 == endPositionX) || (XPosition - 56 == endPositionX)) && (((YPosition + 56 == endPositionY) && TeamNum == 2) || ((YPosition - 56 == endPositionY) && TeamNum == 1)))
            {
                FirstMove = true;
                return true;
            }
            else
                return false;
        }
    }
}