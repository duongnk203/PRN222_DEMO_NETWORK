using System.Net;

class Program
{
    static void Main(string[] args)
    {
        WebRequest request = WebRequest.Create("http://www.contoso.com/default,html");

        request.Credentials = CredentialCache.DefaultCredentials;

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Console.WriteLine("Status: " + response.StatusDescription);
        Console.WriteLine(new String('*', 50));

        Stream dataStream = response.GetResponseStream();

        StreamReader reader = new StreamReader(dataStream);

        String responseFromServer = reader.ReadToEnd();

        Console.WriteLine(responseFromServer);
        Console.WriteLine(new String('*', 50));
        reader.Close();
        dataStream.Close();
        response.Close();
    }
}