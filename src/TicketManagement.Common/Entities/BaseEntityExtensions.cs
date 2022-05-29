namespace TicketManagement.Common.Entities
{
    public static class BaseEntityExtensions
    {
        public static bool IsEmpty(this BaseEntity entity) =>
            entity.IsEmpty(entity);
        public static string StringForEquals(this BaseEntity entity) =>
            entity.StringForEquals(entity);
    }
}
