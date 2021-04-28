using System;

namespace BLL.App.DTO.CustomDTO
{
    public class AddTeamDTO
    {
        public Guid[] Members { get; set; } = default!;
        public Team Team { get; set; } = default!;
    }

}