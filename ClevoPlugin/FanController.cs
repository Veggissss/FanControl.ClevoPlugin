using System;
using System.IO;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;

namespace FanControl.DellPlugin
{
    public class FanController
    {
        readonly Process _serverProcess;

        public FanController()
        {
            // Start the 32-bit server process
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Server\\ControlServer.exe",
                UseShellExecute = false
            };

            // Allow time for server start
            _serverProcess = Process.Start(startInfo);

            // Wait for server start
            System.Threading.Thread.Sleep(500);

            Console.WriteLine("Fan Control Server started.");
        }

        public string Communicate(params string[] lines)
        {
            Console.WriteLine($"Sending {lines.Length} lines to pipe...");

            using (var pipeClient = new NamedPipeClientStream(".", "FanControlPipe", PipeDirection.InOut))
            {
                pipeClient.Connect();

                var writer = new StreamWriter(pipeClient, Encoding.UTF8);
                var reader = new StreamReader(pipeClient, Encoding.UTF8);

                // Send each line individually
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                    writer.Flush();
                }

                // Read response
                return reader.ReadLine();
            }
        }

        public void SetFanSpeed(int fanNr, double fanSpeedPercentage)
        {
            var response = Communicate("SetFanSpeed", fanNr.ToString(), fanSpeedPercentage.ToString());
            Console.WriteLine(response);
        }

        public void SetFansAuto(int fanNr)
        {
            var response = Communicate("SetFanSpeedAuto", fanNr.ToString());
            Console.WriteLine(response);
        }

        public void Dispose()
        {
            // Close server app
            _serverProcess.Close();
        }
    }
}
