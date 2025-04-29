using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaForca.ConsoleApp
{
    internal class Menu
    {
        public void mostrarMenuJogo(string dicaDaPalavra, int qtErros, char[] letrasDigitadas)
        {
            Console.WriteLine(" ---------------------------------------");
            Console.WriteLine(" Máximo de erros: 5");
            Console.WriteLine(" ---------------------------------------");
            Console.WriteLine($" Palavra secreta: {dicaDaPalavra}");
            Console.WriteLine(" ---------------------------------------");

            int tamanho = letrasDigitadas.Length;

            Console.Write(" Letras já usadas: ");

            for (int letra = 0; letra < tamanho; letra++)
            {
                if (letrasDigitadas[letra] != '_')
                {
                    if ((letra + 1) < tamanho)
                        if (letrasDigitadas[letra + 1] != '_')
                            Console.Write($"{letrasDigitadas[letra]} | ");
                        else
                            Console.Write($"{letrasDigitadas[letra]}");
                }
            }

            Console.WriteLine("\n ---------------------------------------");
            Console.WriteLine($" Quantidade de erros: {qtErros}");
        }

        public char opcaoDecisao()
        {
            char opcaoResposta;

            Console.WriteLine(" ---------------------------------------");
            Console.WriteLine(" 1 - Palavra");
            Console.WriteLine(" 2 - Letra");
            Console.WriteLine(" ---------------------------------------");
            Console.Write(" Escolha se quer responder com uma palavra ou letra: ");
            opcaoResposta = Console.ReadLine()[0];

            return opcaoResposta;
        }

        public char opcaoSaida()
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
                    mensagemErroOpcao();
                }
                else
                    break;
            }

            return opcao;
        }

        public void mensagemErroOpcao()
        {
            Console.Clear();
            Console.WriteLine(" Erro! Essa escolha não existe, favor escolher uma que exista.");
            Console.WriteLine(" Aperte Enter para continuar...");
            Console.ReadLine();
        }
    }
}
