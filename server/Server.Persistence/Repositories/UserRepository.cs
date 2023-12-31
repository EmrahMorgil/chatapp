﻿using Azure.Core;
using Dapper;
using MediatR;
using Server.Application.Dto;
using Server.Application.Features.Commands.GetAllMessages;
using Server.Application.Features.Commands.GetUsers;
using Server.Application.Features.Commands.Login;
using Server.Application.Features.Commands.UpdateUser;
using Server.Application.Interfaces.Repository;
using Server.Application.Password;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using Server.Persistence.Context;
using Server.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Server.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dbContext;
        public UserRepository(DapperContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AuthenticationResponse> CreateUser(User user)
        {
            var query = $"INSERT INTO [User] ({RepositoryHelper.GetInsertFields<User>()}) VALUES ({RepositoryHelper.GetInsertFieldParams<User>()})";
            var emailControl = "SELECT * FROM [User] WHERE email = @email";
            var newResponse = new AuthenticationResponse();

            using (var connection = _dbContext.CreateConnection())
            {
                var getUser = await connection.QueryFirstOrDefaultAsync<User>(emailControl, new { email = user.email });

                if (getUser != null)
                    newResponse.success = false;
                else
                {
                    var userControl = await connection.ExecuteAsync(query, user);
                    if (userControl != null)
                    {
                        newResponse.success = true;
                        newResponse.body = user;
                        newResponse.token = JwtService.GenerateToken(user.email);
                    }
                    else
                        newResponse.success = false;
                }
            }
            return newResponse;
        }

        public async Task<BaseResponse<List<UserViewDto>>> GetUsers(GetUsersCommand entity)
        {
            var newResponse = new BaseResponse<List<UserViewDto>>();
            var query = $"SELECT [id],[name],[image] FROM [User]";

            using (var connection = _dbContext.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { id = entity.id });
                if (user != null)
                {
                    var users = await connection.QueryAsync<UserViewDto>(query);
                    newResponse.body = users.ToList();
                }
            }

            if (newResponse.body != null)
                newResponse.success = true;
            else
                newResponse.success = false;

            return newResponse;
        }

        public async Task<AuthenticationResponse> LoginUser(LoginCommand user)
        {
            var query = "SELECT * FROM [User] WHERE email = @email";
            var newResponse = new AuthenticationResponse();

            using (var connection = _dbContext.CreateConnection())
            {
                var userControl = await connection.QueryFirstOrDefaultAsync<User>(query, new { email = user.email });
                if (userControl != null)
                {
                    newResponse.success = Encryption.VerifyPassword(user.password, userControl.password);
                    if (newResponse.success)
                    {
                        newResponse.body = userControl;
                        newResponse.token = JwtService.GenerateToken(user.email);
                    }
                }
                return newResponse;
            }
        }

        public async Task<BaseResponse<User>> UpdateUser(UpdateUserCommand user)
        {
            var query = $"UPDATE [User] SET name = '{user.name}', email = '{user.email}', password = '{user.password}', image = '{user.image}' WHERE id = '{user.id}';";
            var updatedUserQuery = $"SELECT * FROM [User] WHERE id = '{user.id}'";
            var newResponse = new BaseResponse<User>();

            using (var connection = _dbContext.CreateConnection())
            {
                var userControl = await connection.ExecuteAsync(query);
                var updatedUser = await connection.QueryFirstOrDefaultAsync<User>(updatedUserQuery, new { id = user.id });
                if (userControl != null && updatedUser != null)
                {
                    newResponse.success = true;
                    newResponse.body = updatedUser;
                }
                else
                    newResponse.success = false;
            }
            return newResponse;
        }
    }
}
