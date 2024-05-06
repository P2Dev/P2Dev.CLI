using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace P2Dev.CLI
{
    public delegate Task<int> CommandDelegate(List<string> cmdParams);

    public class Cmd
    {
        public Dictionary<string, Cmd> SubCmds { get; set; } = new Dictionary<string, Cmd>();

        public CommandDelegate Command;

        public bool prettyOutput { get; set; } = false;
        public string actingAs { get; set; } = null;

        public void PrintResults(object obj)
        {
            Console.WriteLine(JsonConvert.SerializeObject(obj, prettyOutput ? Formatting.Indented : Formatting.None));
        }

        public async Task<int> RunCmd(List<string> cmdSegments, List<string> cmdParams)
        {
            //global params check here
            prettyOutput = HasParamFlag("pretty", cmdParams, false);

            if (cmdSegments.Count > 0)
            {
                if (SubCmds.ContainsKey(cmdSegments[0]))
                {
                    string cmdSegment = cmdSegments[0];
                    cmdSegments.RemoveAt(0);

                    return await SubCmds[cmdSegment].RunCmd(cmdSegments, cmdParams);
                }
                else
                {
                    //Missing command
                    Console.WriteLine(cmdSegments[0] + " not found");
                    return -1;
                }
            }
            else
            {
                //run cmd
                if (Command != null)
                    return await Command(cmdParams);
            }

            return 0;
        }

        static public string GetParamValue(string param, List<string> cmdParams, string defaultValue = null)
        {
            for (int a = 0; a < cmdParams.Count; a++)
            {
                if (cmdParams[a].Equals(param))
                {
                    if (a < cmdParams.Count - 1)
                    {
                        return cmdParams[a + 1];
                    }
                    else
                    {
                        return defaultValue;
                    }
                }
            }

            return defaultValue;
        }

        static public int GetParamValueInt(string param, List<string> cmdParams, int defaultValue = -1)
        {
            return Int32.Parse(GetParamValue(param, cmdParams, defaultValue.ToString()));
        }

        static public bool HasParamFlag(string param, List<string> cmdParams, bool defaultValue = false)
        {
            for (int a = 0; a < cmdParams.Count; a++)
            {
                if (cmdParams[a].Equals(param))
                    return true;
            }

            return defaultValue;
        }
    }
}
