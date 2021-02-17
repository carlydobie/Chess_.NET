using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assign5
{
    class Knight : ChessPiece
    {
        public Knight(int newTeamNum, int newXPosition, int newYPosition, Button newPiece) 
            : base(newTeamNum, newXPosition, newYPosition, newPiece)
        {

        }
        /*
        * rules for moving
        * 
        * 2 up/down 1 left/right
        * 
        * 1 up/down 2 left/right
        * */ 
        override public bool ValidMove(int endPositionX, int endPositionY,List<ChessPiece> boardPieces)
        {
            int PieceX = Piece.Parent.Location.X;
            int PieceY = Piece.Parent.Location.Y;

            //knight has moved two spaces up or two spaces down
            if ((PieceY + 112 == endPositionY) || (PieceY - 112 == endPositionY))
            {
                //knight has moved one space to the right or left
                if ((PieceX + 56 == endPositionX) || (PieceX - 56 == endPositionX))
                {
                    return true; //L shaped movement
                }
                else
                    return false; //not L shaped movement
            }
            else if ((PieceX + 112 == endPositionX) || (PieceX - 112 == endPositionX))
            {
                //knight has moved one space to the right or left
                if ((PieceY + 56 == endPositionY) || (PieceY - 56 == endPositionY))
                {
                    return true; //L shaped movement
                }
                else
                    return false; //not L shaped movement
            }
            else
                return false; //not L shaped movement
        }
    }
}
