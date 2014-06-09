using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Core;


namespace Yahtzee_IIA.Models
{
    [Table]
    public class Score
    {
        #region Fields

        private long _id;
        private String _player1;
        private String _player2;
        private String _player3;
        private String _player4;
        private int _score1;
        private int _score2;
        private int _score3;
        private int _score4;

        #endregion

        #region Properties
        
        [Column(IsPrimaryKey = true, DbType = "BigInt NOT NULL IDENTITY", CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        //[Column(DbType = "Int NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Column(DbType = "NVarChar(140) NOT NULL", CanBeNull = false)]
        public String Player1
        {
            get { return _player1; }
            set { _player1 = value; }
        }

        [Column(DbType = "NVarChar(140) NOT NULL", CanBeNull = false)]
        public String Player2
        {
            get { return _player2; }
            set { _player2 = value; }
        }

        [Column(DbType = "NVarChar(140)", CanBeNull = true)]
        public String Player3
        {
            get { return _player3; }
            set { _player3 = value; }
        }

        [Column(DbType = "NVarChar(140)", CanBeNull = true)]
        public String Player4
        {
            get { return _player4; }
            set { _player4 = value; }
        }

        [Column(DbType = "Int NOT NULL", CanBeNull = false)]
        public int Score1
        {
            get { return _score1; }
            set { _score1 = value; }
        }

        [Column(DbType = "Int NOT NULL", CanBeNull = false)]
        public int Score2
        {
            get { return _score2; }
            set { _score2 = value; }
        }

        [Column(DbType = "Int", CanBeNull = true)]
        public int Score3
        {
            get { return _score3; }
            set { _score3 = value; }
        }
      
        [Column(DbType = "Int", CanBeNull = true)]
        public int Score4
        {
            get { return _score4; }
            set { _score4 = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aPlayers"></param>
        public Score(Player[] aPlayers, int nbPlayer)
        {
            Player1 = aPlayers[0].Name;
            Score1 = aPlayers[0].GrandTotal;

            Player2 = aPlayers[1].Name;
            Score2 = aPlayers[1].GrandTotal;

            if (nbPlayer > 2)
            {
                Player3 = aPlayers[2].Name;
                Score3 = aPlayers[2].GrandTotal;
            }
            else
            {
                Player3 = "";
                Score3 = 0;
            }


            if (nbPlayer > 3)
            {
                Player4 = aPlayers[3].Name;
                Score4 = aPlayers[3].GrandTotal;
            }
            else
            {
                Player4 = "";
                Score4 = 0;
            }
          
        }

        public Score()
        {
            
            Player1 = null;
            Score1 = 0;

            Player2 = null;
            Score2 = 0;

            Player3 = null;
            Score3 = 0;

            Player4 = null;
            Score4 = 0;
       
        }


        #endregion
    }
}
