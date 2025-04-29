using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaForca.ConsoleApp
{
    internal class Forca
    {
        Menu menu = new Menu();

        public string PalavraSecreta { get; set; }
        public int QtErros { get; set; }
        public bool JogadorEnforcou { get; set; }
        public bool JogadorAcertou { get; set; }
        public char[] LetrasDigitadas { get; set; }
        public string DicaDaPalavra { get; set; }
        public int QtErrosMaximo { get; set; }
        public char[] LetrasEncontradas { get; set; }

        public Forca()
        {
            QtErros = 0;
            JogadorEnforcou = false;
            JogadorAcertou = false;
            QtErrosMaximo = 5;
        }

        public char[] constroiArrayVazio(int tamanhoArray, char[] array)
        {
            for (int caractere = 0; caractere < tamanhoArray; caractere++)
            {
                // acessar o array no índice 0
                array[caractere] = '_';
            }

            return array;
        }

        public void condicaoVitoria(string chutePalavra, bool ePalavra)
        {
            if (ePalavra == true)
                JogadorAcertou = chutePalavra == PalavraSecreta;
            else
                JogadorAcertou = DicaDaPalavra == PalavraSecreta;

            JogadorEnforcou = QtErros > QtErrosMaximo;

            if (JogadorAcertou)
            {
                Console.Clear();
                Console.WriteLine(" ---------------------------------------");
                Console.WriteLine(" Você acertou a palavra!");
                Console.WriteLine($" A palavra secreta era: {PalavraSecreta}");
                Console.WriteLine(" ---------------------------------------");
                Console.WriteLine(" Aperte Enter para continuar...");
            }
            else if (JogadorEnforcou)
            {
                Console.Clear();
                Console.WriteLine(" ---------------------------------------");
                Console.WriteLine(" Que azar, você perdeu!");
                Console.WriteLine($" A palavra secreta era: {PalavraSecreta}");
                Console.WriteLine(" ---------------------------------------");
                Console.WriteLine(" Aperte Enter para continuar...");
            }
        }

        // define apartir de que ponto há apenas espaços vazios no array
        public int indiceEspacoLivre(char[] array)
        {
            int index = 0;

            for (int indice = 0; indice < array.Length; indice++)
            {
                if ((indice - 1) >= 0)
                {
                    if (array[indice - 1] != ' ' && array[indice] == ' ')
                    {
                        index = indice;
                        break;
                    }
                }
            }

            return index;
        }

        // define a quantidade de espaços vazios de um array
        public int qtEspacosVazios(char[] array)
        {
            int contadorEspacosVazios = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == ' ')
                    contadorEspacosVazios++;
            }

            return contadorEspacosVazios;
        }

        //define a quantidade máxima de letras que a partida pode ter para definir o tamanho do array das letras já digitadas
        public int maximoLetras()
        {
            int contadorLetra;
            int tamanhoPalavra = PalavraSecreta.Length;
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
                    if (PalavraSecreta[indiceLetraSecreta] == letrasPalavraSecreta[indiceLetra])
                    {
                        break;
                    }
                    else if (indiceLetra == (tamanhoPalavra - 1))
                        encontrouLetra = false;
                }

                if (encontrouLetra == false)
                    indiceUltimaLetra = indiceEspacoLivre(letrasPalavraSecreta);
                letrasPalavraSecreta[indiceUltimaLetra] = PalavraSecreta[indiceLetraSecreta];
            }

            return letrasPalavraSecreta.Length - qtEspacosVazios(letrasPalavraSecreta) + QtErrosMaximo;
        }

        // verifica se a letra pode ser adicionada no array
        public bool letraPodeEntrar(char chute)
        {
            bool podeEntrar = true;
            for (int i = 0; i < LetrasDigitadas.Length; i++)
            {
                if (LetrasDigitadas[i] == chute)
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

        public void escolhaPalavraSecreta()
        {
            string[] frutas = { "ABACATE", "GOIABA", "MELANCIA", "MAÇA" };
            string[] paises = { "CHINA", "INDIA", "RUSSIA", "BRASIL" };
            string[] animais = { "ELEFANTE", "TARTARUGA", "ARARA", "CACHORRO" };

            string[] tema = new string[4];
            int indiceAleatorio = 0;

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
                        menu.mensagemErroOpcao("Essa escolha não existe, favor escolher uma que exista.");
                        break;
                }
            }

            Random random = new Random();
            indiceAleatorio = random.Next(0, tema.Length);

            PalavraSecreta = tema[indiceAleatorio];
        }

        public void desenhoForca()
        {
            string cabeca = QtErros >= 1 ? " o " : " ";
            string troncoCima = QtErros >= 2 ? "|" : " ";
            string troncoBaixo = QtErros >= 2 ? " | " : " ";
            string bracoEsquerdo = QtErros >= 3 ? "/" : " ";
            string bracoDireito = QtErros >= 4 ? @"\" : " ";
            string pernas = QtErros >= 5 ? "/ \\" : " ";

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

        public bool checaPalavra(string chutePalavra)
        {
            bool respostaEncontrada = false;

            if (chutePalavra == PalavraSecreta)
                respostaEncontrada = true;
            
            return respostaEncontrada;
        }

        public bool checaLetra(char chuteLetra)
        {
            bool respostaEncontrada = false;

            for (int contador = 0; contador < PalavraSecreta.Length; contador++)
            {
                char letraAtual = PalavraSecreta[contador];

                if (chuteLetra == letraAtual)
                {
                    LetrasEncontradas[contador] = letraAtual;
                    respostaEncontrada = true;
                }
            }
            return respostaEncontrada;
        }
    }
}
