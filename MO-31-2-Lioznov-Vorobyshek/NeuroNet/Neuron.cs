using System.Security.Policy;
using MO_31_2_Lioznov_Vorobyshek.NeuroNet;
using static System.Math;

namespace MO_31_2_Lioznov_Vorobyshek.NeuroNet
{
    class Neuron
    {
        //поля
        private NeuronType type;        //тип нейрона
        private double[] weights;       //его веса
        private double[] inputs;        //его входы
        private double output;          //его выход
        private double derivative;      //производная

        //константы для функции активации 
        private double a = 0.01d;

        //свойства 
        public double[] Weights { get => weights; set => weights = value; }
        public double[] Inputs { get => inputs; set => inputs = value; }
        public double Output { get => output; }
        public double Derivative { get => derivative; }

        //конструктор 
        public Neuron(double[] memoryWeights, NeuronType typeNeuron)
        {
            this.type = typeNeuron;
            this.weights = memoryWeights;
        }

        //метод активации нейрона
        public void Activator(double[] i)
        {
            inputs = i;
            double sum = weights[0];
            for (int j = 0; j < inputs.Length; j++)
            {
                sum += inputs[j] * weights[j + 1];
            }

            switch (type)
            {
                case NeuronType.Hidden:
                    output = LeakyReLU(sum);
                    derivative = LeakyReLU_Derivativator(sum);
                    break;
                case NeuronType.Output:
                    output = Exp(sum);
                    break;
            }


        }
        // Функция активации LeakyReLU
        private double LeakyReLU(double sum)
        {
            return sum > 0 ? sum : a * sum;
        }

        // Производная функции LeakyReLU
        private double LeakyReLU_Derivativator(double sum)
        {
            return sum > 0 ? 1.0 : a;
        }



    }
}
