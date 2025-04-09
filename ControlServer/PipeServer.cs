using System.IO.Pipes;
using System.Text;

namespace FanControl.Server
{
    public class Server
    {
        private static readonly FanControl _fanControl = new();
        private static bool _isDisposed = false;

        static void Main(string[] args)
        {
            while (!_isDisposed) 
            {
                using var pipeServer = new NamedPipeServerStream("FanControlPipe", PipeDirection.InOut);
                Console.WriteLine("Waiting for connection...");
                pipeServer.WaitForConnection();

                // Handle the incoming request
                StreamReader reader = new (pipeServer, Encoding.UTF8);
                StreamWriter writer = new (pipeServer, Encoding.UTF8);

                string request = ReadLine(reader);
                Console.WriteLine("Received request: " + request);

                switch (request) 
                {
                    case "SetFanSpeed":
                        int fanNr = int.Parse(ReadLine(reader));

                        // Handle gobal number seperator '.' vs ','
                        double fanSpeedPercentage = double.Parse(ReadLine(reader), System.Globalization.CultureInfo.InvariantCulture);

                        SetFanSpeed(fanNr, fanSpeedPercentage);

                        WriteLine(writer, $"Fan nr {fanNr} speed set to {fanSpeedPercentage}%");
                        break;

                    case "SetFanSpeedAuto":
                        int autoFanNr = int.Parse(ReadLine(reader));
                        SetFansAuto(autoFanNr);

                        WriteLine(writer, $"Fan {autoFanNr} speed set to auto");
                        break;

                    case "Shutdown":
                        WriteLine(writer, "Shutting down server");
                        _isDisposed = true;
                        break;

                    default:
                        WriteLine(writer, "Unknown command");
                        break;
                }
            }
        }

        private static string ReadLine(StreamReader reader)
        {
            string? line = reader.ReadLine();
            if (line == null)
            {
                throw new EndOfStreamException("End of stream reached, line is null.");
            }
            return line;
        }

        private static void WriteLine(StreamWriter writer, string message)
        {
            writer.WriteLine(message);
            writer.Flush();
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
