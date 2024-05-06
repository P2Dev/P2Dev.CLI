# P2Dev.CLI Library

The P2Dev.CLI library is a flexible command-line interface (CLI) toolkit for .NET applications. It allows developers to easily create and manage command structures within their applications, including support for nested commands and asynchronous execution.

## Features

- **Command Handling**: Process commands and sub-commands asynchronously.
- **Parameter Parsing**: Automatically handle and parse command-line parameters.
- **Flexible Output**: Support for JSON output formatting, including pretty-printing.

## Installation

To use P2Dev.CLI in your project, include the necessary files in your project or build them into a library.

## Usage

Below is a simple example that demonstrates setting up a CLI client with a root command and a couple of subcommands:

```csharp
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
```

This example shows how to initialize the CLI client, set a root command, and define two subcommands. Each command prints a message to the console.
