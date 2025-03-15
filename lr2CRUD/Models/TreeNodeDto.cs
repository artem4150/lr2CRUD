namespace lr2CRUD.Models
{
    public class TreeNodeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NodeType { get; set; }
        public bool HasChildren { get; set; }
        public List<TreeNodeDto> Children { get; set; } = new List<TreeNodeDto>();

        // Новые свойства для связи с родителем
        public int ParentId { get; set; }
        public string ParentType { get; set; }
    }
    public class UpdateNodeDto
    {
        public string NodeType { get; set; }
        public int NodeId { get; set; }
        public int NewParentId { get; set; }
    }
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        // Добавьте дополнительные поля, если необходимо
    }

    public class OutfitDto
    {
        public int OutfitId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
    }

    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int OutfitId { get; set; }
    }
}
