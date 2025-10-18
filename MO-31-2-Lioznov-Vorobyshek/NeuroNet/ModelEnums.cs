namespace MO_31_2_Lioznov_Vorobyshek.NeuroNet
{
    enum MemoryMode // режимы работы памяти 
    {
        GET, // считывание памяти
        SET, // сохранение памяти
        INIT // инициализация 
    }
    enum NeuronType // тип нейрона
    {
        Hidden, // скрытый
        Output // выходной
    }
    enum NetworkMode //
    {
        Train, // обучение 
        Test, // проверка
        Demo // распознавание
    }
    internal class ModelEnums
    {
    }
}
