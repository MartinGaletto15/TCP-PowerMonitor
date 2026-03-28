using System.Net.Sockets;
using System.Text;

string ip = "127.0.0.1";
int port = 12345;

while (true) // Da la opcion a reconectarse
{
    try
    {
        Console.WriteLine("========== Client disponible ==========");

        using TcpClient client = new();
        await client.ConnectAsync(ip, port);
        using var NetworkStream = client.GetStream();

        Console.WriteLine("Conectado al servidor");

        // Aca se simula el envio de datos
        Random random = new();
        while (client.Connected)
        {
            // Generamos datos realistas
            double voltaje = 220 + (random.NextDouble() * 30); // 220V a 250V
            double amperios = 5 + random.NextDouble();

            string message = $"ID:1|V:{voltaje}|A:{amperios}";
            byte[] Data = Encoding.UTF8.GetBytes(message);

            NetworkStream.Write(Data, 0, Data.Length);
            Console.WriteLine($"Enviado: {message}");

            await Task.Delay(5000); // Simulamos 5 segundos de espera
        }

        Console.WriteLine("========== Client desconectado, esperando reconexion");
    }
    catch
    {
        Console.WriteLine("Error: Client no disponible");
        await Task.Delay(5000);
    }
}