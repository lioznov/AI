namespace MO_31_2_Lioznov_Vorobyshek.NeuroNet
{
    enum MemoryMode //режим работы памяти
    {
        GET, //считывание памяти
        SET, //сохранение
        INIT //инициализация
    }
    enum NeuronType //тип нейрона
    {
        Hidden, //скрытый нейрон
        Output //выходной нейрон
    }

    enum NetworkMode
    {
        Train, //обученние
        Test, //тренировка
        Demo //распознавание
    }
}
