using System.IO.Pipes;
using System.Text;

namespace FanControl.Server
{
    public class Server
    {
        private static readonly FanControl _fanControl = new();

        static void Main(string[] args)
        {
            while (true) 
            {
                using var pipeServer = new NamedPipeServerStream("FanControlPipe", PipeDirection.InOut);
                Console.WriteLine("Waiting for connection...");
                pipeServer.WaitForConnection();
                Console.WriteLine("Client request!");

                // Handle the incoming request
                StreamReader reader;
                StreamWriter writer;
                reader = new StreamReader(pipeServer, Encoding.UTF8);
                writer = new StreamWriter(pipeServer, Encoding.UTF8);
                
                var request = reader.ReadLine();
                Console.WriteLine("Received request: " + request);

                // Assuming the request is a function name or action to trigger
                if (request == "SetFanSpeed")
                {
                    string fanNrArg = reader.ReadLine();
                    Console.WriteLine(fanNrArg);
                    var fanNr = int.Parse(fanNrArg);

                    string fanSpeedArg = reader.ReadLine();
                    Console.WriteLine(fanSpeedArg);
                    var fanSpeedPercentage = double.Parse(fanSpeedArg, System.Globalization.CultureInfo.InvariantCulture);

                    SetFanSpeed(fanNr, fanSpeedPercentage);
                    writer.WriteLine("Fan speed set");
                    writer.Flush();
                }
                else if (request == "SetFanSpeedAuto")
                {
                    string fanNrArg = reader.ReadLine();
                    var fanNr = int.Parse(fanNrArg);
                    SetFansAuto(fanNr);
                    writer.WriteLine($"Fan {fanNr} speed set to auto");
                    writer.Flush();
                }
            }
        }

        private static void SetFanSpeed(int fanNr, double fanSpeedPercentage)
        {
            _fanControl.SetFanSpeed(fanNr, fanSpeedPercentage);
        }

        private static void SetFansAuto(int fanNr)
        {
            _fanControl.SetFansAuto(fanNr);
        }
    }
}
