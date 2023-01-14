
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CG.Cryptography;
using System.Text;

var builder = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddCryptography();
    });

var host = builder.Build();

host.RunDelegate((host, token) =>
{
    // Get a cryptographer object.
    var cryptographer = host.Services.GetRequiredService<ICryptographer>();

    // Generate a IV and SALT from your password.
    var tuple = cryptographer.GenerateKeyAndIVAsync(
        "my password",
        "my salt value"
        ).Result;

    Console.WriteLine("Enter a line to encrypt, then hit the enter key");
    var value = Console.ReadLine();

    // Encrypt the value.
    var encryptedValue = cryptographer.AesEncryptAsync(
        tuple.Item1,
        tuple.Item2,
        value
        ).Result;

    // Decrypt the value.
    var plainValue = cryptographer.AesDecryptAsync(
        tuple.Item1,
        tuple.Item2,
        encryptedValue
        ).Result;

    Console.WriteLine();
    Console.WriteLine($"Here is the value encrypted: {encryptedValue}");
    Console.WriteLine($"Here is the value decrypted: {plainValue}");

    Console.WriteLine();
    Console.WriteLine("done");
});