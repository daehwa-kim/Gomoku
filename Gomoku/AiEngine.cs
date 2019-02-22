using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    public class Ai
    {
        private int level = 0, behavior = AiBehavior.Random;
        private bool debugMode = false;

        private Stone[,] stones;

        private List<Subset> subsets = new List<Subset>();
        public int Level { get => level; set => level = value; }
        public int Behavior { get => behavior; set => behavior = value; }
        public bool DebugMode { get => debugMode; set => debugMode = value; }

        public Stone[,] Stones { set => stones = value; }

        public Stone Pick(int color)
        {
            List<Stone> outputs = new List<Stone>();
            int highest = 0;

            Scan(color);

            // Get stones with the highest weight
            for (int row = 0; row < stones.GetLength(0); row++)
            {
                for (int col = 0; col < stones.GetLength(1); col++)
                {
                    if (stones[row, col].AiWeight > highest)
                    {
                        highest = stones[row, col].AiWeight;
                        outputs.Clear();
                        outputs.Add(stones[row, col]);
                    }
                    else if (stones[row, col].AiWeight == highest)
                    {
                        outputs.Add(stones[row, col]);
                    }
                }
            }

            if (outputs.Count > 0)
            {
                for (int i = outputs.Count - 1; i >= 0; i--)
                {
                    if (!outputs[i].IsAllowedFor(color) || !outputs[i].IsEmpty)
                    {
                        outputs.Remove(outputs[i]);
                    }
                }
            }

            int count = 0;
            foreach (Stone stone in stones)
            {
                if (stone.Color != 0)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                outputs.Clear();
                outputs.Add(stones[7, 7]);
            }

            ResetWeights();

            // In the case of more than two stones with the highest weight, pick one randomly.
            Random random = new Random();

            return (outputs.Count > 0) ? outputs[random.Next(outputs.Count)] : null;
        }
        private void Scan(int color)
        {
            subsets.Clear();

            // Find subsets in each spot
            for (int length = 2; length <= 6; length++)
            {
                for (int row = 0; row < stones.GetLength(0); row++)
                {
                    for (int col = 0; col < stones.GetLength(1); col++)
                    {
                        for (int direction = 0; direction <= 3; direction++)
                        {
                            ExamineSubset(row, col, length, direction, color);
                            ExamineSubset(row, col, length, direction, (color == 1 ? 2 : 1), true);
                        }
                    }
                }
            }

            if (debugMode)
            {
                foreach (Stone stone in stones)
                {
                    stone.Text = (stone.AiWeight > 0) ? stone.AiWeight.ToString() : "";
                }
            }
        }

        private void ExamineSubset(int headRow, int headCol, int length, int direction, int color, bool opponentSide = false)
        {
            int own = 0, opponent = 0, empty = 0;
            int v = (direction != SubsetDirection.Horizontal) ? 1 : 0;
            int h = (direction != SubsetDirection.Vertical) ? ((direction == SubsetDirection.ReverseDiagonal) ? -1 : 1) : 0;

            for (int i = 0; i < length; i++)
            {
                int row = headRow + (v * i);
                int col = headCol + (h * i);

                if (Stone.Exists(row, col) && stones[row, col].IsEmpty)
                {
                    empty++;
                }
                else if (Stone.Exists(row, col) && stones[row, col].Color == color)
                {
                    own++;
                }
                else
                {
                    opponent++;
                }
            }

            Subset found = new Subset();
            int[] items = new int[length];
            for (int i = 0; i < length; i++)
            {
                int row = headRow + (v * i);
                int col = headCol + (h * i);
                items[i] = (Stone.Exists(row, col)) ? stones[row, col].Color : -1;
            }
            found.Set(headRow, headCol, direction, color, items);

            int rowFirstEmpty = found.FirstEmpty(0);
            int colFirstEmpty = found.FirstEmpty(1);
            int rowLastEmpty = found.LastEmpty(0);
            int colLastEmpty = found.LastEmpty(1);
            int attackWeight = 1;
            int defenseWeight = 1;

            if (opponentSide)
            {
                defenseWeight = 2;
            }
            else
            {
                attackWeight = 2;
            }

            if (Stone.Exists(rowFirstEmpty, colFirstEmpty) && stones[rowFirstEmpty, colFirstEmpty].IsEmpty)
            {
                if (found.Length == 5 && found.OwnCount == 4 && found.EmptyCount == 1 && !opponentSide) // Force Attack
                {
                    stones[rowFirstEmpty, colFirstEmpty].AiWeight += 2000;
                    subsets.Add(found);
                }
                else if (found.Length == 5 && found.OwnCount == 4 && found.EmptyCount == 1 && opponentSide) // Force Defend
                {
                    stones[rowFirstEmpty, colFirstEmpty].AiWeight += 900;
                    subsets.Add(found);
                }
                else if (found.Length == 6 && found.OwnCount == 3 && found.EmptyCount == 1 &&
                    ((found.Items[0] != color && found.Items[0] != 0) && (found.Items[length - 1] != color && found.Items[length - 1] != 0)))
                {

                }
                else if (found.Length == 6 && found.OwnCount == 3 && found.EmptyCount == 2 &&
                    ((found.Items[0] != color && found.Items[0] != 0) ^ (found.Items[length - 1] != color && found.Items[length - 1] != 0)))
                {
                    if (found.Items[0] == 0 && found.Items[1] == 0)
                    {
                        stones[rowFirstEmpty, colFirstEmpty].AiWeight += 30 * attackWeight;
                        stones[found.RowOf(1), found.ColumnOf(1)].AiWeight += 30 * attackWeight;
                        subsets.Add(found);
                    }
                    else if (found.Items[length - 1] == 0 && found.Items[length - 2] == 0)
                    {
                        stones[rowLastEmpty, colLastEmpty].AiWeight += 30 * attackWeight;
                        stones[found.RowOf(length - 2), found.ColumnOf(length - 2)].AiWeight += 30 * attackWeight;
                        subsets.Add(found);
                    }
                    else if (found.Items[0] == 0)
                    {
                        stones[rowFirstEmpty, colFirstEmpty].AiWeight += 60 * attackWeight;
                        //stones[rowLastEmpty, colLastEmpty].AiWeight += 20 * attackWeight;
                        subsets.Add(found);
                    }
                    else if (found.Items[length - 1] == 0)
                    {
                        //stones[rowFirstEmpty, colFirstEmpty].AiWeight += 20 * attackWeight;
                        stones[rowLastEmpty, colLastEmpty].AiWeight += 60 * attackWeight;
                        subsets.Add(found);
                    }
                }
                else if (found.Length == 6 && found.OwnCount == 3 && found.EmptyCount == 3 &&
                    (found.Items[0] == 0 && found.Items[length - 1] == 0))
                {
                }
                else if (found.Length == 5 && found.OwnCount == 3 && found.EmptyCount == 2 &&
                    !(found.Items[0] == 0 && found.Items[1] == 0) && !(found.Items[length - 1] == 0 && found.Items[length - 2] == 0) &&
                    !(found.Items[1] == 0 && found.Items[2] == 0) && !(found.Items[2] == 0 && found.Items[3] == 0))
                {
                    if (found.Items[0] == 0 && found.Items[length - 1] == 0)
                    {
                        stones[rowFirstEmpty, colFirstEmpty].AiWeight += 60 * (attackWeight + 2);
                        stones[rowLastEmpty, colLastEmpty].AiWeight += 60 * (attackWeight + 2);
                    }
                    else
                    {
                        stones[rowFirstEmpty, colFirstEmpty].AiWeight += 60 * attackWeight;
                        stones[rowLastEmpty, colLastEmpty].AiWeight += 60 * attackWeight;
                    }

                    if (found.Items[2] == 0 && (found.Items[1] == 0 || found.Items[3] == 0)) // Level 1
                    {
                        stones[found.RowOf(2), found.ColumnOf(2)].AiWeight += 15 * attackWeight;
                    }

                    subsets.Add(found);
                }
                else if (found.Length == 5 && found.OwnCount == 3 && found.EmptyCount == 1 &&
                    ((found.Items[0] != color && found.Items[0] != 0) || (found.Items[length - 1] != color && found.Items[length - 1] != 0)))
                {
                    stones[rowFirstEmpty, colFirstEmpty].AiWeight += 1;
                    subsets.Add(found);
                }
                else if (found.Length == 5 && found.OwnCount == 2 && found.EmptyCount == 3)
                {
                    stones[rowFirstEmpty, colFirstEmpty].AiWeight += 3;
                    stones[rowLastEmpty, colLastEmpty].AiWeight += 3;
                    subsets.Add(found);
                }
                else if (found.Length == 5 && found.OwnCount == 1 && found.EmptyCount >= 3)
                {
                    stones[rowFirstEmpty, colFirstEmpty].AiWeight += 1;
                    stones[rowLastEmpty, colLastEmpty].AiWeight += 1;
                    subsets.Add(found);
                }
                else if (found.Length == 3 && found.OwnCount == 2)
                {
                    stones[rowFirstEmpty, colFirstEmpty].AiWeight += 1;
                }
                else if (found.Length == 2 && found.OwnCount == 1)
                {
                    stones[rowFirstEmpty, colFirstEmpty].AiWeight += 0;
                }
            }
        }

        private void ResetWeights()
        {

            for (int row = 0; row < stones.GetLength(0); row++)
            {
                for (int col = 0; col < stones.GetLength(1); col++)
                {
                    stones[row, col].AiWeight = 0;
                }
            }
        }
    }
}
