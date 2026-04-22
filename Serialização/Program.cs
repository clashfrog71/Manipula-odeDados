using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.RegularExpressions;

using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);
void FazerArquivoJson(StreamReader stream)
{
    var Artistas = ObterMusicas(stream)
        .GroupBy(x => x.Artista)
        .Select(y => new { Artista = y.Key, Musicas = y.OrderBy(m => m.Lancamento), Total = y.Count() })
        .ToList();
    var nomeArquivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "musicas.json");
    var arquivoJson = new FileStream(nomeArquivo, FileMode.Create, FileAccess.Write);
    JsonSerializer.Serialize(arquivoJson, Artistas);

}
void OrdenandoArtistasporDatadeLancamento(StreamReader stream)
{
    var musicas = ObterMusicas(stream)
        .GroupBy(x => x.Artista)
        .Select(y => new { Artista = y.Key, Musicas = y.OrderBy(m => m.Lancamento), Total = y.Count() })
        .ToList();  
}

void ComparandoStrings(StreamReader stream)
{
    var musicas = ObterMusicas(stream)
    .Where(x => x.Artista.Equals("ColdPlay", StringComparison.CurrentCultureIgnoreCase))
    .Take(50);
}
void ExibirMusicas(IEnumerable<Musica> musicas)
{
    Console.WriteLine("\nMúsicas do arquivo:");
    foreach (var musica in musicas)
    {
        var linha = $"\t- {musica.Titulo} ({musica.Artista}) - {musica.Duracao}s [{musica.Lancamento}]";
        Console.WriteLine(linha);
    }
}

IEnumerable<Musica> ObterMusicas(StreamReader stream)
{
    var linha = stream.ReadLine();
    while (linha is not null)
    {
        var partes = linha.Split(';');
        if (partes.Length == 5)
        {
            var musica = new Musica
            {
                Titulo = string.IsNullOrWhiteSpace(partes[0]) ? "Titulo não encontrado" : partes[0],
                Artista = string.IsNullOrEmpty(partes[1]) ? "Artista não encontrado" : partes[1],
                Duracao = int.TryParse(partes[2], out int duracaoConvertida) ? duracaoConvertida : 350,
                Generos = partes[3].Split(',').Select(g => g.Trim()),
                Lancamento = DateTime.TryParse(partes[4], out var lancamentoConvertido) ? lancamentoConvertido : DateTime.Today
            };
            yield return musica;
        }
        linha = stream.ReadLine();
    }
}

class Musica
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; }
    public IEnumerable<string> Generos { get; set; }
    public DateTime Lancamento { get; set; }
    public int Total { get; set; }
}

