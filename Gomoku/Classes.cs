using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Gomoku
{
    class Classes
    {
    }

    public class Stone : System.Windows.Forms.Button
    {
        private int row, column, owner = 0, aiWeight = 0, color = 0;

        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        public int Owner { get => owner; set => owner = value; }
        public int AiWeight { get => aiWeight; set => aiWeight = value; }
        public int Color
        {
            get => color;
            set
            {
                color = value;
                BackgroundImage = StoneImage.FromColor(color);
            }
        }

        public bool IsEmpty { get => (Color == 0); }

        public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.HighQualityBicubic;

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            base.OnPaint(paintEventArgs);
        }

        public static bool Exists(int row, int col)
        {
            return (row >= 0 && row < 15 && col >= 0 && col < 15);
        }

        // Set a player
        public void SetPlayer(Player value)
        {
            Owner = (value != null) ? value.PlayerId : 0;
            Color = (value != null) ? value.Color : 0;
        }

        // Erase
        public void Erase()
        {
            SetPlayer(null);
            Text = "";
            Image = null;
        }

        // Check if it is an allowed spot
        public bool IsAllowedFor(int color, bool showMsg = false)
        {
            bool isAllowed = true;

            if (color == 2)
            {
                isAllowed = true;
            }

            if (!isAllowed && showMsg)
            {
                // Msg here
            }

            return isAllowed;
        }
    }

    public class Subset
    {
        private int headRow, headColumn, direction, color;
        private int[] items;
        private int own = 0, opponent = 0, empty = 0;

        public int HeadRow { get => headRow; set => headRow = value; }
        public int HeadColumn { get => headColumn; set => headColumn = value; }
        public int Direction { get => direction; set => direction = value; }
        public int Color { get => color; set => color = value; }
        public int OwnCount { get => own; }
        public int OpponentCount { get => opponent; }
        public int EmptyCount { get => empty; }
        public int Length { get => items.Length; }
        public int[] Items
        {
            get => items;
            set
            {
                items = value;
                own = empty = opponent = 0;

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] == color)
                    {
                        own++;
                    }
                    else if (items[i] == 0)
                    {
                        empty++;
                    }
                    else
                    {
                        opponent++;
                    }
                }
            }
        }

        public int RowOf(int index)
        {
            int v = (direction != SubsetDirection.Horizontal) ? 1 : 0;
            int h = (direction != SubsetDirection.Vertical) ? ((direction == SubsetDirection.ReverseDiagonal) ? -1 : 1) : 0;
            int row = 0;

            row = HeadRow + (v * index);

            return row;
        }

        public int ColumnOf(int index)
        {
            int v = (direction != SubsetDirection.Horizontal) ? 1 : 0;
            int h = (direction != SubsetDirection.Vertical) ? ((direction == SubsetDirection.ReverseDiagonal) ? -1 : 1) : 0;
            int col = 0;

            col = HeadColumn + (h * index);

            return col;
        }

        public int FirstEmpty(int rowCol)
        {
            int foundIndex = -1;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == 0)
                {
                    foundIndex = i;
                    break;
                }
            }

            return rowCol == 0 ? RowOf(foundIndex) : ColumnOf(foundIndex);
        }

        public int LastEmpty(int rowCol)
        {
            int foundIndex = -1;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == 0) foundIndex = i;
            }

            return rowCol == 0 ? RowOf(foundIndex) : ColumnOf(foundIndex);
        }

        public void Set(int headRow, int headCol, int direction, int color, int[] items)
        {
            HeadRow = headRow;
            HeadColumn = headCol;
            Direction = direction;
            Color = color;
            Items = items;
        }
    }
    
    public class Player
    {
        private int playerId = 0, type = 0, color = 0, turnCount = 0;
        private string displayName = "";
        private Ai ai = null;

        public int PlayerId { get => playerId; set => playerId = value; }
        public int Type { get => type; set => type = value; }
        public int Color { get => color; set => color = value; }
        public int TurnCount { get => turnCount; set => turnCount = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public Ai Ai { get => ai; set => ai = value; }


        public void PutStone(Stone value)
        {
            value.Owner = playerId;
            value.Color = color;
        }
    }


    /***** Lookup Classes *****/
    public class StoneImage
    {
        private static Image black = Properties.Resources.ImgStoneBlack;
        private static Image white = Properties.Resources.ImgStoneWhite;
        public static Image Black { get => black; set => black = value; }
        public static Image White { get => white; set => white = value; }

        public static Image FromColor(int value)
        {
            Image image = null;
            switch (value)
            {
                case 1:
                    image = Black;
                    break;
                case 2:
                    image = White;
                    break;
            }
            return image;
        }
    }

    public class PlayerType
    {
        public static int Local { get => 0; }
        public static int Remote { get => 1; }
        public static int Ai { get => 2; }
    }

    public class PlayMode
    {
        public static int AI { get => 0; }
        public static int Network { get => 1; }
        public static int One { get => 2; }
    }

    public class AiBehavior
    {
        public static int Random { get => 0; }
        public static int Aggressive { get => 1; }
        public static int Defensive { get => 2; }
    }

    public class SubsetType
    {
        public static int HalfOpen { get => 1; }
        public static int FourInFive { get => 2; }
        public static int ThreeInSix { get => 4; }
        public static int ThreeInFive { get => 6; }
        public static int TwoInFive { get => 8; }
        public static int OneInFive { get => 10; }
    }

    public class SubsetDirection
    {
        public static int Horizontal { get => 0; }
        public static int Vertical { get => 1; }
        public static int Diagonal { get => 2; }
        public static int ReverseDiagonal { get => 3; }
    }

}
