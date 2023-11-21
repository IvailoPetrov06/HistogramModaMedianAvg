using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HistogramModaMedianAvg
{
    public partial class Form1 : Form
    {
        private List<double> numbers;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            numbers = textBox1.Text.Split(' ').Select(s => double.Parse(s)).ToList();

            var avg = numbers.Average();
            textBox2.Text = $"Average is: {avg}";

            numbers.Sort();
            double median = 0;
            if (numbers.Count % 2 == 1)
                median = numbers[(numbers.Count - 1) / 2];
            else
            {
                int index = numbers.Count / 2;
                median = (numbers[index] + numbers[index - 1]) / 2.0;
            }
            textBox3.Text = $"Median is: {median}";

            List<double> mode = FindMode(numbers);

            if (mode.Count == 0)
            {
                textBox4.Text = "No mode.";
            }
            else
            {
                textBox4.Text = $"Mode is: {string.Join(", ", mode)}";
            }

            DisplayHistogram(numbers);
        }

        private List<double> FindMode(List<double> numbers)
        {
            var roundedNumbers = numbers.Select(n => Math.Round(n));

            var groupedNumbers = roundedNumbers.GroupBy(x => x).OrderByDescending(g => g.Count());
            var maxFrequency = groupedNumbers.First().Count();

            if (maxFrequency == 1)
            {
                return new List<double>();
            }

            var modeValues = groupedNumbers.TakeWhile(g => g.Count() == maxFrequency).Select(g => g.Key);
            return modeValues.ToList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }
        private void DisplayHistogram(List<double> numbers)
        {
            chart1.Series.Clear();

            Series histogramSeries = new Series("Histogram");

            var binWidth = 1.0;
            var bins = numbers.GroupBy(x => Math.Floor(x / binWidth) * binWidth)
                              .OrderBy(g => g.Key)
                              .Select(g => new { Value = g.Key, Count = g.Count() });

            foreach (var bin in bins)
            {
                histogramSeries.Points.AddXY(bin.Value, bin.Count);
            }

            chart1.Series.Add(histogramSeries);

            chart1.ChartAreas[0].AxisX.Title = "Value";
            chart1.ChartAreas[0].AxisY.Title = "Frequency";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
