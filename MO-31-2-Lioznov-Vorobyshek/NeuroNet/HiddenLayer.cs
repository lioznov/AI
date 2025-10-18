using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MO_31_2_Lioznov_Vorobyshek.NeuroNet
{
    class HiddenLayer : Layer
    {
        public HiddenLayer(int non, int nopn, NeuronType nt, string type) : base(non, nopn, nt, type)
        {

        }
        // прямой ход
        public override void Recognize(Network net, Layer nextLayer)
        {
            double[] hidden_out = new double[numofneurons];
            for (int i = 0; i < numofneurons; i++)
                hidden_out[i] = neurons[i].Output;

            nextLayer.Data = hidden_out; // передача выходного сигнала на вход следуюшего
        }

        // обратный проход

        public override double[] BackwardPass(double[] gr_sums)
        {
            double[] gr_sum = new double[numofprevneurons];
            for (int j = 0; j < numofprevneurons; j++) // цикл вычисления градиентной суммы
            {
                double sum = 0;
                for(int k=0;k<numofneurons;k++)
                    sum += neurons[k].Weights[j] * neurons[k].Derivative * gr_sums[k];

                gr_sum[j] = sum;
                
            }
            for (int i=0;i<numofneurons;i++) //цикл синаптических весоыв
                for(int n=0;n<numofprevneurons+1;n++)
                {
                    double deltaw;
                    if (n == 0) // если порог
                        deltaw = momentum * lastdeltaweights[i, 0] + learningrate * neurons[i].Derivative * gr_sums[i];
                    else
                        deltaw = momentum * lastdeltaweights[i, n] + learningrate * neurons[i].Inputs[n - 1] * neurons[i].Derivative * gr_sums[i];

                    lastdeltaweights[i,n] = deltaw;
                    neurons[i].Weights[n] += deltaw;
                }
            return gr_sum;
        }
    }
}
