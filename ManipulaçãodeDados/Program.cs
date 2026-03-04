using var arquivo = new FileStream("C:\\Users\\clash\\source\\repos\\ManipulaçãodeDados\\ManipulaçãodeDados\\musicas.csv", FileMode.Open, FileAccess.Read);
using var leitor = new StreamReader(arquivo);

var musicas = Obtermusicas(leitor).Select(a => a.Artista).Distinct();
var generos = Obtermusicas(leitor).SelectMany(m => m.Generos).Distinct();
EstatiticasDeMusicas(leitor);

void EstatiticasDeMusicas(StreamReader stream)
{
    var musicas = Obtermusicas(stream).ToList();
    Console.WriteLine($"\nexistem {musicas.Count()} musicas na coleção");
    Console.WriteLine($"\nexistem {musicas.Count(x => x.Duracao >=600 )} musicas com mais de 10 minutos de duração");
    Console.WriteLine($"\na menor musica: {musicas.MinBy(x=> x.Duracao).Nome} tem {musicas.Min(x => x.Duracao)} segundos");
    Console.WriteLine($"\na maior musica: {musicas.MaxBy(x=> x.Duracao).Nome}  tem {musicas.Max(x=> x.Duracao)} segundos");

}
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
            Duracao = int.Parse(dados[2]),
            Generos = dados[3].Split(',').Select(g => g.Trim())
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
    public IEnumerable<string> Generos { get; set; }
}

