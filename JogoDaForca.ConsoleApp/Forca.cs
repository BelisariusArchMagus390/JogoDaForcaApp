using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaForca.ConsoleApp
{
    internal class Forca
    {
        static char[] constroiArrayVazio(int tamanhoArray, char[] array)
        {
            for (int caractere = 0; caractere < tamanhoArray; caractere++)
            {
                // acessar o array no índice 0
                array[caractere] = '_';
            }

            return array;
        }

        static bool[] condicaoVitoria(string palavraSecreta, string dicaDaPalavra, int qtErros, int qtErrosMaximo, string chutePalavra, bool ePalavra)
        {
            bool jogadorAcertou;
            bool jogadorEnforcou;

            if (ePalavra == true)
                jogadorAcertou = chutePalavra == palavraSecreta;
            else
                jogadorAcertou = dicaDaPalavra == palavraSecreta;

            jogadorEnforcou = qtErros > qtErrosMaximo;

            if (jogadorAcertou)
            {
                Console.Clear();
                Console.WriteLine(" ---------------------------------------");
                Console.WriteLine(" Você acertou a palavra!");
                Console.WriteLine($" A palavra secreta era: {palavraSecreta}");
                Console.WriteLine(" ---------------------------------------");
                Console.WriteLine(" Aperte Enter para continuar...");
            }
            else if (jogadorEnforcou)
            {
                Console.Clear();
                Console.WriteLine(" ---------------------------------------");
                Console.WriteLine(" Que azar, você perdeu!");
                Console.WriteLine($" A palavra secreta era: {palavraSecreta}");
                Console.WriteLine(" ---------------------------------------");
                Console.WriteLine(" Aperte Enter para continuar...");
            }

            bool[] estatusJogador = { jogadorAcertou, jogadorEnforcou };

            return estatusJogador;
        }

        // define apartir de que ponto há apenas espaços vazios no array
        static int indiceEspacoLivre(char[] letrasPalavraSecreta)
        {
            int index = 0;

            for (int indice = 0; indice < letrasPalavraSecreta.Length; indice++)
            {
                if ((indice - 1) >= 0)
                {
                    if (letrasPalavraSecreta[indice - 1] != ' ' && letrasPalavraSecreta[indice] == ' ')
                    {
                        index = indice;
                        break;
                    }
                }
            }

            return index;
        }

        // define a quantidade de espaços vazios de um array
        static int qtEspacosVazios(char[] letrasPalavraSecreta)
        {
            int contador = 0;
            for (int i = 0; i < letrasPalavraSecreta.Length; i++)
            {
                if (letrasPalavraSecreta[i] == ' ')
                    contador++;
            }

            return contador;
        }

        //define a quantidade máxima de letras que a partida pode ter para definir o tamanho do array das letras já digitadas
        static int maximoLetras(string palavraSecreta, int qtErrosMaximo)
        {
            int contadorLetra;
            int tamanhoPalavra = palavraSecreta.Length;
            char[] letrasPalavraSecreta = new char[tamanhoPalavra];
            bool encontrouLetra;
            int indiceUltimaLetra = 0;

            for (int caractere = 0; caractere < tamanhoPalavra; caractere++)
            {
                letrasPalavraSecreta[caractere] = ' ';
            }

            for (int indiceLetraSecreta = 0; indiceLetraSecreta < tamanhoPalavra; indiceLetraSecreta++)
            {
                encontrouLetra = true;
                for (int indiceLetra = 0; indiceLetra < tamanhoPalavra; indiceLetra++)
                {
                    if (palavraSecreta[indiceLetraSecreta] == letrasPalavraSecreta[indiceLetra])
                    {
                        break;
                    }
                    else if (indiceLetra == (tamanhoPalavra - 1))
                        encontrouLetra = false;
                }

                if (encontrouLetra == false)
                    indiceUltimaLetra = indiceEspacoLivre(letrasPalavraSecreta);
                letrasPalavraSecreta[indiceUltimaLetra] = palavraSecreta[indiceLetraSecreta];
            }

            return letrasPalavraSecreta.Length - qtEspacosVazios(letrasPalavraSecreta) + qtErrosMaximo;
        }

        // verifica se a letra pode ser adicionada no array
        static bool letraPodeEntrar(char chute, char[] letrasDigitadas)
        {
            bool podeEntrar = true;
            for (int i = 0; i < letrasDigitadas.Length; i++)
            {
                if (letrasDigitadas[i] == chute)
                {
                    podeEntrar = false;
                    break;
                }
            }
            if (podeEntrar)
                return true;
            else
                return false;
        }

        static string escolhaPalavraSecreta()
        {

            string[] frutas = { "ABACATE", "GOIABA", "MELANCIA", "MAÇA" };
            string[] paises = { "CHINA", "INDIA", "RUSSIA", "BRASIL" };
            string[] animais = { "ELEFANTE", "TARTARUGA", "ARARA", "CACHORRO" };

            string[] tema = new string[5];
            int indiceAleatorio = 0;
            string palavraSecreta;

            bool checagemResposta = false;
            while (checagemResposta == false)
            {
                Console.Clear();
                Console.WriteLine(" ---------------------------------------");
                Console.WriteLine(" 1 - Frutas");
                Console.WriteLine(" 2 - Países");
                Console.WriteLine(" 3 - Animais");
                Console.WriteLine(" ---------------------------------------");
                Console.Write(" Escolha o tema da forca: ");
                char opcaoTema = Console.ReadLine()[0];

                switch (opcaoTema)
                {
                    case '1':
                        tema = frutas;
                        checagemResposta = true;
                        break;
                    case '2':
                        tema = paises;
                        checagemResposta = true;
                        break;
                    case '3':
                        tema = animais;
                        checagemResposta = true;
                        break;
                    default:
                        mensagemErroOpcao();
                        break;
                }
            }

            Random random = new Random();
            indiceAleatorio = random.Next(tema.Length);

            palavraSecreta = tema[indiceAleatorio];

            return palavraSecreta;
        }

        static void desenhoForca(int qtErros)
        {
            string cabeca = qtErros >= 1 ? " o " : " ";
            string troncoCima = qtErros >= 2 ? "|" : " ";
            string troncoBaixo = qtErros >= 2 ? " | " : " ";
            string bracoEsquerdo = qtErros >= 3 ? "/" : " ";
            string bracoDireito = qtErros >= 4 ? @"\" : " ";
            string pernas = qtErros >= 5 ? "/ \\" : " ";

            Console.Clear();
            Console.WriteLine(" ---------------------------------------");
            Console.WriteLine(" Jogo da Forca");
            Console.WriteLine(" ---------------------------------------");
            Console.WriteLine("  ___________        ");
            Console.WriteLine("  |/        |        ");
            Console.WriteLine("  |        {0}       ", cabeca);
            Console.WriteLine("  |        {0}{1}{2}    ", bracoEsquerdo, troncoCima, bracoDireito);
            Console.WriteLine("  |        {0}       ", troncoBaixo);
            Console.WriteLine("  |        {0}       ", pernas);
            Console.WriteLine("  |                  ");
            Console.WriteLine("  |                  ");
            Console.WriteLine(" _|____              ");
        }
    }
}
