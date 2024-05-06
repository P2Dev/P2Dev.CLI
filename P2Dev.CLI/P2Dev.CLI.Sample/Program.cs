using P2Dev.CLI;

CliClient client = new CliClient();

client.RootCmd = new Cmd()
{
    Command = async (cmdParams) =>
    {
        Console.WriteLine("Hello World");
        return 0;
    },
    SubCmds = new Dictionary<string, Cmd>()
    {
        {
            "subcmd", new Cmd()
            {
                Command = async (cmdParams) =>
                {
                    Console.WriteLine("Hello SubCmd");
                    return 0;
                }
            }
        },
        {
            "subcmd2", new Cmd()
            {
                Command = async (cmdParams) =>
                {
                    Console.WriteLine("Hello SubCmd2");
                    return 0;
                }
            }
        }
    }
};

client.ProcessCommand(args).Wait();