using System.Reflection;
using NeuralLib;
namespace Seno;

static class Program
{
    static void Main ()
    {
        //  Rede Neural
        NeuralNetwork neural = new ();

        //  Funções de ativação utilizadas.
        IActivateFunction linear = new NeuralLib.Activate.Linear(decimal.One, 0);
        IActivateFunction relu = new NeuralLib.Activate.ReLU();
        IActivateFunction tanh = new NeuralLib.Activate.Tanh(decimal.One, decimal.One);

        //  Neurônios de percepção.
        neural[0] = new (0, linear);
        neural[0].Bias = 0;

        //  Neurônios ocultos.
        int u = 0;
        for(int i = 0; i < 400; i++)
        {
            u = i + 1;
            neural[u] = new(u, tanh);
        }

        //  Neurônio de saída.
        u++;
        neural[u] = new(u, tanh);

        Random rnd = new Random();

        foreach(Neuron receiver in neural.Elements.FindAll(x => x.Id <= 200 && x.Id > 0))
        {
            double porc = rnd.NextDouble();
            decimal sign = porc > 0.5 ? decimal.One : decimal.MinusOne;
            neural.Connections.Add(new Connection<Neuron>(neural[0], receiver, Math.Round((decimal)rnd.NextDouble() * (sign / 10000), 20)));
            foreach(Neuron receiver_2 in neural.Elements.FindAll(x => x.Id <= 400 && x.Id > 200))
            {
                porc = rnd.NextDouble();
                sign = porc > 0.5 ? decimal.One : decimal.MinusOne;
                neural.Connections.Add(new Connection<Neuron>(receiver, receiver_2, Math.Round((decimal) rnd.NextDouble() * (sign / 10000), 20)));
            }
        }

        foreach (Neuron receiver in neural.Elements.FindAll(x => x.Id <= 400 && x.Id > 200))
        {
            double porc = rnd.NextDouble();
            decimal sign = porc > 0.5 ? decimal.One : decimal.MinusOne;
            neural.Connections.Add(new Connection<Neuron>(receiver, neural[401], Math.Round((decimal)rnd.NextDouble() * (sign / 10000), 20)));
        }

        //  Treina a rede
        int n = 10000;

        for (int i = 0; i <= n; i++)
        {

            decimal angle = 0;
            do
            {
                angle = (decimal)rnd.NextDouble() * 180;
            } while (angle > 180 || angle < 0); 

            decimal[] input = { angle };
            decimal expected = (decimal)Math.Sin((Math.PI * (double) angle) / 180.0);

            //  Aloca os valores nos neurônios de entrada.
            neural[0].Receive(input[0]);
            neural[0].Activate();

            //  Faz a propagação do valor.
            Neuron[] senders = { neural[0] };
            neural.Pulse(senders).Wait();

            List<Neuron> toSend = new();

            //  Ativa a primeira camada oculta.
            foreach (Neuron sender in neural.Elements.FindAll(x => x.Id <= 200 && x.Id > 0))
            {
                sender.Activate();
                toSend.Add(sender);
            }
            neural.Pulse(toSend.ToArray()).Wait();
            toSend.Clear();

            //  Ativa a segunda camada oculta.
            foreach (Neuron sender in neural.Elements.FindAll(x => x.Id <= 400 && x.Id > 200))
            {
                sender.Activate();
                toSend.Add(sender);
            }
            neural.Pulse(toSend.ToArray()).Wait();
            toSend.Clear();

            //  Ativa o neurônio de saída.
            neural[401].Activate();

            //  Calcula o erro no neurônio de saída.
            decimal gradient = neural[401].ActivateFunction.GetOutputGradient(neural[401].Output, expected);
            Console.WriteLine("Output (t = {0}): {1}; Expected: {2}; Gradiente = {3}", i, neural[401].Output, expected, gradient);

            decimal next_error = decimal.Zero;
            bool bias_edited = false;

            foreach (Connection<Neuron> connection in neural.Connections.FindAll(x => x.Receiver == neural[401]))
            {
                next_error += gradient * connection.Weight;
                connection.Weight += (decimal)0.01 * gradient * connection.Sender.Output;
                if (!bias_edited) { connection.Receiver.Bias += (decimal)0.0001 * gradient; bias_edited = true; }
            }

            bias_edited = false;

            decimal next_next_error = decimal.Zero;

            foreach (Connection<Neuron> connection in neural.Connections.FindAll(x => x.Receiver.Id > 0 && x.Receiver.Id <= 200))
            {
                gradient = connection.Receiver.ActivateFunction.GetGradient(next_error, connection.Receiver.Output);
                next_next_error += gradient * connection.Weight;
                connection.Weight += (decimal)0.01 * gradient * connection.Sender.Output;
                if (!bias_edited) { connection.Receiver.Bias += (decimal)0.0001 * gradient; bias_edited = true; }
            }

            bias_edited = false;

            foreach (Connection<Neuron> connection in neural.Connections.FindAll(x => x.Receiver.Id > 200 && x.Receiver.Id <= 400))
            {
                gradient = connection.Receiver.ActivateFunction.GetGradient(next_next_error, connection.Receiver.Output);
                connection.Weight += (decimal)0.01 * gradient * connection.Sender.Output;
                if (!bias_edited) { connection.Receiver.Bias += (decimal)0.0001 * gradient; bias_edited = true; }
            }

            /*
            Console.WriteLine("Error C2: {0}", next_error);

            //  Altera os pesos para os neurônios [8-12]
            foreach (Connection<Neuron> connection in neural.Connections.FindAll(x => x.Receiver.Id > 0 && x.Receiver.Id <= 200))
            {
                gradient = connection.Receiver.ActivateFunction.GetGradient(next_error, connection.Receiver.Output);
                if (connection.Receiver.Id <= 10) next_next_error += gradient * connection.Weight;
                connection.Weight += (decimal)0.001 * gradient * connection.Sender.Output;
                if (!bias_edited) { connection.Receiver.Bias += (decimal)0.00001 * gradient; bias_edited = true; }
            }

            bias_edited = false;

            //  Altera os pesos para os neurônios [3-7]
            foreach (Connection<Neuron> connection in neural.Connections.FindAll(x => x.Receiver.Id > 200 && x.Receiver.Id <= 400))
            {
                gradient = connection.Receiver.ActivateFunction.GetGradient(next_next_error, connection.Receiver.Output);
                connection.Weight += (decimal)0.001 * gradient * connection.Sender.Output;
                if (!bias_edited) { connection.Receiver.Bias += (decimal)0.00001 * gradient; bias_edited = true; }
            }
            
            Console.WriteLine("Error C1: {0}\n", next_next_error);
            */
            //  Atualiza a rede, limpando as variáveis.
            neural.Refresh();
        }


        //  Alguns testes:
        decimal error = decimal.Zero;
        for(int i = 0; i <= 100; i++)
        {
            //  Monta a matriz de dados de entrada.
            decimal[] input = { (180 / 100) * i };
            decimal expected = (decimal)Math.Sin((Math.PI * (180/100) * i) / 180);

            //  Aloca os valores nos neurônios de entrada.
            neural[0].Receive(input[0]);
            neural[0].Activate();

            //  Faz a propagação do valor.
            Neuron[] senders = { neural[0] };
            neural.Pulse(senders).Wait();

            List<Neuron> toSend = new();

            //  Ativa a primeira camada oculta.
            foreach (Neuron sender in neural.Elements.FindAll(x => x.Id <= 200 && x.Id > 0))
            {
                sender.Activate();
                toSend.Add(sender);
            }
            neural.Pulse(toSend.ToArray()).Wait();
            toSend.Clear();

            //  Ativa a segunda camada oculta.
            foreach (Neuron sender in neural.Elements.FindAll(x => x.Id <= 400 && x.Id > 200))
            {
                sender.Activate();
                toSend.Add(sender);
            }
            neural.Pulse(toSend.ToArray()).Wait();
            toSend.Clear();

            //  Ativa o neurônio de saída.
            neural[401].Activate();

            //  Exibe a saída.
            error += Math.Abs(neural[401].Output - expected);
            Console.WriteLine("Output (t = {0}): {1}; Expected: {2}; Erro = {3}", i, neural[401].Output, expected, Math.Abs(neural[401].Output - expected));

            neural.Refresh();
        }
        Console.WriteLine("Erro acumulado: {0}", error);
    }
}