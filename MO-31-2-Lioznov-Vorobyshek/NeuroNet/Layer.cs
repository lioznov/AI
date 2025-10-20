using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using MO_31_2_Lioznov_Vorobyshek.NeuroNet;


namespace MO_31_2_Lioznov_Vorobyshek.NeuroNet
{
    abstract class Layer
    {
        //Поля
        protected string name_Layer; //название слоя
        string pathDirWeights; //путь к каталогу, где находится файл синаптических весов
        string pathFileWeights; //путь к файлу саниптическов весов
        protected int numofneurons; //число нейронов текущего слоя
        protected int numofprevneurons; //число нейронов предыдущего слоя
        protected const double learningrate = 0.060; //скорость обучения
        protected const double momentum = 0.050d; //момент инерции
        protected double[,] lastdeltaweights; //веса предыдущей итерации
        protected Neuron[] neurons; //массив нейронов текущего слоя

        //Свойства
        public Neuron[] Neurons { get => neurons; set => neurons = value; }
        public double[] Data //Передача входных сигналов на нейроны слоя и авктиватор
        {
            set
            {
                for (int i = 0; i < numofneurons; i++)
                {
                    Neurons[i].Activator(value);
                }
            }
        }

        //Конструктор
        protected Layer(int non, int nopn, NeuronType nt, string nm_Layer)
        {
            numofneurons = non; //количество нейронов текущего слоя
            numofprevneurons = nopn; //количество нейронов предыдущего слоя
            Neurons = new Neuron[non]; //определение массива нейронов
            name_Layer = nm_Layer; //наиминование слоя
            pathDirWeights = AppDomain.CurrentDomain.BaseDirectory + "memory\\";
            pathFileWeights = pathDirWeights + name_Layer + "_memory.csv";

            double[,] Weights; //временный массив синаптических весов
            lastdeltaweights = new double[non, nopn + 1];

            if (File.Exists(pathFileWeights)) //определяет существует ли pathFileWeights
                Weights = WeightInitialize(MemoryMode.GET, pathFileWeights); //считывает данные из файла
            else
            {
                Directory.CreateDirectory(pathDirWeights);
                Weights = WeightInitialize(MemoryMode.INIT, pathFileWeights);
            }

            for (int i = 0; i < non; i++) //цикл формирования нейронов слоя и заполнения
            {
                double[] tmp_weights = new double[nopn + 1];
                for (int j = 0; j < nopn; j++)
                {
                    tmp_weights[j] = Weights[i, j];
                }
                Neurons[i] = new Neuron(tmp_weights, nt); //заполнение массива нейронами
            }
        }


        //Метод работы с массивом синаптических весов слоя
        public double[,] WeightInitialize(MemoryMode mm, string path)
        {
            char[] delim = new char[] { ';', ' ' };
            string tmpStr;
            string[] tmpStrWeights;
            double[,] weights = new double[numofneurons, numofprevneurons + 1];


            switch (mm)
            {
                case MemoryMode.GET:
                    tmpStrWeights = File.ReadAllLines(path); //читаем все строки файла
                    string[] memory_element; //временный массив, хранящий веса одного нейрона в виде строк
                    for (int i = 0; i < numofneurons; i++)
                    {
                        memory_element = tmpStrWeights[i].Split(delim);
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            weights[i, j] = double.Parse(memory_element[j].Replace(',', '.'),
                                System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    break;


                case MemoryMode.SET:
                    tmpStr = "";
                    for (int i = 0; i < numofneurons; i++)
                    {
                        string[] tmpRow = new string[numofprevneurons + 1];
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            tmpRow[j] = weights[i, j].ToString(System.Globalization.CultureInfo.InvariantCulture);
                        }
                        tmpStr += string.Join(";", tmpRow) + "\n";
                    }
                    File.WriteAllText(path, tmpStr);
                    break;


                case MemoryMode.INIT:
                    Random random = new Random();
                    for (int i = 0; i < numofneurons; i++)
                    {
                        double sum = 0.0;
                        double squaredSum = 0.0;

                        //Генерация весов
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            weights[i, j] = random.NextDouble() * 2.0 - 1.0;
                            sum += weights[i, j];
                            squaredSum += weights[i, j] * weights[i, j];
                        }

                        //Вычисляем среднее и дисперсию
                        double mean = sum / (numofprevneurons + 1);
                        double variance = (squaredSum / (numofprevneurons + 1)) - (mean * mean);
                        double root = Math.Sqrt(variance);

                        //Нормализуем веса
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            weights[i, j] = (weights[i, j] - mean) / root;
                        }
                    }

                    //Сохранение весов
                    tmpStr = "";
                    for (int i = 0; i < numofneurons; i++)
                    {
                        string[] tmpRow = new string[numofprevneurons + 1];
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            tmpRow[j] = weights[i, j].ToString(System.Globalization.CultureInfo.InvariantCulture);
                        }
                        tmpStr += string.Join(";", tmpRow) + "\n";
                    }
                    File.WriteAllText(path, tmpStr);
                    break;
            }
            return weights;
        }
        abstract public void Recognize(Network net, Layer nextLayer); //для прямых проходов
        abstract public double[] BackwardPass(double[] stuff); //и обратных
    }
}


//как генерируется синаптические веса
// Синаптические веса должны быть случайными значениями от -1 до +1
// У каждого нейрона синаптические веса и порог, среднее мат ожидание должно быть = 0
// Среднее квадратичное отклонение должно быть = 1
