using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee_IIA.Models
{
    class YahtzeeDataContext: DataContext
    {
        #region Fields
            private static string _Password;
            private static YahtzeeDataContext _Instance;
            private static object _InstanceLocker;
        #endregion

        #region Properties
            /// <summary>
            ///     Obtient ou définie le mot de passe utilisé pout chifrer la base de données
            /// </summary>
            public static string Password
            {
                get { return _Password; }
                set { _Password = value; }
            }

            public Table<Game> Game
            {
                get { return GetTable<Game>(); }
            }

            /// <summary>
            ///      Obtient l'instance unique du contexte pour accéder à la base de données. (singleton)
            /// </summary>
            public static YahtzeeDataContext Instance
            {
                get
                {
                    lock(_InstanceLocker)
                    {
                        if (_Instance == null)
                        {
                            Initialize();
                        }
                        return _Instance;
                    }
                }
                set { _InstanceLocker = value; }
            }
        #endregion

        #region Constructors

            /// <summary>
            ///     Constructeur statique. Est appelé lors de l'initialisation de la premiere instance ou au premier 
            ///     accès à une propriété ou une methode statique. Permet entre autre l'initialisation des champs statiques.
            /// </summary>
            static YahtzeeDataContext()
            {
                _InstanceLocker = new object();
            }

            /// <summary>
            /// Initialise une nouvelle instance de la classe WP.PasswordBox.Models.PasswordBoxDataContext
            /// </summary>
            /// <param name="connection">Chaine de connexion à utiliser</param>
            /// <param name="wipe">Détermine si la base de données existante doit être supprimée</param>
            private YahtzeeDataContext(string connection, bool wipe = false) : base(connection) // Constructeur de la class parent ( : base())
            {
                if (wipe && this.DatabaseExists())
                {
                    this.DeleteDatabase();
                }

                if (!this.DatabaseExists())
                {
                    this.CreateDatabase();
                }

                this.SubmitChanges(); // Permet de sauvegarder et / ou focer l'ouverture de la connexion.
            }
        #endregion

        #region Methods

            public static void Initialize(string connection = "", bool wipe = false)
            {
                if (_Instance != null)
                {
                    _Instance.Dispose(true); 
                    _Instance = null;
                }
                if (string.IsNullOrWhiteSpace(connection)) {
                    connection = "data Source='isostore:/YahtzeeDB.sdf';Password='" + Password + "'";
                }

                _Instance = new YahtzeeDataContext(connection, wipe);
            }
         #endregion
    }
}
