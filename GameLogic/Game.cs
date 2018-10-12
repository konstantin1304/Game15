using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public delegate void DelegateVictory();

    public enum GameMode
    {
        Horizontal=0,
        Vertical=1,
        Mixed=2
    }
    public class Game
    {
        public event DelegateVictory Victory;
        private Random rnd = new Random();
        private int[,] _gameField = new int[4, 4];
        public int zeroX { get; private set; }
        public int zeroY { get; private set; }
        bool checkVertical { get; set; }
        bool checkHorizontal { get; set; }

        public int this[int x, int y]
        {
            get => _gameField[y, x];
        }



        public void zeroUp()
        {
            if (zeroY == 3) return;

            _gameField[zeroX, zeroY] = _gameField[zeroX, zeroY + 1];
            ++zeroY;
            _gameField[zeroX, zeroY] = 0;

        }
        public void zeroDown()
        {
            if (zeroY == 0) return;

            _gameField[zeroX, zeroY] = _gameField[zeroX, zeroY - 1];
            --zeroY;
            _gameField[zeroX, zeroY] = 0;
        }

        public void zeroLeft()
        {
            if (zeroX == 3) return;

            _gameField[zeroX, zeroY] = _gameField[zeroX + 1, zeroY];
            ++zeroX;
            _gameField[zeroX, zeroY] = 0;
        }
        public void zeroRight()
        {
            if (zeroX == 0) return;

            _gameField[zeroX, zeroY] = _gameField[zeroX - 1, zeroY];
            --zeroX;
            _gameField[zeroX, zeroY] = 0;
        }



        public Game()
        {
            
        }

        public void InitGame(GameMode mode)
        {
            bool isGenHorizontal = true;
            switch (mode)
            {
                case GameMode.Horizontal:
                    {
                        isGenHorizontal = true;
                        this.checkHorizontal = true;
                        this.checkVertical = false;
                    }
                    break;
                case GameMode.Vertical:
                    {
                        isGenHorizontal = false;
                        this.checkHorizontal = false;
                        this.checkVertical = true;
                    }
                    break;
                case GameMode.Mixed:
                    {
                        isGenHorizontal = DateTime.Now.Millisecond % 2 == 0;
                        this.checkHorizontal = true;
                        this.checkVertical = true;
                    }
                    break;

            }

            

            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };

            if (isGenHorizontal)
            {

                for (int n = 0, i = 0; i < 4; ++i)
                {
                    for (int j = 0; j < 4; ++j, ++n)
                    {
                        _gameField[j, i] = arr[n];
                        if (arr[n] == 0)
                        {
                            zeroX = j;
                            zeroY = i;
                        }
                    }
                }
                Shuffle(arr);
            }
            else
            {
                for (int n = 0, i = 0; i < 4; ++i)
                {
                    for (int j = 0; j < 4; ++j, ++n)
                    {
                        _gameField[i, j] = arr[n];
                        if (arr[n] == 0)
                        {
                            zeroX = j;
                            zeroY = i;
                        }
                    }
                }
                Shuffle(arr);
            }
        }
        /// <summary>
        /// Проверка поля на предмет выигрыша
        /// </summary>
        /// <returns></returns>
        public bool IsWin()
        {
            bool result = true;
            //Проверка горизонтального направления
            if (checkHorizontal)
            {
                for (int i = 0; i < 15; i++)
                {

                    if (_gameField[i % 4, i / 4] != (i + 1))
                    {
                         result = false;
                        break;
                    }
                }
                if (result)
                {
                    return true;
                }
            }
            //Проверка вертикального направления
            if (checkVertical)
            {

                result = true;
                for (int i = 0; i < 15; i++)
                {

                    if (_gameField[i / 4, i % 4] != (i + 1))
                    {
                        result = false;
                        break;
                    }

                }
            }
            return result;
        }
        public bool CheckAndGo(int value)
        {
            if (zeroX > 0 && _gameField[zeroX - 1, zeroY] == value)
            {
                zeroRight();
                return true;
            }
            if (zeroX < 3 && _gameField[zeroX + 1, zeroY] == value)
            {
                zeroLeft();
                return true;
            }
            if (zeroY > 0 && _gameField[zeroX, zeroY - 1] == value)
            {
                zeroDown();
                return true;
            }
            if (zeroY < 3 && _gameField[zeroX, zeroY + 1] == value)
            {
                zeroUp();
                return true;
            }

            return false;

        }

        private void Shuffle(int[] arr)
        {
            //Перемешивание
            Random rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                switch (rand.Next(4))
                {
                    case 0:
                        this.zeroDown();
                        break;
                    case 1:
                        this.zeroUp();
                        break;
                    case 2:
                        this.zeroRight();
                        break;
                    case 3:
                        this.zeroLeft();
                        break;
                }
            }

        }
    }
}
    
