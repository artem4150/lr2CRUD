using System.Collections.Generic;

namespace MyProject.Api.Models
{
    public class TreeNodeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Тип узла: "User", "Outfit", "Comment"
        public string NodeType { get; set; }
        // Флаг наличия дочерних узлов (для отображения плюса)
        public bool HasChildren { get; set; }
        public List<TreeNodeDto> Children { get; set; } = new List<TreeNodeDto>();
    }

    public class UpdateNodeDto
    {
        // Например, для изменения родительской связи у Outfit или Comment
        public string NodeType { get; set; }
        public int NodeId { get; set; }
        public int NewParentId { get; set; }
    }
}
