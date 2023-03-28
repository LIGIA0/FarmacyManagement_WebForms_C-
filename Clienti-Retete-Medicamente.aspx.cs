using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Management_Farmacie
{
    public partial class Clienti_Retete_Medicamente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            {
                int id = 0;
                SqlConnection sqlConnection = new SqlConnection(GridViewDS.ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommandGetColumnCount = new SqlCommand("SELECT COUNT(*) FROM RETETE", sqlConnection);
                id = (int)sqlCommandGetColumnCount.ExecuteScalar() + 2;
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Retete(data_eliberarii, diagnostic, emitent, Id_client) VALUES" +
                    "(@data_eliberarii, @diagnostic, @emitent, @Id_client)", sqlConnection);

                
                 TextBox txtData = (TextBox)GridView1.FooterRow.FindControl("txtData");
                 TextBox txtDiagnostic = (TextBox)GridView1.FooterRow.FindControl("txtDiagnostic");
                 TextBox txtEmitent = (TextBox)GridView1.FooterRow.FindControl("txtEmitent");
                 DropDownList ddClient = (DropDownList)GridView1.FooterRow.FindControl("ddClient");

                 sqlCommand.Parameters.Add("data_eliberarii", System.Data.SqlDbType.Date);
                 sqlCommand.Parameters.Add("diagnostic", System.Data.SqlDbType.NVarChar, 50);
                 sqlCommand.Parameters.Add("emitent", System.Data.SqlDbType.NVarChar, 40);
                 sqlCommand.Parameters.Add("Id_client", System.Data.SqlDbType.Int);

                 sqlCommand.Parameters["Id_client"].Value = ddClient.SelectedValue;

                // Validare data_eliberarii input
                string data = txtData.Text.Trim();
                if (txtData.Text.Length != 10 || !DateTime.TryParseExact(data, "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data_eliberarii))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "validation", "alert('Inputul pentru data este incorect. Data trebuie sa fie in formatul \"dd.mm.yyyy\" si sa contina 10 caractere.');", true);
                    return;
                }
                sqlCommand.Parameters["data_eliberarii"].Value = data_eliberarii;

                // Validare diagnostic input
                string diagnostic = txtDiagnostic.Text;
                if (txtDiagnostic.Text.Length >= 5 && txtDiagnostic.Text.Length <= 50 && txtDiagnostic.Text.Count(char.IsDigit) <= 0)
                {
                    sqlCommand.Parameters["diagnostic"].Value = diagnostic;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "validation", "alert('Inputul pentru diagnostic este incorect. Nu se pot introduce cifre, iar lungimea trebuie sa fie intre 5 si 50 de caractere.');", true);
                    return;
                }

                // Validare emitent input
                string emitent = txtEmitent.Text;
                if (txtEmitent.Text.Length >= 5 && txtEmitent.Text.Length <= 50 && txtEmitent.Text.Count(char.IsDigit) <= 0)
                {
                    sqlCommand.Parameters["emitent"].Value = emitent;
                }
                else
                {
                     ScriptManager.RegisterStartupScript(this, GetType(), "validation", "alert('Inputul pentru emitent este incorect. Nu se pot introduce cifre, iar lungimea trebuie sa fie intre 8 si 50 de caractere.');", true);
                     return;
                }
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "validation", $"alert('An error occurred while inserting the data. {ex.Message}');", true);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["farmacieCS"].ToString();
            using (SqlConnection sqlconn = new SqlConnection(conn))
            {

                Response.Redirect("Grafic.aspx");
            }
        }
    }
}