namespace LightGame.Entity
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
