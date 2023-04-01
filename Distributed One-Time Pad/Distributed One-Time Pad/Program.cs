﻿
//https://www.nuget.org/packages/Microsoft.Extensions.Configuration.CommandLine/8.0.0-preview.2.23128.3
//program execution starts from here
using Microsoft.Extensions.Configuration;
using System.IO.Enumeration;
using OneTimePadDLL;
using System.Text.Json;
using System.Linq;
using System;
using System.Net.Security;




Console.WriteLine("Command line Arguments: {0}", args.Length);

IConfiguration config = new ConfigurationBuilder()
    .AddCommandLine(args)
    .Build();

switch (args[0])
{
    case "generate":
        {

            GenerateKey();
            break;
        }

    case "encrypt":
        {
            EncryptFile();
            break;
        }

    default:
        Console.WriteLine("Syntax is OTP Generate/encrypt/decrypt outputfile size series");
        break;

}

void EncryptFile()
{

    string Keyfile;
    string Outputfile;
    string InputFile;
    string message = string.Empty;

    if (String.IsNullOrEmpty(config["key"]))
    {
        Console.WriteLine("Use --key to designate a keyfile. No key? Use generate to generate a key");
        return;
    }
    else
    {
        Keyfile = Convert.ToString(config["key"]!);

    }

    if (String.IsNullOrEmpty(config["outputfile"]))
    {
        Outputfile = "encryptedMessage.txt";
    }
    else
    {
        //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings#possible-dereference-of-null
        Outputfile = Convert.ToString(config["outputfile"]!);
    }

    if (String.IsNullOrEmpty(config["inputfile"]))
    {
        //if there is no file, just assume that the last item in the array is the message.
        //TODO Fix this
        message = args[args.Length-1];
        InputFile = string.Empty;
    }
    else
    {
        //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings#possible-dereference-of-null
        InputFile = Convert.ToString(config["inputfile"]!);
        message = File.ReadAllText(InputFile);
    }


    OneTimePadOperations oneTimePadOperations = new OneTimePadOperations();
    
    string jsonString = File.ReadAllText(Keyfile);
    OneTimePad OTP = JsonSerializer.Deserialize<OneTimePad>(jsonString)!;


    string? EncryptedMessage = oneTimePadOperations.EncryptMessage(message, OTP.PadKey);

    File.WriteAllText(Outputfile, EncryptedMessage);


}

void GenerateKey() {

    string Outputfile;
    int MaxCharacters;
    int Series;

    if (String.IsNullOrEmpty(config["key"]))
    {
        Outputfile = "key.json";
    }
    else
    {
        //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings#possible-dereference-of-null
        Outputfile = Convert.ToString(config["key"]!);
    }

    if (String.IsNullOrEmpty(config["size"]))
    {
        MaxCharacters = OneTimePadOperations.MaxNumberOfCharacters;
    }
    else
    {

        MaxCharacters = Convert.ToInt32(config["size"]!);
    }

    if (String.IsNullOrEmpty(config["series"]))
    {
        Series = 1;
    }
    else
    {
        Series = Convert.ToInt32(config["series"]!);
    }

    OneTimePadOperations oneTimePadOperations = new OneTimePadOperations();
    OneTimePad OTP = oneTimePadOperations.GeneratePad(Series, MaxCharacters);

    string jsonString = JsonSerializer.Serialize(OTP);
    File.WriteAllText(Outputfile, jsonString);

    Console.WriteLine(File.ReadAllText(Outputfile));

}