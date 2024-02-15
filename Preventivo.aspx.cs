using NomeProgetto.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Concessionaria
{
    public partial class Preventivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
                CaricaAuto();
                CaricaOptional();
            }
        }

        protected void CaricaAuto()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ID, Marca, Modello FROM Auto", connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    
                    List<Auto> autoList = new List<Auto>();

                    while (reader.Read())
                    {
                        Auto auto = new Auto
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Marca = reader["Marca"].ToString(),
                            Modello = reader["Modello"].ToString()
                        };

                        autoList.Add(auto);
                    }

                   
                    ddlAuto.DataSource = autoList;
                    ddlAuto.DataTextField = "MarcaModello";
                    ddlAuto.DataValueField = "ID";
                    ddlAuto.DataBind();

                    reader.Close();
                }
                catch (Exception ex)
                {
                  
                }
            }
        }


        protected void CaricaOptional()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ID, Nome, Prezzo FROM Optional", connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string nomeOptional = reader["Nome"].ToString();
                        decimal prezzoOptional = Convert.ToDecimal(reader["Prezzo"]); // Memorizza il prezzo come decimal
                        ListItem item = new ListItem(nomeOptional, prezzoOptional.ToString());
                        CheckBoxList1.Items.Add(item);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                  
                }
            }
        }


        protected void ddlAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int autoId = int.Parse(ddlAuto.SelectedValue);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT Marca, Modello, Prezzo, Immagine FROM Auto WHERE ID = @AutoID", connection);
                command.Parameters.AddWithValue("@AutoID", autoId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string prezzoString = reader["Prezzo"].ToString(); // Leggi il prezzo come stringa
                        decimal prezzoDecimale;

                        // Prova a convertire la stringa del prezzo in un numero decimale
                        if (decimal.TryParse(prezzoString, out prezzoDecimale))
                        {
                            lblPrezzoBase.Text = $"Prezzo base: €{prezzoDecimale:N2}"; // Formatta e visualizza il prezzo base
                            imgAuto.ImageUrl = reader["Immagine"].ToString();
                            imgAuto.Visible = true;
                            lblPrezzoBase.Visible = true;
                        }
                        else
                        {
                            // Gestisci il caso in cui la conversione non riesce
                            lblPrezzoBase.Text = "Prezzo non valido";
                            imgAuto.Visible = false;
                            lblPrezzoBase.Visible = false;
                        }
                    }


                    reader.Close();
                }
                catch (Exception ex)
                {
                  
                }
            }
        }


        protected void btnCalcolaPreventivo_Click(object sender, EventArgs e)
        {
            
            // Calcola il prezzo base dell'auto
            decimal prezzoBase = 0;
            if (decimal.TryParse(lblPrezzoBase.Text.Replace("Prezzo base: €", ""), out prezzoBase))
            {

                // Calcola il costo degli optional selezionati
                decimal costoOptional = 0;
                foreach (ListItem item in CheckBoxList1.Items)
                {
                    if (item.Selected)
                    {
                        decimal optionalPrice = Convert.ToDecimal(item.Value);
                        costoOptional += optionalPrice;
                    }
                }

                // Calcola il costo della garanzia
                decimal costoGaranzia = 0;
                if (ddlGaranzia.SelectedIndex > 0)
                {
                    int anniGaranzia = int.Parse(ddlGaranzia.SelectedValue);
                    costoGaranzia = anniGaranzia * 120;
                }

                // Calcola il totale complessivo del preventivo
                decimal totalePreventivo = prezzoBase + costoOptional + costoGaranzia;

                
                lblDettagliPreventivo.Text = $"Prezzo base dell'auto: €{prezzoBase}<br />";
                lblDettagliPreventivo.Text += $"Costo degli optional: €{costoOptional}<br />";
                lblDettagliPreventivo.Text += $"Costo della garanzia: €{costoGaranzia}<br />";
                lblDettagliPreventivo.Text += $"Totale complessivo: €{totalePreventivo}<br />";
                divPreventivo.Visible = true;
            }
            else
            {
                lblDettagliPreventivo.Text = "Impossibile calcolare il preventivo. Controlla che i dati siano corretti.";
                divPreventivo.Visible = true;
            }
        }







    }
}
