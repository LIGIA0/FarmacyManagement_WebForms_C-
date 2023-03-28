using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Management_Farmacie
{
    public partial class Farmacie : System.Web.UI.Page
    {
        
        protected void btnInsert_Click(object sender, EventArgs e)
        {

            {
                int cod = 0;
                SqlConnection sqlConnection = new SqlConnection(GridViewDS.ConnectionString);
                sqlConnection.Open();

                SqlCommand sqlCommandGetColumnCount = new SqlCommand("SELECT COUNT(*) FROM MEDICAMENTE", sqlConnection);
                cod = (int)sqlCommandGetColumnCount.ExecuteScalar() + 2; 

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Medicamente(cod_medicament,id_furnizor, stoc, pret, denumire) VALUES" +
                    "(@cod_medicament,@id_furnizor, @stoc, @pret, @denumire)", sqlConnection);
                DropDownList ddFurnizor = (DropDownList)GridView1.FooterRow.FindControl("ddFurnizor");
                TextBox txtStoc = (TextBox)GridView1.FooterRow.FindControl("txtStoc");
                TextBox txtDen = (TextBox)GridView1.FooterRow.FindControl("txtDenumire");
                TextBox txtPret = (TextBox)GridView1.FooterRow.FindControl("txtPret");

                sqlCommand.Parameters.Add("cod_medicament", System.Data.SqlDbType.NVarChar);
                sqlCommand.Parameters.Add("id_furnizor", System.Data.SqlDbType.Int);
                sqlCommand.Parameters.Add("denumire", System.Data.SqlDbType.NVarChar);
                sqlCommand.Parameters.Add("pret", System.Data.SqlDbType.Float);
                sqlCommand.Parameters.Add("stoc", System.Data.SqlDbType.Int);

                sqlCommand.Parameters["cod_medicament"].Value = cod;
                sqlCommand.Parameters["id_furnizor"].Value = ddFurnizor.SelectedValue;
                sqlCommand.Parameters["denumire"].Value = txtDen.Text;

                // validare stocuri
                int stoc;
                if (int.TryParse(txtStoc.Text, out stoc) && stoc <= 10000)
                {
                    sqlCommand.Parameters["stoc"].Value = stoc;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "validation", "alert('Inputul pentru stoc este incorect. Introduceti o valoare intre 0 si 10000.');", true);
                    return;
                }

                // Validare pret input
                float pret;
                if (float.TryParse(txtPret.Text, out pret) && pret < 1000)
                {
                    sqlCommand.Parameters["pret"].Value = pret;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "validation", "alert('Inputul pentru pret este incorect. Introduceti o valoare mai mica 1000.');", true);
                    return;
                }

                // Validare denumire input
                if (txtDen.Text.Length >= 4 && txtDen.Text.Count(char.IsLetter) >= 4)
                {
                    sqlCommand.Parameters["denumire"].Value = txtDen.Text;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "validation", "alert('The name input is incorrect. Va rugam introduceti un string cu cel putin 4 caractere si continand cel putin 4 litere.');", true);
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
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(System.Configuration
                .ConfigurationManager.ConnectionStrings["farmacieCS"].ToString());
            conn.Open();

           
                SqlCommand command = new SqlCommand("SELECT * FROM Medicamente WHERE pret>@pret", conn);
                command.Parameters.AddWithValue("@pret", txtPret.Text);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();

                adapter.Fill(ds, "Medicamente");
                GridView2.DataSource = ds.Tables["Medicamente"];
                GridView2.DataBind();
            
        }

        

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(System.Configuration
                .ConfigurationManager.ConnectionStrings["farmacieCS"].ToString());
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT SUM(stoc) as TotalQuantity FROM Medicamente WHERE id_furnizor=@id_furnizor", conn);
            command.Parameters.AddWithValue("@id_furnizor", Convert.ToInt32(DropDownList2.SelectedItem.Value));
            SqlDataReader reader = command.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);
            GridView3.DataSource = dt;
            GridView3.DataBind();

            reader.Close();
            conn.Close();
        }
        protected void MyGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GridView1.DataBind();  
        }

    }
}


