using System.Text.RegularExpressions;

using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

var linha = stream.ReadLine();
var partes = linha.Split(';');
int duracao = 0;
var texto = "The Broken Road;Rolling Stones;3:99;Rock, Blues Rock";
var regex = Regex.Match(texto, @"\d?\d:\d\d");

if (regex.Success)
{
    var minutos = int.Parse(regex.Groups[1].Value);
    var segundos = int.Parse(regex.Groups[2].Value);
    Console.WriteLine($"A duração da música é {duracao}");
    duracao = (minutos*60) + segundos;
}
if (partes.Length == 5)
{
    var musica = new Musica
    {
        Titulo = partes[0],
        Artista = partes[1],
        Duracao = duracao,
        Generos = partes[3].Split(',').Select(g => g.Trim()),
        Lancamento = DateTime.TryParse(partes[4], out var lancamentoConvertido) ? lancamentoConvertido : DateTime.Today
    };
}


void VerificarSenha()
{

    var senha = "123";
    var numerodeCaracteres = senha.Length;
    var senhaPossuiMaiuscula = senha.Count(a => char.IsUpper(a));
    var senhaPossuiMinuscula = senha.Count(a => char.IsLower(a));
    var senhaPossuiNumero = senha.Count(a => char.IsDigit(a));
    var senhaPossuiSimbolo = senha.Count(a => char.IsLetterOrDigit(a));

    if (numerodeCaracteres >= 8 ||
        senhaPossuiMaiuscula == 1 ||
        senhaPossuiMinuscula == 1 ||
        senhaPossuiNumero == 1 ||
        senhaPossuiSimbolo == 1)
    {
        Console.WriteLine("senha forte");
    }
    else
    {
        Console.WriteLine("senha fraca");
    }
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
        if (partes.Length == 5) {
        var musica = new Musica
        {
            Titulo = string.IsNullOrWhiteSpace(partes[0]) ? "Titulo não encontrado" : partes[0],
            Artista = string.IsNullOrEmpty(partes[1])?"Artista não encontrado" : partes[1],
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
}
