using System.Text;

HttpResponseMessage result;
const string BASE = "https://localhost:7026";

using var client = new HttpClient();

string employee = @"{
    ""name"": ""Julia Hendricks"",
    ""age"": 30,
    ""position"": ""CEO""
}";

result = await client.PostAsync($"{BASE}/employee/1", new StringContent(employee, Encoding.UTF8, "application/json"));
Console.WriteLine($"{result.StatusCode}:\n{await result.Content.ReadAsStringAsync()}");

result = await client.GetAsync($"{BASE}/employee/1");
Console.WriteLine($"{result.StatusCode}:\n{await result.Content.ReadAsStringAsync()}");

employee = @"{
    ""name"": ""Julia Hendricks"",
    ""age"": 31,
    ""position"": ""CEO""
}";
result = await client.PutAsync($"{BASE}/employee/1", new StringContent(employee, Encoding.UTF8, "application/json"));
Console.WriteLine($"{result.StatusCode}:\n{await result.Content.ReadAsStringAsync()}");

result = await client.GetAsync($"{BASE}/employee/1");
Console.WriteLine($"{result.StatusCode}:\n{await result.Content.ReadAsStringAsync()}");

result = await client.DeleteAsync($"{BASE}/employee/1");
Console.WriteLine($"{result.StatusCode}:\n{await result.Content.ReadAsStringAsync()}");

result = await client.GetAsync($"{BASE}/employee/1");
Console.WriteLine($"{result.StatusCode}:\n{await result.Content.ReadAsStringAsync()}");