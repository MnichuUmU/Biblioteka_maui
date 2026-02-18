using SQLite;


namespace Biblioteka_v1
{   
    
    [Table("ksiazka")]
    public class Ksiazka
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id {  get; set; }

        [Column("tytle")]
        public string Tytle { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("tags")]
        public string Tags { get; set; }

        [Column("client_id")]
        public int ClientId { get; set; }

        [Column("date")]
        public string Date { get; set; }
        [Column("stan")]
        public string Stan { get; set; }

    }
}
