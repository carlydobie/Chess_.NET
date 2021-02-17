using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assign5
{
    class Rook : ChessPiece
    {
        bool firstMove;

        public bool FirstMove
        {
            set { firstMove = value; }
            get { return firstMove; }
        }

        public Rook(int newTeamNum, int newXPosition, int newYPosition, Button newPiece) : base( newTeamNum,  newXPosition, newYPosition,  newPiece)
        {
            firstMove = false;
        }

        // Rules for Movement 
        //       infinite spaces ^ or v or < or >

        /* Name: ValidMove
         * Input: int endPosition (where the user want the piece to go to)
         * Output: bool (wheather it is a valid position)
         * Purpose: Validates a users move of the current board piece
         * */
        public override bool ValidMove(int endPositionX, int endPositonY, List<ChessPiece> boardPieces)
        {
            int PieceX = Piece.Parent.Location.X;
            int PieceY = Piece.Parent.Location.Y;

            if ((PieceX != endPositionX) && (PieceY != endPositonY)) //moving diagonally
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}