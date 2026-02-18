using SQLite;


namespace Biblioteka_v1
{

    [Table("logs")]
    public class Logs
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("book_id")]
        public int BookId { get; set; }

        [Column("acc_id")]
        public int AccountId { get; set; }

        [Column("date")]
        public string Date { get; set; }

        [Column("action")]
        public bool Action { get; set; }
        //true -> returned
        //false -> borrowed



    }
}
