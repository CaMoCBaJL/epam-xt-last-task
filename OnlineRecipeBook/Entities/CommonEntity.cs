namespace Entities
{
    public abstract class CommonEntity // more like just Entity or EntityBase
    {
        public int Id { get; set; }


        public CommonEntity() { }

        public CommonEntity(int id) => Id = id;
    }
}
