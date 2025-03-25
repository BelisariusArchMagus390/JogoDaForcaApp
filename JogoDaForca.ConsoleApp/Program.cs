namespace JogoDaForca.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string palavraSecreta = "MELANCIA";

            char[] letrasEncontradas = new char[palavraSecreta.Length];

            for (int caractere = 0; caractere < letrasEncontradas.Length; caractere++)
            {
                // acessar o array no índice 0
                letrasEncontradas[caractere] = '_';
            }

            string dicaDaPalavra = String.Join(" ", letrasEncontradas);

            Console.Clear();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Jogo da Forca");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"Palavra secreta: {dicaDaPalavra}");
            Console.WriteLine("---------------------------------------");

            Console.Write("Digite uma letra: ");
            char chute = Console.ReadLine()[0]; // obtém apenas um caractere que o usuário digitou

            Console.WriteLine(chute);

            Console.ReadLine();
        }
    }
}
