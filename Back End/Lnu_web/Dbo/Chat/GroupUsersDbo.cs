using Lnu_web.Dbo.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lnu_web.Dbo.Chat
{
    public class GroupUsersDbo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("Group")]
        public Guid GroupId { get; set; }
        public GroupDbo Group { get; set; }

        public Guid UserId { get; set; }
        public string User { get; set; }
    }
}
