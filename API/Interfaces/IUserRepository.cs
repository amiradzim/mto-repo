using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(MtoUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<MtoUser>> GetUsersAsync();
        Task<MtoUser> GetUserByIdAsync(int id);
        Task<MtoUser> GetUserByUsernameAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);
    }
}