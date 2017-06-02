using UFilm.Domain.Adults;

namespace UFilm.Console.Models.Adults
{
    public class ActorsEditModel : ModelBase
    {
        public int AvatarId { get; set; }
        public Actor Actor { get; set; }
    }
}