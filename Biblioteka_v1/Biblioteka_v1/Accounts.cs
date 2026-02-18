using SQLite;


namespace Biblioteka_v1
{

    [Table("accounts")]
    public class Accounts
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FName { get; set; }

        [Column("last_name")]
        public string LName { get; set; }

        [Column("mail")]
        public string Mail { get; set; }

        [Column("pass")]
        public string Password { get; set; }

        [Column("admin")]
        public bool Admin { get; set; }

    }
}
