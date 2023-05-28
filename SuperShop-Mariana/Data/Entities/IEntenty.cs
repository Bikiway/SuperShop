namespace SuperShop_Mariana.Data.Entities
{
    public interface IEntenty
    {
        int Id { get; set; } //Comum a todas as entidades

        //bool WasDeleted { get; set; } //Apagar registos, sem realmente apagar. Bool em falso e mudar para true para recupera-los.
    }
}
