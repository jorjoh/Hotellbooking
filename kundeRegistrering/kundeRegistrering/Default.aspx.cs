using System;
using System.Data;
using System.Web.UI;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace kundeRegistrering
{
    public partial class _Default : Page
    {
        // Instansierer objektet ds av DataSet klassen
        DataSet ds;
        // Instansierer objektet da av MySqlDataAdapter klassen 
        MySqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Connectiong string som inneholder informasjon for å koble til databasen
            String cs = "Host=localhost;Database=guest;User=root;Password=root";
            // Tilkoblingsobjekt som bruker cs til å koble til databasen
            MySqlConnection dbconn = new MySqlConnection(cs);
            // Åpner databasetilkoblingen
            dbconn.Open();
            // SQL-spørring for å hente ut all informasjon fra hotelguest tabellen
            String sql = "select * from hotelguest";
            // Kommando objekt som inneholder SQL-spørringen og tilkoblingsinformasjonen til databasen
            MySqlCommand cmd = new MySqlCommand(sql, dbconn);
            // Dataadapter objektet fyller dataadapteren med sql-commandoen (cmd)
            da = new MySqlDataAdapter(cmd);
            // Bygger commandoen basert på dataadpteren
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            // Definerer dataset
            ds = new DataSet("TEST");
            // Fyller datasettet med informasjonen fra tabellen
            da.Fill(ds, "hotelguest");

         }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Oppretter en ny rad
            DataRow dr = ds.Tables["hotelguest"].NewRow();
            // Fyller den nye raden med navnet på gjesten
            dr["Navn"] = TextBox1.Text;
            // Fyller videre inn raden med startdato / ankomstdato
            dr["StartDato"] = TextBox2.Text;
            // Fyller videre inn i raden med lengden på oppholdet
            dr["AntallDager"] = TextBox3.Text;
            
            //Legger til raden i tabellen
            ds.Tables["hotelguest"].Rows.Add(dr);
            //Oppdaterer datasettet
            da.Update(ds, "hotelguest");

            // Oppretter en messagebox som forteller at reservasjonen er registrert og gir reservasjonsid til kunden så kunden kan kansellere bestillingen sin
            DialogResult result = MessageBox.Show("Du har vellykket registrert din reservasjon! Ditt referansenummer er: " + (ds.Tables["hotelguest"].Rows.Count));
            // Hvis brukern trykker OK i messageboxen
            if (result == DialogResult.OK)
            {
                // Oppdater siden
                Response.Redirect(Request.RawUrl);
                // Kobler grid'en til datasource
                GridView1.DataSource = ds;
                // Binder griden til datasourcen, slik at tilstand fungerer fordi data alltid lastes.
                GridView1.DataBind();
            }

        }
        
        protected void Button2_Click(object sender, EventArgs e)
        {
            // Knapp for å slette sin reseravsjon
        }
    }
}