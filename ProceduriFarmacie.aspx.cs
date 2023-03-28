using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ZedGraph;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Management_Farmacie
{
    public partial class ProceduriFarmacie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sqlCreate = "CREATE PROCEDURE getMedicamente(@stoc INT,  @rowCount INT OUT) AS " +
                "SELECT * FROM Medicamente WHERE Stoc >= @stoc SET @rowCount = @@ROWCOUNT";
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["farmacieCS"].ToString();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = System.Data.CommandType.Text;
                try
                {
                    sqlCommand.CommandText = "DROP PROCEDURE getMedicamente";
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }

                sqlCommand.CommandText = sqlCreate;
                sqlCommand.ExecuteNonQuery();
                Label1.Text = "Procedura creata cu succes!";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["farmacieCS"].ToString();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("getMedicamente", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@stoc", System.Data.SqlDbType.Int);
            sqlCommand.Parameters[0].Direction = System.Data.ParameterDirection.Input;
            sqlCommand.Parameters[0].Value = TextBox1.Text;
            sqlCommand.Parameters.Add("@rowCount", System.Data.SqlDbType.Int);
            sqlCommand.Parameters[1].Direction = System.Data.ParameterDirection.Output;

            using (SqlDataReader dr = sqlCommand.ExecuteReader())
            {
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }

            int rowCount = (int)sqlCommand.Parameters["@rowCount"].Value;
            TextBox2.Text = rowCount.ToString();

            connection.Close();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["farmacieCS"].ToString();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("TopVanzari", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RowCount", 5);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                GridView2.DataSource = dataSet.Tables[0];
                GridView2.DataBind();
                decimal totalValue = 0;
                foreach (DataRow row in dataSet.Tables[0].Rows) 
                {
                    totalValue += Convert.ToDecimal(row["valoare"]);
                }
                TextBox3.Text = totalValue.ToString();

            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            
                string connString = System.Configuration.ConfigurationManager.ConnectionStrings["farmacieCS"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("GetTopVanzariCursor", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter outputParameter = new SqlParameter();
                    outputParameter.ParameterName = "@nume";
                    outputParameter.Direction = ParameterDirection.Output;
                    outputParameter.SqlDbType = SqlDbType.VarChar;
                    outputParameter.Size = 50;
                    command.Parameters.Add(outputParameter);

                    connection.Open();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, 0, 5, "Vanzari"); 

                    decimal totalValue = Convert.ToDecimal(dataSet.Tables["Vanzari"].Rows[0]["valoare"]);
                    string numeClient = Convert.ToString(command.Parameters["@nume"].Value);

                    GridView3.PageIndex = 0;
                    GridView3.PageSize = 5; 
                    GridView3.DataSource = dataSet.Tables["Vanzari"];
                    GridView3.DataBind();

                    TextBox4.Text = "Valoarea maxima a fost vanduta clientului " + numeClient + ": " + totalValue.ToString();
                }
            


        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["farmacieCS"].ToString();
            using (SqlConnection sqlconn = new SqlConnection(conn))
            {

                Response.Redirect("Graphic.aspx?tip=" + this.DropDownList1.SelectedItem.Text);
            }
        }
    }
}

    