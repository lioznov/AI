namespace MO_31_2_Lioznov_Vorobyshek.NeuroNet
{
    class Network
    {
        //все слои сети
        private InputLayer input_layer = null;
        private HiddenLayer hidden_layer1 = new HiddenLayer(70, 15, NeuronType.Hidden, nameof(hidden_layer1));
        private HiddenLayer hidden_layer2 = new HiddenLayer(70, 15, NeuronType.Hidden, nameof(hidden_layer2));
        private OutputLayer output_layer = new OutputLayer(70, 15, NeuronType.Hidden, nameof(output_layer));

        private double[] fact = new double[10];
        private double[] e_error_avr;

        //свойства

        public double[] Fact { get => fact; }
        public double[] E_error_avr { get => e_error_avr; set => e_error_avr = value; }

        public Network() { }
    }
}
