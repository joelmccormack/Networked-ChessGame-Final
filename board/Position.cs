namespace ChessGame
{
    public struct Position
    {
        private int x;
        private int y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        internal int GetY()
        {
            return y;
        }
        internal int GetX()
        {
            return x;
        }

        internal Position AddX(int v)
        {
            return new Position(this.x + v, y);
        }

        internal Position AddY(int v)
        {

            return new Position(this.x , y + v);
        }

        public string GetString()
        {
            return "X=" + x + " y=" + y ;
        }
    }
}