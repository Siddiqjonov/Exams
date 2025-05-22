﻿using ContactMate.Core.Errors;
using ContactMate.Dal;
using ContactMate.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactMate.Repository.Services;

//public class UserRepositroy : IUserRepositroy
//{
//    private readonly MainContext MainContext;

//    public UserRepositroy(MainContext mainContext)
//    {
//        MainContext = mainContext;
//    }

//    public async Task DeleteUserById(long userId)
//    {
//        var user = await SelectUserByIdAsync(userId);
//        MainContext.Users.Remove(user);
//        await MainContext.SaveChangesAsync();
//    }

//    public async Task<long> InsertUserAsync(User user)
//    {
//        await MainContext.Users.AddAsync(user);
//        await MainContext.SaveChangesAsync();
//        return user.UserId;
//    }

//    public async Task<User> SelectUserByIdAsync(long userId)
//    {
//        var user = await MainContext.Users.Include(u => u.UserRole).FirstOrDefaultAsync(u => u.UserId == userId);
//        return user ?? throw new EntityNotFoundException($"User with userId {userId} not found");
//    }

//    public async Task<User> SelectUserByUserNameAsync(string userName)
//    {
//        var user = await MainContext.Users.Include(u => u.UserRole).FirstOrDefaultAsync(u => u.UserName == userName);
//        return user ?? throw new EntityNotFoundException($"User with {userName} not found");
//    }

//    public async Task UpdateUserRoleAsync(long userId, string UserRoleName)
//    {
//        var user = await SelectUserByIdAsync(userId);
//        user.UserRole.UserRoleName = UserRoleName;
//        MainContext.Users.Update(user);
//        await MainContext.SaveChangesAsync();
//    }
//}
