namespace TrisGame
{
    public partial class Form1 : Form
    {
        private string giocatore1;
        private string giocatore2;
        private string turnoAttuale; // Tiene traccia di chi gioca ("X" o "O")
        private Button[] celle; // Array per i bottoni della griglia
        private int vittorieG1 = 0;
        private int vittorieG2 = 0;

        public Form1()
        {
            InitializeComponent();
            turnoAttuale = "X"; // Inizia sempre il giocatore 1
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Inizializza l'array di bottoni
            celle = new Button[] { btnUPsx, btnUPct, btnUPdx, btnMIDsx, btnMIDct, btnMIDdx, btnLOWsx, btnLOWct, btnLOWdx };

            // Assegna un handler (o gestore degli eventi)  a tutti i bottoni della griglia
            foreach (var btn in celle)
            {
                btn.Click += Cella_Click;
            }

            // Resetta le label delle celle e dei risultati
            ResetGriglia();
        }

        private void btnCONFERMA_Click(object sender, EventArgs e)
        {
            giocatore1 = textBoxGiocatore1.Text;
            giocatore2 = textBoxGiocatore2.Text;

            if (string.IsNullOrEmpty(giocatore1) || string.IsNullOrEmpty(giocatore2))
            {
                // Mostra errore se i nomi non sono inseriti
                MessageBox.Show("Inserisci i nomi di entrambi i giocatori.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Aggiorna le label con i nomi dei giocatori
            label_stampaG1.Text = giocatore1;
            label_stampaG2.Text = giocatore2;

            // Avvio del gioco
            label_G1vitt.Text = giocatore1;
            label_G2vitt.Text = giocatore2;

            // Avvia il gioco
            ResetGriglia();
        }

        private void Cella_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null || btn.Text != "") return; // Controlla se la conversione è fallita ed evita di sovrascrivere una cella già scelta

            btn.Text = turnoAttuale;

            if (CheckVincitore())
            {
                // Aggiungi vittoria al giocatore
                if (turnoAttuale == "X")
                {
                    vittorieG1++;
                    label_nvittorieG1.Text = vittorieG1.ToString();
                }
                else
                {
                    vittorieG2++;
                    label_nvittorieG2.Text = vittorieG2.ToString();
                }

                ResetGriglia();
                InvertiTurno(); // Inverti il turno dopo ogni round
                return;
            }

            if (CheckPareggio())
            {
                ResetGriglia();
                InvertiTurno(); // Inverti il turno in caso di pareggio
                return;
            }

            InvertiTurno();


        }

        private void InvertiTurno()
        {
            // Inverte il turno, se il giocatore 1 è X, diventa O, e viceversa
            if (turnoAttuale == "X")
            {
                turnoAttuale = "O";
            }
            else
            {
                turnoAttuale = "X";
            }

        }

        private bool CheckVincitore()
        {
            // Controlla tutte le combinazioni vincenti
            var combinazioniVincenti = new int[][]
            {
                new int[] { 0, 1, 2 },
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 }
            };

            foreach (var combinazione in combinazioniVincenti)
            {
                if (celle[combinazione[0]].Text == turnoAttuale &&
                    celle[combinazione[1]].Text == turnoAttuale &&
                    celle[combinazione[2]].Text == turnoAttuale)
                {
                    //Se tutte e tre le celle contengono lo stesso simbolo (turnoAttuale), significa che il giocatore ha completato una combinazione vincente.
                    return true;
                }
            }
            return false;
        }

        private bool CheckPareggio()
        {
            // Controlla se tutte le celle sono piene
            foreach (var btn in celle)
            {
                if (btn.Text == "") return false;
            }
            return true;
        }

        private void ResetGriglia()
        {
            // Resetta il testo di tutte le celle
            foreach (var btn in celle)
            {
                btn.Text = "";
                btn.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Eventuale codice da eseguire al caricamento della form
        }
    }
}
