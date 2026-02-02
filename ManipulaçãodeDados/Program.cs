var arquivo = new FileStream("musicas.csv",FileMode.Open, FileAccess.Read);
class Musica()
{
    public string Nome { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; }
}