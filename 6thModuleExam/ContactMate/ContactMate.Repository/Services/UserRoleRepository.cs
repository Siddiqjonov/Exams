﻿using ContactMate.Core.Errors;
using ContactMate.Dal;
using ContactMate.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactMate.Repository.Services;

//public class UserRoleRepository : IUserRoleRepository
//{
//    private readonly MainContext MainContext;

//    public UserRoleRepository(MainContext mainContext)
//    {
//        MainContext = mainContext;
//    }

//    public async Task<long> InsertUserRoleAsync(UserRole userRole)
//    {
//        await MainContext.UserRoles.AddAsync(userRole);
//        await MainContext.SaveChangesAsync();
//        return userRole.UserRoleId;
//    }

//    public async Task<ICollection<UserRole>> SelectAllRolesAsync()
//    {
//        var userRoles = await MainContext.UserRoles.ToListAsync();
//        return userRoles;
//    }

//    public async Task<ICollection<User>> SelectAllUsersByRoleNameAsync(string roleName)
//    {
//        var users = await MainContext.Users.Include(u => u.UserRole).Where(u => u.UserRole.UserRoleName == roleName).ToListAsync();
//        return users;
//    }

//    public async Task<UserRole> SelectUserRoleByRoleName(string userRoleName)
//    {
//        var userRole = await MainContext.UserRoles.FirstOrDefaultAsync(uR => uR.UserRoleName == userRoleName);
//        return userRole == null ? throw new EntityNotFoundException($"Role with role name: {userRoleName} not found") : userRole;
//    }
//}
