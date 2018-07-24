using System;
using System.Net;
using System.IO;


namespace MLValidateAPIKeys
{
    class Program
    {
        static void Main(string[] args)
        {
            
                Program hub = new Program();
                String idMercadoLivre;
                String secretKeyMercadoLivre;

                var UserKeys = hub.InputUserData(); // Get user Keys to validate
                idMercadoLivre = UserKeys.Item1;
                secretKeyMercadoLivre = UserKeys.Item2;

                int RequestCode = hub.MercadoLivreGetSessionAndReturnStatus(idMercadoLivre, secretKeyMercadoLivre); // tries to get a Mercado Livre Access token

                requestsStatusChecker(RequestCode);
            }

            private static void requestsStatusChecker(int RequestCode)
            {
                if (RequestCode == 200) // verify the HTTP code
                {
                    Console.WriteLine("As chaves do cliente são válidas.");
                }
                else if (RequestCode == 400)
                {
                    Console.WriteLine("As chaves do cliente  são inválidas.");
                }
                else
                {
                    Console.WriteLine("Erro desconhecido. HTTP Code: " + RequestCode);
                }


                Console.ReadKey();
            }

            private int MercadoLivreGetSessionAndReturnStatus(string idMercadoLivre, string secretKeyMercadoLivre)
            {

                var webRequest = WebRequest.CreateHttp("https://api.mercadolibre.com/oauth/token?grant_type=client_credentials&client_id=" + idMercadoLivre + "&client_secret=" + secretKeyMercadoLivre);
                webRequest.Method = "POST";

                try
                {
                    var webResponse = (HttpWebResponse)webRequest.GetResponse();
                    var WebStatusCode = webResponse.StatusCode;

                    Console.WriteLine("OK> " + (int)WebStatusCode);

                    return (int)WebStatusCode;
                }
                catch (WebException we)
                {
                    var WebStatusCode = ((HttpWebResponse)we.Response).StatusCode;
                    Console.WriteLine("Error> " + (int)WebStatusCode);

                    return (int)WebStatusCode;
                }

            }

            private Tuple<string, string> InputUserData()
            {
                String idMercadoLivre;
                String secretKeyMercadoLivre;

                Console.WriteLine("Olá Lojista, bem vindo a 2BHub!");
                Console.WriteLine("Para começarmos, informe suas chaves de acesso no Mercado Livre.");

                Console.WriteLine();
                Console.Write("Id Mercado Livre: ");
                idMercadoLivre = Console.ReadLine();


                Console.Write("Secret Key Mercado Livre: ");
                secretKeyMercadoLivre = Console.ReadLine();

                Console.WriteLine("");
                Console.WriteLine("*----------------------------------*");

                Console.WriteLine("id: " + idMercadoLivre);
                Console.WriteLine("Secret: " + secretKeyMercadoLivre);

                Console.WriteLine("*----------------------------------*");
                Console.WriteLine("");


                return new Tuple<String, String>(idMercadoLivre, secretKeyMercadoLivre);
            }
        }

}