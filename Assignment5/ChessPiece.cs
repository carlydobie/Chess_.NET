using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assign5
{
    abstract class ChessPiece //: IComparable
    {
        private Button piece;
        private int teamNum;
        private int xposition;
        private int yposition;
        private string idNum;

        public string IdNum
        {
            get { return idNum; }
            set { idNum = value; }
        }

        public int TeamNum
        {
            get { return teamNum; }
            set { teamNum = value; }
        }

        public int XPosition
        {
            get { return xposition; }
            set { xposition = value; }
        }

        public int YPosition
        {
            get { return yposition; }
            set { yposition = value; }
        }

        public Button Piece
        {
            get { return piece; }
            set { piece = value; }
        }

        public ChessPiece() //default constructor
        {
            teamNum = 0;
            xposition = 0;
            yposition = 0;
            piece = null;
        }

        public ChessPiece(int newTeamNum, int newXPosition, int newYPosition, Button newPiece) //constructor
        {
            piece = newPiece;
            teamNum = newTeamNum;
            xposition = newXPosition;
            yposition = newYPosition;
        }

        ~ChessPiece() //destructor
        {
            
        }

        public virtual bool ValidMove(int endPositionX, int endPositonY, List<ChessPiece> boardPieces)
        {
            return false;
        }
    }
}
