using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PulseChat.Application.DTOs;
using PulseChat.Domain.Entities;

namespace PulseChat.Application.Interfaces;

//public interface IGroupService
//{
//    Task<IEnumerable<Group>> GetAllAsync();
//    Task<Group?> GetByIdAsync(Guid id);
//    Task<Group> CreateAsync(Group group);
//    Task<Group> CreateAsync(GroupCreateDto dto);


//    Task UpdateAsync(Guid id, GroupUpdateDto dto);

//    Task DeleteAsync(Guid id); // Eklenmeli
//}




    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetAllAsync();
        Task<Group?> GetByIdAsync(Guid id);
        Task<Group> CreateAsync(GroupCreateDto dto);
        Task UpdateAsync(Guid id, GroupUpdateDto dto);
        Task DeleteAsync(Guid id);
        Task<GroupDto> CreateGroupAsync(GroupCreateDto dto);
        Task<List<GroupDto>> GetAllGroupsAsync();
        //Task<List<Group>> GetAllGroupsAsync();



}

