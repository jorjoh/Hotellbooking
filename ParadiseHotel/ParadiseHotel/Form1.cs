using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ParadiseHotel
{
    public partial class Form1 : Form
    {
        // Lager objekt av label "gjest" på rot - nivå slik at den blir tilgjenlig i resten av prosjektet
        Label gjest = new Label();
       
        // Diverse tall varriabler etablert, og som brukes i prosjektet 
        int nr;
        int tildeltRom;
        int id;
        int antallDager;
        
        // String som inneholder informasjonen i kolonnen Navn fra databasen
        String guestName;
       
        // Oppretter arraylister for å håndtere informasjonen fra databasen
        ArrayList arrayListID = new ArrayList();
        ArrayList arrayListName = new ArrayList();
        ArrayList arrayListAntallDager = new ArrayList();
        ArrayList arrayListTildeltRom = new ArrayList();
        ArrayList arrayListRomNr = new ArrayList();

        // Instansierer string som senere brukes for å parse ut tallet fra en label
        String nummerParseingFralabel;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // SQL- spørring som henter all informasjon fra hotelguest
            string sql = "SELECT * FROM hotelguest";
            // Connectiong string som inneholder informasjon for å koble til databasen
            MySqlConnection con = new MySqlConnection("host=127.0.0.1;user=root;password=root;database=guest;");
            // Kommando objekt som inneholder SQL-spørringen og tilkoblingsinformasjonen til databasen
            MySqlCommand cmd = new MySqlCommand(sql, con);
            // Åpner databasetilkoblingen
            con.Open();
            // Sender SQL-koden til databaseserveren og lagrer resultatene i DataReaderen (reader-objektet)
            MySqlDataReader reader = cmd.ExecuteReader();
           
            // Leser / skriver ut alle veridene i sine respektive variabler så lenge datareaderen finner informasjon i databasen
            while (reader.Read())
            {
                id = (int)reader["ID"];
                guestName = (string)reader["Navn"];
                antallDager = (int)reader["AntallDager"];
                tildeltRom = (int)reader["TildeltRom"];

               // Legger informasjonen fra varriablenee ovenfor inn i sine respektive arraylister
                arrayListID.Add(id);
                arrayListName.Add(guestName);
                arrayListAntallDager.Add(antallDager);
                arrayListTildeltRom.Add(tildeltRom);
            }

            /* DEBUGGING FOR Å SJEKKE INFORMARSJONEN FRA DATABASEN ER RIKTIG
             * Console.WriteLine("--------- Bruker 1 ----------");
            Console.WriteLine(arrayListID[0]);
            Console.WriteLine(arrayListName[0]);
            Console.WriteLine(arrayListTildeltRom[0]);
            Console.WriteLine(arrayListAntallDager[0]);
            Console.WriteLine("------------ SLUTT PÅ BRUKER 1--------");
            */

            // Skjuler panelet som ligger i WYSIWYG panelet for å forhindre drag and drop effekt på et felt som ikke inneholder noe rom.
            panel1.Visible = false;

            // Tillater slipp på panel og listebox
            listBox1.AllowDrop = true;
            panel1.AllowDrop = true;


            int i, j;
            // Lager 12 panel som "rom"
            for (i = 1; i <= 3; i++)
            {
                for (j = 1; j <= 4; j++)
                {
                    // Instansierer rom som et panel objekt
                    Panel rom = new Panel();
                    // Definerer posisjonen til rom panelet
                    rom.Location = new Point(i * 200, j * 50);
                    // Setter bredden på panelet
                    rom.Width = 180;
                    // Setter høyden på panelet
                    rom.Height = 40;
                    // Gir panelet et name som kan identifisere rommet
                    rom.Name = "Rom " + (((i * 4) - 3) + (j - 1));
                    // Gir panelet en tekst som viser hvilket rom det er
                    rom.Text = "Rom " + (((i * 4) - 3) + (j - 1));
                    // Setter bakgrunnsfargen på panelet til grønn (ledig)
                    rom.BackColor = Color.Green;
                    // Tillater å slippe gjester på panelet
                    rom.AllowDrop = true;
                    // Setter opp hendelseshåndterere for DragOver og DragDrop
                    rom.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
                    rom.DragOver += new System.Windows.Forms.DragEventHandler(this.panel1_DragOver);
                    // Gjør panelene synlige i applikasjonen
                    rom.Visible = true;


                    // Instansierer gjest som en label
                    gjest = new Label();
                    // Setter posisjonen på labelen gjest
                    gjest.Location = new Point(10, 10);
                    // Setter bredden på labelen
                    gjest.Width = 180;
                    // Setter labelteksten til å vise rom sin text som inneholder rom nummer
                    gjest.Text = rom.Text;

                    // Legger romnummerne inn i en arrayliste
                    arrayListRomNr.Add(rom.Name);
                    // Legger til gjest labelen i rom sin controls
                    rom.Controls.Add(gjest);
                    // Legger til rommene til form
                    this.Controls.Add(rom);

                    // Finner tallverdier i gjest.Text
                    nummerParseingFralabel = Regex.Match(gjest.Text, @"\d+").Value;
                    // Legger resultatet fra nummerParseingFraLabel i nr-objektet som ble instansiert på toppen i prosjektet
                    nr = Int32.Parse(nummerParseingFralabel);

                    // for-løkke som løper gjennom ArrayList som inneholde ID fra databasen 
                    for (int r = 0; r < arrayListID.Count; r++)
                    {
                        // If-sjekk som sjekker om den tildelte romverdien er lik nummeret på noen av rommene
                        if (arrayListTildeltRom[r].Equals(nr))
                        {
                            // Setter gjest.Text til personen som har fått tildelt det valgte rommet og hvor lenge den personen skal være der
                            gjest.Text = arrayListName[r] + " - " + arrayListAntallDager[r] + " dager"; 
                            // Setter bakgrunnsfargen på panelet til rød (opptatt)
                            rom.BackColor = Color.Red;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // Setter datasource til listeboxen
           listBox1.DataSource = hotelguestBindingSource;
           // Velger hvilken kolonne fra datasourcen som skal vises i listeboxen
           listBox1.DisplayMember = "navn";
           // Filtrerer bindingsourcen til å kun hente gjester som ikke har fått tildelt rom
           hotelguestBindingSource.Filter = "TildeltRom = 0";
           // Fyller tableadapteren med gjester som ikke har fått tildeltrom
           this.hotelguestTableAdapter.Fill(this.riktigDataSet.hotelguest);
        }

        
        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Sjekker om venstre museknapp holdes inne og flyttes på
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // I så fall instansieres DragDropEffects 
                DragDropEffects dropEffect = listBox1.DoDragDrop(listBox1.SelectedItem, DragDropEffects.Move);
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Instansierer en string med navnet på den valgte gjesten når venstre museknapp holdes nede
            String selectedValue = (listBox1.SelectedItem ?? "test").ToString();

            //listBox1.DoDragDrop(listBox1.SelectedItem, DragDropEffects.All); // Debugging for å teste om det virker

            // Tar verdien i stringen selectedValue og utfører DoDragDrop
            DoDragDrop(selectedValue, DragDropEffects.All);

        }

        private void panel1_DragOver(object sender, DragEventArgs e)
        {
            // Gir DragEventArgs copy som DragDropEffects
            e.Effect = DragDropEffects.Copy;
        }

        public void panel1_DragDrop(object sender, DragEventArgs e)
        {
            // Instansierer objektet drv av DataRowView klassen som inneholder klasse-verdiene til den valgte verdien når man drar en gjest og slipper gjesten på et rom
            DataRowView drv = (DataRowView) listBox1.SelectedItem;
            // Instansierer en string som tar vare på verdien navn på den valgte gjesten og gjør den om til en string / tekst
            String valueOfItem = drv["navn"].ToString();

            // Instansierer objekt av klassen Control som benytter seg av objekt sender som er en av verdiene i konstruktøren til metoden
            Control c = (Control) sender;

            // Finner tallverdier i c.Controls[0].Text og legger den i stringen nummerParseingFralabel
            nummerParseingFralabel = Regex.Match(c.Controls[0].Text, @"\d+").Value;
            // Legger tallet fra nummerParseingFralabel i heltalls-objektet romNr
            int romNr = Int32.Parse(nummerParseingFralabel);

            // Definerer c.Controls[0].Text til navnet på den valgte gjesten
            c.Controls[0].Text = valueOfItem;
            // Definerer c.Controls[0].Name til rommet gjesten får tildelt
            c.Controls[0].Name = gjest.Name;

            //Setter bakgrunnsfarge til rød når "drop" er fullført
            c.BackColor = Color.Red;

            // Hvis arraylisten med navn inneholder navnet på den valgte gjesten
            if (arrayListName.Contains(valueOfItem))
            {
                /* settes id til id'en som gjesten har i databasen. 
                 * Dette gjøres ved å finne posisjonen til gjestens navn i arraylisten som inneholder alle gjestenes navn. 
                 * denne posisjonen settes som ID fordi ID'en til gjesten har samme posisjon i arraylisten som inneholder ID'en til gjestene 
                 */
                id = arrayListName.IndexOf(valueOfItem);
                // Console.WriteLine(arrayListID[id]); // DEBUGGING FOR Å SJEKKE OM ID ER RIKTIG

                /* Definerer SQL-setningen som brukes for å oppdatere tabellen med hvilket rom gjesten har fått tildelt.
                 * Her oppdateres kolonnen TildeltRom med det rommet som gjesten blir sluppet på.
                 * Her bruker vi posisjonen til gjestens navn for å finne samme posisjon i arraylista over ID'ene for å finne den valgte gjestens ID.
                 */
                string sql = "UPDATE hotelguest SET TildeltRom =" + romNr + " WHERE id=" + arrayListID[id];
                // Connectiong string som inneholder informasjon for å koble til databasen
                MySqlConnection con = new MySqlConnection("host=127.0.0.1;user=root;password=root;database=guest;");
                // Kommando objekt som inneholder SQL-spørringen og tilkoblingsinformasjonen til databasen
                MySqlCommand cmd = new MySqlCommand(sql, con);
                // Åpner databasetilkoblingen
                con.Open();
                // Kjører SQL-koden til databaseserveren som ren SQL-kode ved hjelp av ExecuteNonQuery(); funksjonen
                cmd.ExecuteNonQuery();
                
                //Console.WriteLine(sql); //DEBUGGING FOR Å SE SQL-KODEN SOM SENDES TIL DATABASEN
            }
        } 

        private void button3_Click(object sender, EventArgs e)
        {
            // starter Chrome og fører deg til bestillingsskjema
            Process.Start("http://localhost:11722/Default");
        }
    }
}