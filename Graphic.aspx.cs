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
    public partial class Graphic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ZedGraphWeb1.RenderGraph += this.ZedGraphWeb1_RenderGraph;
        }
      

        public void ZedGraphWeb1_RenderGraph(ZedGraph.Web.ZedGraphWeb webObject, System.Drawing.Graphics g, ZedGraph.MasterPane pane)
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
                            decimal[] values = new decimal[5];
                            string[] labels = new string[5];
                            for (int i = 0; i < 5; i++)
                            {
                                DataRow row = dataSet.Tables[0].Rows[i];
                                values[i] = Convert.ToDecimal(row["valoare"]);
                                labels[i] = GetClientName(row["id_client"].ToString());
                            }
                            ZedGraph.GraphPane myPane = pane[0];
                            myPane.Title.Text = "Top 5 Vanzari";
                            myPane.Legend.IsVisible = false;
                            myPane.XAxis.Title.Text = "Nume client";
                            myPane.YAxis.Title.Text = "Valoare vanzari";
                            myPane.Fill = new ZedGraph.Fill(System.Drawing.Color.White);
                            ZedGraph.PieItem myPie = myPane.AddPieSlice(Convert.ToDouble(values[0]), System.Drawing.Color.Blue, 0f, labels[0]);
                            myPie.LabelType = ZedGraph.PieLabelType.Name_Value_Percent;
                            for (int i = 1; i < 5; i++)
                            {
                                myPie = myPane.AddPieSlice(Convert.ToDouble(values[i]), GetColor(i), 0f, labels[i]);
                                myPie.LabelType = ZedGraph.PieLabelType.Name_Value_Percent;
                            }
                            myPane.AxisChange(g);
               
            }

            }

            private string GetClientName(string clientId)
            {
                string connString = System.Configuration.ConfigurationManager.ConnectionStrings["farmacieCS"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("SELECT nume FROM clienti WHERE id_client = @id", connection);
                    command.Parameters.AddWithValue("@id", clientId);
                    connection.Open();
                    return command.ExecuteScalar().ToString();
                }
            }


            private static System.Drawing.Color GetColor(int index)
            {
                switch (index)
                {
                    case 0:
                        return System.Drawing.Color.Pink;
                    case 1:
                        return System.Drawing.Color.Turquoise;
                    case 2:
                        return System.Drawing.Color.Red;
                    case 3:
                        return System.Drawing.Color.Yellow;
                    case 4:
                        return System.Drawing.Color.Orange;
                    default:
                        return System.Drawing.Color.Gray;
                }
            }
       

    }
    }