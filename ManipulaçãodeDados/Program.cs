using var arquivo = new FileStream("C:\\Users\\clash\\source\\repos\\ManipulaçãodeDados\\ManipulaçãodeDados\\musicas.csv", FileMode.Open, FileAccess.Read);
using var leitor = new StreamReader(arquivo);

var musicas = Obtermusicas(leitor).FirstOrDefault();
exibirMusicas(musicas);
void exibirMusicas(IEnumerable<Musica> musicas)
{
    foreach (var musica in musicas)
    {
        var contador = 0;
        Console.WriteLine($"Musica: {musica.Nome}");
        contador++;
        if(contador >= 10)
            break;
    }
}
IEnumerable<Musica> Obtermusicas(StreamReader leitor)
{
    var linha = leitor.ReadLine();
    while (linha is not null)
    {
        var dados = linha.Split(';');
        var musica=  new Musica

        {
            Nome = dados[0],
            Artista = dados[1],
            Duracao = int.Parse(dados[2])
        };
            yield return musica;
        linha = leitor.ReadLine();
        

    }
}
public static class Musicaextensions
{
    public static IEnumerable<Musica> FiltrarPor(this IEnumerable<Musica> musicas, Func<Musica, bool> criterio)
    {
        foreach (var musica in musicas)
        {
            if (criterio(musica))
            {
                yield return musica;
            }
        }
    }
}
public class Musica()
{
    public string Nome { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; }
}

