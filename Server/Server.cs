using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener listener = new(IPAddress.Any, 12345);
listener.Start();
Console.WriteLine("====== CENTRAL DE MONITOREO ENERGÉTICO INICIADA ======");

while (true)
{

    Console.WriteLine("\n[ESPERANDO CONEXIÓN DE MEDIDOR...]");
    using var client = await listener.AcceptTcpClientAsync();
    using var stream = client.GetStream();

    Console.WriteLine($"[CONECTADO] Dispositivo desde: {client.Client.RemoteEndPoint}");

    var buffer = new byte[1024];
    int bytesRead;
    try
    {
        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
            var rawData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // Lógica de procesamiento: Separamos los datos por el caracter '|'
            var partes = rawData.Split('|');
            if (partes.Length == 3)
            {
                string id = partes[0].Split(':')[1];
                double volt = double.Parse(partes[1].Split(':')[1]);
                double amp = double.Parse(partes[2].Split(':')[1]);

                Console.WriteLine($"[LECTURA] ID: {id} | V: {volt}V | A: {amp}A | Potencia: {volt * amp}W");

                // Simulación de regla de negocio
                if (volt > 240) Console.ForegroundColor = ConsoleColor.Red;
                if (volt > 240) Console.WriteLine("¡ALERTA: SOBRETENSIÓN DETECTADA!");
                Console.ResetColor();
            }
        }
    }
    catch
    {
        Console.WriteLine("[ERROR] Conexión perdida abruptamente.");
    }
}