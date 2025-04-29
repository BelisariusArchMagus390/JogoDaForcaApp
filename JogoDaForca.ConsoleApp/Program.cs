namespace JogoDaForca.ConsoleApp
{
    internal class Program
    {
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
                    // junta todas as letras do array letrasEncontradas em uma única string, mas coloca um espaço " " entre as letras
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

                    // junta todas as letras do array letrasEncontradas em uma única string
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