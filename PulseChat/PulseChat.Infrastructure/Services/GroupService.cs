//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using PulseChat.Application.Interfaces;
//using PulseChat.Domain.Entities;

//namespace PulseChat.Infrastructure.Services;

//public class GroupService : IGroupService
//{
//    private readonly IGroupRepository _groupRepository;

//    public GroupService(IGroupRepository groupRepository)
//    {
//        _groupRepository = groupRepository;
//    }

//    public async Task<IEnumerable<Group>> GetAllGroupsAsync()
//    {
//        return await _groupRepository.GetAllAsync();
//    }

//    public async Task<Group?> GetGroupByIdAsync(Guid id)
//    {
//        return await _groupRepository.GetByIdAsync(id);
//    }

//    public async Task CreateGroupAsync(Group group)
//    {
//        await _groupRepository.AddAsync(group);
//    }

//    public async Task<Group> CreateAsync(GroupCreateDto dto)
//    {
//        var group = new Group
//        {
//            Name = dto.Name,
//            IsPrivate = dto.IsPrivate
//        };

//        await _groupRepository.AddAsync(group);
//        return group;
//    }
//    public async Task UpdateAsync(Guid id, GroupUpdateDto dto)
//    {
//        var group = await _groupRepository.GetByIdAsync(id);
//        if (group == null)
//            throw new Exception("Group not found");

//        group.Name = dto.Name;
//        group.IsPrivate = dto.IsPrivate;

//        await _groupRepository.UpdateAsync(group);
//    }


//}


using PulseChat.Application.DTOs;
using PulseChat.Application.Interfaces;
using PulseChat.Domain.Entities;

namespace PulseChat.Infrastructure.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;

    public GroupService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<IEnumerable<Group>> GetAllAsync()
    {
        return await _groupRepository.GetAllAsync();
    }

    public async Task<Group?> GetByIdAsync(Guid id)
    {
        return await _groupRepository.GetByIdAsync(id);
    }

    public async Task<Group> CreateAsync(GroupCreateDto dto)
    {
        var group = new Group
        {
            Name = dto.Name,
            IsPrivate = dto.IsPrivate
        };

        await _groupRepository.AddAsync(group);
        return group;
    }

    public async Task UpdateAsync(Guid id, GroupUpdateDto dto)
    {
        var existing = await _groupRepository.GetByIdAsync(id);
        if (existing is null)
            throw new Exception("Group not found");

        existing.Name = dto.Name;
        existing.IsPrivate = dto.IsPrivate;

        await _groupRepository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _groupRepository.DeleteAsync(id);
    }


    public async Task<GroupDto> CreateGroupAsync(GroupCreateDto dto)
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };

        await _groupRepository.AddAsync(group);

        return new GroupDto
        {
            Id = group.Id,
            Name = group.Name
        };
    }
    public async Task<List<GroupDto>> GetAllGroupsAsync()
    {
        var groups = await _groupRepository.GetAllAsync();
        return groups.Select(g => new GroupDto
        {
            Id = g.Id,
            Name = g.Name
        }).ToList();
    }




}
