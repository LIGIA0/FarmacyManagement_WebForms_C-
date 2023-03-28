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
    public partial class FunctiiManagementFarmacie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            string sqlCreate = "CREATE FUNCTION dbo.CalculateTotalValue() RETURNS FLOAT AS BEGIN DECLARE @totalValue FLOAT = 0; SELECT @totalValue = @totalValue + (pret * stoc) FROM medicamente; RETURN @totalValue; END; ";
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["farmacieCS"].ToString();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = System.Data.CommandType.Text;
                try
                {
                    sqlCommand.CommandText = "DROP FUNCTION CalculateTotalValue";
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }

                sqlCommand.CommandText = sqlCreate;
                sqlCommand.ExecuteNonQuery();
                Label1.Text = "Functie creata cu succes!";
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["farmacieCS"].ToString();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT pret, stoc, denumire FROM medicamente", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    dataTable.Columns.Add(new DataColumn("total", typeof(double)));
                    foreach (DataRow row in dataTable.Rows)
                    {
                        double pret = Convert.ToDouble(row["pret"]);
                        double cantitate = Convert.ToDouble(row["stoc"]);
                        double total = pret * cantitate;
                        row["total"] = total;
                    }

                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
                else
                {
                }
            }

            
            string query = "SELECT dbo.CalculateTotalValue() AS TotalValue";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        float totalValue = Convert.ToSingle(result);
                        TextBox1.Text = totalValue.ToString();
                    }
                    else
                    {
                    }
                }
            }



        }

    }
    }
