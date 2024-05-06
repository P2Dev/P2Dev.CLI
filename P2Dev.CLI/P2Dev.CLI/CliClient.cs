using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Dev.CLI
{
    public class CliClient
    {
        static CliClient _instance;

        public static CliClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CliClient();
                return _instance;
            }
        }

        public async Task<int> ProcessCommand(string[] args)
        {
            List<string> cmdSegments = new List<string>();
            List<string> cmdParams = new List<string>();

            bool paramsFlag = false;

            foreach (string arg in args)
            {
                if (arg.StartsWith("--"))
                    paramsFlag = true;

                if (paramsFlag)
                    cmdParams.Add(arg.Replace("--", ""));
                else
                    cmdSegments.Add(arg);
            }

            return await RootCmd.RunCmd(cmdSegments, cmdParams);
        }

        public Cmd RootCmd { get; set; }
    }
}
