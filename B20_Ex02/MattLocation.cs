namespace B20_Ex02
{
    public class MattLocation
    {
        private int m_Row;
        private int m_Col;

        public MattLocation(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }

        public int Col
        {
            get
            {
                return m_Col;
            }

            set
            {
                m_Col = value;
            }
        }

        public static bool operator ==(MattLocation cell1, MattLocation cell2)
        {
            return cell1.Row == cell2.Row && cell1.Col == cell2.Col;
        }

        public static bool operator !=(MattLocation cell1, MattLocation cell2)
        {
            return cell1.Row != cell2.Row && cell1.Col != cell2.Col;
        }
    }
}
