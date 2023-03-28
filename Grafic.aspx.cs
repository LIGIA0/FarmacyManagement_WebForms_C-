using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ZedGraph;

namespace Management_Farmacie
{
    public partial class Grafic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ZedGraphWeb1.RenderGraph += ZedGraphWeb1_RenderGraph;
        }

        private void ZedGraphWeb1_RenderGraph(ZedGraph.Web.ZedGraphWeb webObject, System.Drawing.Graphics g, ZedGraph.MasterPane pane)
        {
            {
                string conn = System.Configuration.ConfigurationManager.ConnectionStrings["farmacieCS"].ToString();
                using (SqlConnection sqlconn = new SqlConnection(conn))
                {
                    sqlconn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT YEAR(data_eliberarii) AS year, COUNT(*) AS total FROM Retete GROUP BY YEAR(data_eliberarii)", sqlconn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<double> dataList = new List<double>();
                    List<string> labelList = new List<string>();

                    while (reader.Read())
                    {
                        labelList.Add(reader["year"].ToString());
                        dataList.Add(Convert.ToDouble(reader["total"]));
                    }

                    reader.Close();
                    sqlconn.Close();

                    ZedGraph.MasterPane masterPane = new ZedGraph.MasterPane();
                    ZedGraph.GraphPane myPane = pane[0];
                    myPane.Title.Text = "Retete per An";
                    myPane.XAxis.Title.Text = "Anul Eliberarii";
                    myPane.YAxis.Title.Text = "Total Retete";

                    ZedGraph.BarItem bar = myPane.AddBar("Total Retete", null, dataList.ToArray(), Color.Blue);
                    bar.Bar.Fill = new ZedGraph.Fill(Color.Blue, Color.White, Color.Blue);

                    myPane.XAxis.Type = ZedGraph.AxisType.Text;
                    myPane.XAxis.Scale.TextLabels = labelList.ToArray();
                    myPane.Chart.Fill = new ZedGraph.Fill(Color.White, Color.LightGray, 45.0f);
               
                    masterPane.AxisChange(g);
                    masterPane.Draw(g);
                }
            }




        }



    }
}
