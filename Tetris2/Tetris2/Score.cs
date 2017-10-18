using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris2
{
    class Score
    {

        public int RemovedRow;
        int ScoreTime;
        int CountRemovedRows;
        double Combo = 0.10;

        Game1 game;

        void RemoveRemovedRow()
        {
            if (RemovedRow == 1 || RemovedRow == 2 || RemovedRow == 3)
            {
                ScoreTime++;
                if (ScoreTime == Game1.tablewidth * 3)
                {
                    CountRemovedRows = RemovedRow;
                    RemovedRow = 0;
                }
            }
        }

        public void score()
        {
            Combo = Combo * CountRemovedRows;

            if (CountRemovedRows == 1)
            {
                game.score += (10 * Combo);
            }                
        }
    }
}
