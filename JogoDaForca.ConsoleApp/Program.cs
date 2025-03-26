namespace JogoDaForca.ConsoleApp
{
    internal class Program
    {
        static char opcaoSaida()
        {
            char opcao;

            while (true)
            {
                Console.Clear();
                Console.Write(" Gostaria de jogar de novo?(S/N): ");
                opcao = Console.ReadLine()[0];
                opcao = Char.ToUpper(opcao);

                if (opcao != 'S' && opcao != 'N')
                {
                    Console.Clear();
                    Console.WriteLine(" Opção inválida, favor escolher uma existente.");
                    Console.WriteLine(" Aperte Enter para continuar...");
                }
                else
                    break;

                Console.ReadLine();
            }

            return opcao;
        }

        static char[] constroiDicaPalavra(int tamanhoArray, char[] letraEncontradas)
        {
            for (int caractere = 0; caractere < tamanhoArray; caractere++)
            {
                // acessar o array no índice 0
                letraEncontradas[caractere] = '_';
            }

            return letraEncontradas;
        }

        static void condicaoVitoria(bool jogadorAcertou, bool jogadorEnforcou, string palavraSecreta)
        {
            if (jogadorAcertou)
            {
                Console.Clear();
                Console.WriteLine(" ---------------------------------------");
                Console.WriteLine(" Você acertou a palavra!");
                Console.WriteLine($" A palavra secreta era: {palavraSecreta}");
                Console.WriteLine(" ---------------------------------------");
            }
            else if (jogadorEnforcou)
            {
                Console.Clear();
                Console.WriteLine(" ---------------------------------------");
                Console.WriteLine(" Que azar, você perdeu!");
                Console.WriteLine($" A palavra secreta era: {palavraSecreta}");
                Console.WriteLine(" ---------------------------------------");
            }
        }

        static void Main(string[] args)
        {
            char opcao = 'S';
            string palavraSecreta = "MELANCIA";

            while (opcao == 'S')
            {
                int quantidadeErros = 0;
                bool jogadorEnforcou = false;
                bool jogadorAcertou = false;
                char[] letrasEncontradas = new char[palavraSecreta.Length];

                letrasEncontradas = constroiDicaPalavra(letrasEncontradas.Length, letrasEncontradas);

                do
                {
                    string dicaDaPalavra = String.Join(" ", letrasEncontradas);

                    Console.Clear();
                    Console.WriteLine(" ---------------------------------------");
                    Console.WriteLine(" Jogo da Forca");
                    Console.WriteLine(" ---------------------------------------");
                    Console.WriteLine(" Máximo de erros: 5");
                    Console.WriteLine(" ---------------------------------------");
                    Console.WriteLine($" Palavra secreta: {dicaDaPalavra}");
                    Console.WriteLine(" ---------------------------------------");
                    Console.WriteLine($" Quantidade de erros: {quantidadeErros}");
                    Console.WriteLine("----------------------------------------");

                    Console.Write(" Digite uma letra: ");
                    char chute = Console.ReadLine()[0]; // obtém apenas um caractere que o usuário digitou
                    chute = Char.ToUpper(chute);

                    bool letraFoiEncontrada = false;

                    for (int contador = 0; contador < palavraSecreta.Length; contador++)
                    {
                        char letraAtual = palavraSecreta[contador];

                        if (chute == letraAtual)
                        {
                            letrasEncontradas[contador] = letraAtual;
                            letraFoiEncontrada = true;
                        }
                    }

                    if (letraFoiEncontrada == false)
                        quantidadeErros++;

                    dicaDaPalavra = String.Join("", letrasEncontradas);

                    jogadorAcertou = dicaDaPalavra == palavraSecreta;
                    // o jogador poderá cometer 5 erros antes de perder
                    jogadorEnforcou = quantidadeErros > 5;

                    condicaoVitoria(jogadorAcertou, jogadorEnforcou, palavraSecreta);

                    Console.ReadLine();

                } while (jogadorAcertou == false && jogadorEnforcou == false);

                opcao = opcaoSaida();
            }
        }
    }
}