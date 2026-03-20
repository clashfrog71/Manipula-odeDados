using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

var musicas = ObterMusicas(stream)
    .Take(50);

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
        var musica = new Musica
        {
            Titulo = partes[0],
            Artista = partes[1],
            Duracao = Convert.ToInt32(partes[2]),
            Generos = partes[3].Split(',').Select(g => g.Trim()),
            Lancamento = Convert.ToDateTime(partes[4])
        };
        yield return musica;
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
