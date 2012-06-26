using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace cSharpbot
{
    class BomberBot
    {
        static void Main(string[] args)
        {
            new BomberBot();
        }

        byte[] inFromServer = new byte[1280];
        private Socket socketCliente = null;
        private Bot bot = null;

        private bool conectado = false;

        public BomberBot()
        {
            try
            {
                conectar("cSharpBot", "984198716");
                controlConexion();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private void conectar(String user, String token)
        {
            socketCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketCliente.Connect("localhost", 5000);

            socketCliente.Receive(inFromServer);
            String bienvenida = Encoding.UTF8.GetString(inFromServer);
            Console.WriteLine(bienvenida);

            byte[] msg = System.Text.Encoding.UTF8.GetBytes(user + "," + token);

            socketCliente.Send(msg, msg.Length, SocketFlags.None);
            conectado = true;
        }

        private void controlConexion()
        {
            byte[] response = new byte[512];
            while (conectado)
            {
                Console.WriteLine("turno");

                socketCliente.Receive(response);
                String serverMessage = Encoding.UTF8.GetString(response);
                Console.WriteLine(serverMessage);
                string[] message = Regex.Split(serverMessage, ";");

                if (message.Length == 0)
                    continue;

                if (message[0] == "EMPEZO")
                {
                    bot = new Bot(message[2][0]);
                    bot.updateMap(message[1]);
                }
                else if (message[0] == "TURNO")
                {
                    Console.WriteLine("turno: " + message[1]);
                    bot.updateMap(message[2]);
                    byte[] msg = System.Text.Encoding.UTF8.GetBytes(bot.move());
                    socketCliente.Send(msg, msg.Length, SocketFlags.None);
                }
                else if (message[0] == "PERDIO")
                {
                    Console.WriteLine("perdi :(");
                }
            }
        }
    }
}
