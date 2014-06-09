using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee_IIA.Models
{
    /// <summary>
    /// Classe utilisée pour la base de données locale
    /// </summary>
    public class YahtzeeDataContext : DataContext
    {
        public static string DBConnectionString = "Data source=isostore:/Yahtzee.sdf";
        private static YahtzeeDataContext _Instance;
        private static object _InstanceLocker;

        /// <summary>
        /// Obtient l'insance unique du contexte pour accéder à la base de données
        /// </summary>
        public static YahtzeeDataContext Instance
        {
            get
            {
                lock (_InstanceLocker)
                {
                    if (_Instance == null)
                    {
                        //Initialiser l'instance unique
                        Initialize();
                    }
                    return _Instance;
                }
            }
        }

        /// <summary>
        ///     Constructeur statique. Est appelé lors de l'initialisation de la première instance ou au premier accès
        ///     à une propriété ou une méthode statique. Permet entre autre l'initialisation des champs statiques.
        /// </summary>
        static YahtzeeDataContext()
        {
            _InstanceLocker = new object();
        }

        private YahtzeeDataContext(string connection, bool wipe = false)
            : base(connection)
        {
            if (wipe && this.DatabaseExists())
            {
                //Vide les tables
                /*this.Questions.DeleteAllOnSubmit(this.Questions);
                this.Users.DeleteAllOnSubmit(this.Users);
                this.SubmitChanges();*/
            }
            if (!this.DatabaseExists())
            {
                this.CreateDatabase();
            }
            this.SubmitChanges(); // Permet de sauvegarder et / ou de forcer l'ouverture de la connexion
        }

        /// <summary>
        /// Initialise la base de données
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="wipe"></param>
        public static void Initialize(string connection = "", bool wipe = false)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "Data source='isostore:/Yahtzee.sdf'";
            }
            _Instance = new YahtzeeDataContext(connection, wipe);
        }

        /// <summary>
        /// Obtient la table des Scores
        /// </summary>
        public Table<Score> Score
        {
            get { return GetTable<Score>(); }
        }
    }
}
