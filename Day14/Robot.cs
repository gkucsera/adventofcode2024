namespace Day14
{
    public class Robot
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int DiffX { get; }
        public int DiffY { get; }

        public Robot(int posX, int posY, int diffX, int diffY, int sizeX, int sizeY)
        {
            PosX = posX;
            PosY = posY;
            DiffX = diffX < 0 ? sizeX + diffX : diffX;
            DiffY = diffY < 0 ? sizeY + diffY : diffY;
        }

        public void Move(int sizeX, int sizeY, int moveCount)
        {
            PosX += DiffX * moveCount;
            PosX %= sizeX;
            PosY += DiffY * moveCount;
            PosY %= sizeY;
        }
    }
}