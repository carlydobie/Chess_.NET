using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assign5
{
    class Bishop : ChessPiece
    {
        public Bishop(int newTeamNum, int newXPosition, int newYPosition, Button newPiece) : base(newTeamNum, newXPosition, newYPosition, newPiece)
        {

        }
        // Rules for Movement 
        //       infinite spaces in a diagonal direction (upper left, upper right, bottom left, bottom right)

        public override bool ValidMove(int endPositionX, int endPositonY, List<ChessPiece> boardPieces)
        {
            int x = XPosition;
            int y = YPosition;

            if(XPosition - endPositionX > 0) //if the piece is moving hoizontally to the left
            {
                // down and to the left
                if (YPosition - endPositonY > 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if((x == endPositionX) && (y == endPositonY))
                        {
                            return true;
                        }
                        else
                        {
                            x -= 56;
                            y -= 56;
                        }
                    }
                }
                // up and to the left
                else if(YPosition - endPositonY < 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if ((x == endPositionX) && (y == endPositonY))
                        {
                            return true;
                        }
                        else
                        {
                            x -= 56;
                            y += 56;
                        }
                    }
                }
                else
                {
                    return false;
                }
                
            }
            else if (XPosition - endPositionX < 0) //if the piece is moving horizonally to the right
            {
                // up and to the right
                if (YPosition - endPositonY > 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if ((x == endPositionX) && (y == endPositonY))
                        {
                            
                            return true;
                        }
                        else
                        {
                            x += 56;
                            y -= 56;
                        }
                    }
                }
                // down and to the right
                else if (YPosition - endPositonY < 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if ((x == endPositionX) && (y == endPositonY))
                        {
                            return true;
                        }
                        else
                        {
                            x += 56;
                            y += 56;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
            return false;
        }
    }
}
