namespace JogoDaForca.ConsoleApp
{
    internal class Program
    {
        static void mensagemErroOpcao()
        {
            Console.Clear();
            Console.WriteLine(" Erro! Essa escolha não existe, favor escolher uma que exista.");
            Console.WriteLine(" Aperte Enter para continuar...");
            Console.ReadLine();
        }

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
                    mensagemErroOpcao();
                }
                else
                    break;
            }

            return opcao;
        }

        static char[] constroiArrayVazio(int tamanhoArray, char[] array)
        {
            for (int caractere = 0; caractere < tamanhoArray; caractere++)
            {
                // acessar o array no índice 0
                array[caractere] = '_';
            }

            return array;
        }

        static bool [] condicaoVitoria(string palavraSecreta, string dicaDaPalavra, int qtErros, int qtErrosMaximo, string chutePalavra, bool ePalavra)
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

            bool[] estatusJogador = {jogadorAcertou, jogadorEnforcou};

            return estatusJogador;
        }

        static int indiceEspacoLivre(char [] letrasPalavraSecreta)
        {
            int index = 0;

            for (int indice = 0; indice < letrasPalavraSecreta.Length; indice++)
            {
                if ( (indice-1) >= 0)
                {
                    if (letrasPalavraSecreta[indice-1] != ' ' && letrasPalavraSecreta[indice] == ' ')
                    {
                        index = indice;
                        break;
                    }
                }
            }

            return index;
        }

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
                    else if (indiceLetra == (tamanhoPalavra-1) )
                        encontrouLetra = false;
                }

                if (encontrouLetra == false)
                    indiceUltimaLetra = indiceEspacoLivre(letrasPalavraSecreta);
                    letrasPalavraSecreta[indiceUltimaLetra] = palavraSecreta[indiceLetraSecreta];
            }

            return letrasPalavraSecreta.Length - qtEspacosVazios(letrasPalavraSecreta) + qtErrosMaximo;
        }

        static void mostrarMenuJogo(string dicaDaPalavra, int qtErros, char [] letrasDigitadas)
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
                    if ((letra+1) < tamanho)
                        if (letrasDigitadas[letra+1] != '_')
                            Console.Write($"{letrasDigitadas[letra]} | ");
                        else
                            Console.Write($"{letrasDigitadas[letra]}");
                }
            }

            Console.WriteLine("\n ---------------------------------------");
            Console.WriteLine($" Quantidade de erros: {qtErros}");
        }

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

        static char opcaoDecisao()
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

        static string escolhaPalavraSecreta()
        {

            string[] frutas = {"ABACATE", "GOIABA", "MELANCIA", "MAÇA"};
            string[] paises = {"CHINA", "INDIA", "RUSSIA", "BRASIL"};
            string[] animais = {"ELEFANTE", "TARTARUGA", "ARARA", "CACHORRO"};

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
            Console.WriteLine("  |     {0}{1}{2}    ", bracoEsquerdo, troncoCima, bracoDireito);
            Console.WriteLine("  |        {0}       ", troncoBaixo);
            Console.WriteLine("  |        {0}       ", pernas);
            Console.WriteLine("  |                  ");
            Console.WriteLine("  |                  ");
            Console.WriteLine(" _|____              ");
        }

        static void Main(string[] args)
        {
            char opcao = 'S';
            int qtErrosMaximo = 5;

            while (opcao == 'S')
            {
                string palavraSecreta = escolhaPalavraSecreta();
                int qtMaxLetras = maximoLetras(palavraSecreta, qtErrosMaximo);

                int qtErros = 0;
                bool jogadorEnforcou = false;
                bool jogadorAcertou = false;
                char[] letrasEncontradas = new char[palavraSecreta.Length];
                char[] letrasDigitadas = new char[qtMaxLetras];
                int contadorLetras = 0;

                letrasDigitadas = constroiArrayVazio(letrasDigitadas.Length, letrasDigitadas);

                letrasEncontradas = constroiArrayVazio(letrasEncontradas.Length, letrasEncontradas);

                do
                {
                    string dicaDaPalavra = String.Join(" ", letrasEncontradas);
                    string palavraDigitada;
                    char opcaoResposta;
                    char chute = ' ';
                    string chutePalavra = " ";
                    bool respostaEncontrada = false;
                    bool[] estatus;
                    bool ePalavra = false;

                    bool checagemOpcao = false;
                    while (checagemOpcao == false)
                    {
                        desenhoForca(qtErros);

                        mostrarMenuJogo(dicaDaPalavra, qtErros, letrasDigitadas);

                        opcaoResposta = opcaoDecisao();

                        switch (opcaoResposta)
                        {
                            case '1':
                                Console.WriteLine(" ---------------------------------------");
                                Console.Write(" Digite a palavra: ");
                                chutePalavra = Console.ReadLine().ToUpper();

                                ePalavra = true;

                                if (chutePalavra != palavraSecreta)
                                    respostaEncontrada = false;
                                else
                                    respostaEncontrada = true;

                                checagemOpcao = true;
                                break;

                            case '2':
                                
                                Console.WriteLine(" ---------------------------------------");
                                Console.Write(" Digite uma letra: ");
                                chute = Console.ReadLine()[0]; // obtém apenas um caractere que o usuário digitou
                                chute = Char.ToUpper(chute);

                                if (letraPodeEntrar(chute, letrasDigitadas))
                                {
                                    letrasDigitadas[contadorLetras] = chute;
                                    contadorLetras++;
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(" Erro, essa letra já foi digitada! Favor digitar uma diferente.");
                                    Console.WriteLine(" Aperte Enter para continuar...");
                                    Console.ReadLine();
                                    break;
                                }

                                for (int contador = 0; contador < palavraSecreta.Length; contador++)
                                {
                                    char letraAtual = palavraSecreta[contador];

                                    if (chute == letraAtual)
                                    {
                                        letrasEncontradas[contador] = letraAtual;
                                        respostaEncontrada = true;
                                    }
                                }

                                checagemOpcao = true;
                                break;

                            default:
                                mensagemErroOpcao();
                                break;
                        }
                    }

                    if (respostaEncontrada == false)
                        qtErros++;

                    dicaDaPalavra = String.Join("", letrasEncontradas);

                    estatus = condicaoVitoria(palavraSecreta, dicaDaPalavra, qtErros, qtErrosMaximo, chutePalavra, ePalavra);

                    jogadorAcertou = estatus[0];
                    jogadorEnforcou = estatus[1];

                    Console.ReadLine();

                } while (jogadorAcertou == false && jogadorEnforcou == false);

                opcao = opcaoSaida();
            }
        }
    }
}