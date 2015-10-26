using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MaintenanceScheduling
{
    public partial class Visualization : Form
    {
        /// <summary>
        /// Constructor accepts the ProblemState object to display it in chart
        /// </summary>
        /// <param name="ps"></param>
        public Visualization( ProblemState ps)
        {
            InitializeComponent();
            DisplaySolution(ps);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Displays the solution in chart
        /// </summary>
        /// <param name="ps">solution state as input</param>
        private void DisplaySolution(ProblemState ps)
        {
            chart1.Visible = true;
            for (int i = 0; i < ps.NumberOfIntervals; i++)
            {
                chart1.Series.Add("Series" + i);
                chart1.Series["Series" + i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar;
                chart1.Series["Series" + i].ChartArea = "ChartArea1";
                chart1.Series["Series" + i]["DrawSideBySide"] = "false";
                chart1.Series["Series" + i].YValuesPerPoint = 2;
                chart1.Series["Series" + i].AxisLabel = (i + 1).ToString();

                for (int j = 0; j < ps.Length; j++)
                    chart1.Series["Series" + i].Points.AddXY(i,j,j+ps.Solution[j, i]);
            }

            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = ps.Length;
            chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

            chart1.ChartAreas["ChartArea1"].AxisY.Interval = 1;
            chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.IntervalOffset = -0.5;
            chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format="0";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "POWER UNITS";
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "INTERVALS";
        }
    }
}
