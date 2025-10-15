namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://localhost:7186/api/students");

            if (response.IsSuccessStatusCode)
            {
                var studentsInJson = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Students: " + studentsInJson);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }

            #region test

            //var response1 = await httpClient.GetStringAsync("https://localhost:7186/api/Students");
            //Console.WriteLine(response1);
            #endregion
        }
    }
}
