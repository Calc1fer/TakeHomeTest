
using System;

/*Script containing function that aims to calculate the best possible move on a board. Only implementing the Calculate function,
 assuming a populated board with no matching jewels.*/
public class Board
{
    enum JewelKind
    {
        Empty,
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    }

    enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    struct Move
    {
        public int x;
        public int y;
        public MoveDirection direction;
    }

    int GetWidth()
    {
        return 0;
    }

    int GetHeight()
    {
        return 0;
    }

    JewelKind GetJewel(int x, int y)
    {
        return JewelKind.Empty;
    }
    
    
    Move CalculateBestMoveForBoard()
    {
        //Track the best move and score
        //Simulate the swap on the board and check for resulting matches as a result of the move.
        Move bestMove = new Move{x = -1, y = -1, direction = MoveDirection.Up};
        int bestScore = 0;
        
        //Iterate through the entire board from left to right
        for (int x = 0; x < GetWidth(); x++)
        {
            for (int y = 0; y < GetHeight(); y++)
            {
                //For each direction check we can swap
                foreach (MoveDirection dir in Enum.GetValues(typeof(MoveDirection)))
                {
                    //Params to store the current position of the move
                    int tempX = x;
                    int tempY = y;

                    //Alter the coordinate based on the direction
                    if (dir == MoveDirection.Up) tempY++;
                    else if (dir == MoveDirection.Down) tempY--;
                    else if (dir == MoveDirection.Left) tempX--;
                    else if (dir == MoveDirection.Right) tempX++;
                    
                    //Check if the direction is within the bounds of the board
                    if (tempX >= 0 && tempX < GetWidth() && tempY >= 0 && tempY < GetHeight())
                    {
                        JewelKind jewel = GetJewel(x, y);
                        JewelKind target = GetJewel(tempX, tempY);

                        int score = SwapJewels(x, y, jewel, target);
                        
                        //Update the bestscore if this is better
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = new Move { x = x, y = y, direction = dir};
                        }
                    }
                }
            }
        }

        return bestMove;
    }

    //Function will calculate the score based on swapping the jewels
    private int SwapJewels(int x, int y, JewelKind jewel, JewelKind target)
    {
        int score = 1; //Set to 1 to include the current jewel when checking for at least a 3 match
        
        //Swap the jewels around
        JewelKind tempJewel = jewel;
        jewel = target;
        target = tempJewel;
        
        //Calculate the score for the horizontal axis
        score += HorizontalScore(x, y);
        
        //Calculate the score for the vertical axis
        score += VerticalScore(x, y);
        
        //Swap the jewels back
        target = jewel;
        jewel = tempJewel;
        
        //If the score reaches the minimum of 3 matches then return the score
        if (score >= 3) return score;
        return 0;
    }

    //Function will check for the score along horizontal axis
    private int HorizontalScore(int x, int y)
    {
        int score = 0;

        //Count along the width of the board from the x+1 coordinate and count the score
        for (int tempX = x + 1; tempX < GetWidth(); tempX++)
        {
            if (GetJewel(tempX, y) == GetJewel(tempX - 1, y))
            {
                if(tempX != x) score++; //Do not count the current jewel twice, as it should be added in SwapJewels()
            }
            else break; //stop if no match
        }
        
        for (int tempX = x - 1; tempX >= 0; tempX--)
        {
            if (GetJewel(tempX, y) == GetJewel(tempX + 1, y))
            {
                if(tempX != x) score++;
            }
            else break;
        }
        
        return score;
    }
    
    //Function will check for the score along vertical axis
    private int VerticalScore(int x, int y)
    {
        int score = 0;
        
        //Count along the height of the board from the coordinate and count the score (as above so below)
        for (int tempY = y + 1; tempY < GetHeight(); tempY++)
        {
            if (GetJewel(x, tempY) == GetJewel(x, tempY - 1))
            {
                if(tempY != y) score++;
            }
            else break; 
        }

        for (int tempY = y - 1; tempY >= 0; tempY--)
        {
            if (GetJewel(x, tempY) == GetJewel(x, tempY + 1))
            {
                if(tempY != y) score++;
            }
        }
        return score;
    }
}
