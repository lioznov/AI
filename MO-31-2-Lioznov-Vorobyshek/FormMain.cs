using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MO_31_2_Lioznov_Vorobyshek.NeuroNet;

namespace MO_31_2_Lioznov_Vorobyshek
{
    public partial class FormMain : Form
    {
        // Добавляем поля слоёв
        private Layer hiddenLayer;
        private Layer outputLayer;
        private Network network;



        private double[] inputPixels;


        // Конструктор
        public FormMain()
        {
            InitializeComponent();
            inputPixels = new double[15];

            network = new Network();
        }
        // Обработчик событий
        private void Changing_State_Pixel_Button_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == Color.Black)
            {
                ((Button)sender).BackColor = Color.White;
                inputPixels[((Button)sender).TabIndex] = 1d;
            }
            else
            {
                ((Button)sender).BackColor = Color.Black;
                inputPixels[((Button)sender).TabIndex] = 0d;
            }
        }

        private void SaveTrainSample_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "train.txt";
            string tmpStr = numericUpDown_NecesseryOutput.Value.ToString();

            for (int i = 0; i < inputPixels.Length; i++)
            {
                tmpStr += " " + inputPixels[i].ToString();
            }
            tmpStr += "\n";

            File.AppendAllText(path, tmpStr);
        }

        private void SaveTestSample_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "test.txt";
            string tmpStr = numericUpDown_NecesseryOutput.Value.ToString();

            for (int i = 0; i < inputPixels.Length; i++)
            {
                tmpStr += " " + inputPixels[i].ToString();
            }
            tmpStr += "\n";

            File.AppendAllText(path, tmpStr);
        }

        private void Weights_Click(object sender, EventArgs e)
        {
            hiddenLayer = new NeuroNet.HiddenLayer(10, 15, NeuroNet.NeuronType.Hidden, "Hidden");
        }
    }
}