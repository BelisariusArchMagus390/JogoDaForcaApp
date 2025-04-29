namespace JogoDaForca.ConsoleApp
{
    internal class Program
    {
        static Menu menu = new Menu();
        static Forca forca = new Forca();

        static void Main(string[] args)
        {
            char opcao = 'S';

            while (opcao == 'S')
            {
                forca.escolhaPalavraSecreta();
                int qtMaxLetras = forca.maximoLetras();

                forca.LetrasEncontradas = new char[forca.PalavraSecreta.Length];
                forca.LetrasDigitadas = new char[qtMaxLetras];
                int contadorLetras = 0;

                forca.LetrasDigitadas = forca.constroiArrayVazio(forca.LetrasDigitadas.Length, forca.LetrasDigitadas);

                forca.LetrasEncontradas = forca.constroiArrayVazio(forca.LetrasEncontradas.Length, forca.LetrasEncontradas);

                do
                {
                    // junta todas as letras do array letrasEncontradas em uma única string, mas coloca um espaço " " entre as letras
                    forca.DicaDaPalavra = String.Join(" ", forca.LetrasEncontradas);
                    char opcaoResposta;
                    char chute = ' ';
                    string chutePalavra = " ";
                    bool respostaEncontrada = false;
                    bool ePalavra = false;

                    bool checagemOpcao = false;
                    while (checagemOpcao == false)
                    {
                        forca.desenhoForca();

                        menu.mostrarMenuJogo(forca.DicaDaPalavra, forca.QtErros, forca.LetrasDigitadas);

                        opcaoResposta = menu.opcaoDecisao();

                        switch (opcaoResposta)
                        {
                            case '1':
                                Console.WriteLine(" ---------------------------------------");
                                Console.Write(" Digite a palavra: ");
                                chutePalavra = Console.ReadLine().ToUpper();

                                ePalavra = true;

                                respostaEncontrada = forca.checaPalavra(chutePalavra);

                                checagemOpcao = true;
                                break;

                            case '2':

                                Console.WriteLine(" ---------------------------------------");
                                Console.Write(" Digite uma letra: ");
                                chute = Console.ReadLine()[0]; // obtém apenas um caractere que o usuário digitou
                                chute = Char.ToUpper(chute);

                                if (forca.letraPodeEntrar(chute))
                                {
                                    forca.LetrasDigitadas[contadorLetras] = chute;
                                    contadorLetras++;
                                }
                                else
                                {
                                    menu.mensagemErroOpcao("Essa letra já foi digitada! Favor digitar uma diferente.");
                                    break;
                                }

                                respostaEncontrada = forca.checaLetra(chute);

                                checagemOpcao = true;
                                break;

                            default:
                                menu.mensagemErroOpcao("Essa escolha não existe, favor escolher uma que exista.");
                                break;
                        }
                    }

                    if (respostaEncontrada == false)
                        forca.QtErros++;

                    // junta todas as letras do array letrasEncontradas em uma única string
                    forca.DicaDaPalavra = String.Join("", forca.LetrasEncontradas);

                    forca.condicaoVitoria(chutePalavra, ePalavra);

                    Console.ReadLine();

                } while (forca.JogadorAcertou == false && forca.JogadorEnforcou == false);

                opcao = menu.opcaoSaida();
            }
        }
    }
}