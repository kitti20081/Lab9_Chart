using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab9_Chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {
            // 1. Считываем значения, которые пользователь ввел в текстовые поля
            double Xmin = double.Parse(textBoxXmin.Text);
            double Xmax = double.Parse(textBoxXmax.Text);
            double Step = double.Parse(textBoxStep.Text);
            double b = 2.5; // Константа для Варианта 7

            // 2. Создаем динамические списки для точек
            List<double> xValues = new List<double>();
            List<double> y1Values = new List<double>(); // Вариант 7
            List<double> y2Values = new List<double>(); // Синус

            // 3. Расчет точек в цикле
            for (double x = Xmin; x <= (Xmax + Step / 2); x += Step)
            {
                double radicand = Math.Pow(x, 3) + Math.Pow(b, 3);

                if (radicand >= 0)
                {
                    // Формула варианта 7: y = 9 * (x + 15 * sqrt(x^3 + b^3))
                    double y1 = 9 * (x + 15 * Math.Sqrt(radicand));

                    // Вторая произвольная функция (синус с масштабированием)
                    double y2 = Math.Sin(x) * 50;

                    xValues.Add(x);
                    y1Values.Add(y1);
                    y2Values.Add(y2);
                }
            }

            // 4. Полная очистка и пересоздание коллекций
            chart1.Series.Clear();

            // Создаем первую линию (Вариант 7)
            Series series1 = new Series("Вариант 7");
            series1.ChartType = SeriesChartType.Spline;
            series1.BorderWidth = 3;
            series1.Color = Color.Blue;

            // Создаем вторую линию (Синус)
            Series series2 = new Series("Синус");
            series2.ChartType = SeriesChartType.Spline;
            series2.BorderWidth = 3;
            series2.Color = Color.Orange;

            chart1.Series.Add(series1);
            chart1.Series.Add(series2);

            // 5. Размечаем оси динамически (без лишних индексов)
            chart1.ChartAreas[0].AxisX.Minimum = Xmin;
            chart1.ChartAreas[0].AxisX.Maximum = Xmax;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = Step;

            // 6. Передаем таблицы значений напрямую в объекты серий
            series1.Points.DataBindXY(xValues, y1Values);
            series2.Points.DataBindXY(xValues, y2Values);
        }
    }
}
