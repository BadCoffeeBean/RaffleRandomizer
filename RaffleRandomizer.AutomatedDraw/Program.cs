using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

Console.WriteLine("Welcome to Navflix!");
await Task.Delay(2000);
Console.WriteLine("Today, we're drawing the 75 lucky minor prize winners. Good luck to everyone!");
await Task.Delay(2000);

var url = "https://localhost:5001/raffle/winners/db?count=1&prizeType=minor&allowMultipleChances=false";

for (int x = 0; x < 75; x++)
{
	await Task.Delay(1000);
	dynamic result;
	do { result = await url.GetJsonListAsync(); } while (result[0].firstName.Trim() == "FILLER");
	Console.WriteLine($"[{x + 1}] {result[0].firstName.Trim().ToUpper()} {result[0].lastName.Trim().ToUpper()}");
}

Console.WriteLine("That's it for today. Congratulations to everyone who won and be sure to show yourself up on Thursday for even bigger prizes!");
await Task.Delay(2000);
Console.WriteLine("Shutting down...");
Console.ReadKey();