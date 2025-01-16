using static System.Console;
using System.Net;
using System.Threading.Tasks.Dataflow;
public class  Program
{
    static void Main(string[] args)
    {
        WebRequest request = WebRequest.Create("https://www.google.com");
        request.Credentials = CredentialCache.DefaultCredentials;

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        WriteLine("status: " + response.StatusCode);
        WriteLine("==============================");

        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string responseFromServer = reader.ReadToEnd();

        WriteLine(responseFromServer);
        WriteLine("===============================");
        response.Close();
        dataStream.Close();
        reader.Close();

    }
}