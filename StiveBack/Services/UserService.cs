﻿using Microsoft.AspNetCore.Identity;
using StiveBack.Database;
using StiveBack.Models;
using StiveBack.Ressources;

namespace StiveBack.Services
{
    public class UserService
    {
        private readonly MainDbContext _database;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService()
        {
            
        }

        public UserService(MainDbContext mainDbContext)
        {
            _database = mainDbContext;
            _passwordHasher = new PasswordHasher<User>();
        }


        /// <summary>
        /// Récupère tous les utilisateurs
        /// </summary>
        public List<UserRessource> SelectAll()
        {
            return _database.users.ToList().Select(user => UserToUserRessource(user)).ToList();
        }

        /// <summary>
        /// Récupère un utilisateur par son id
        /// </summary>
        /// <param name="id"></param>
        public UserRessource Select(int id)
        {
            User user = _database.users.FirstOrDefault(user => user.Id == id);
            return UserToUserRessource(user);
        }

        /// <summary>
        /// Ajoute un utilisateur
        /// </summary>
        /// <param name="userSaveRessource"></param>
        public UserRessource Add(UserSaveRessource userSaveRessource)
        {
            User user = UserSaveRessourceToUser(userSaveRessource);

            _database.users.Add(user);
            _database.SaveChanges();

            return UserToUserRessource(user);
        }

        /// <summary>
        /// Met à jour un utilisateur
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userSaveRessource"></param>
        public UserRessource Update(int id, UserSaveRessource userSaveRessource)
        {
            User user = _database.users.FirstOrDefault(user => user.Id == id);

            user.FirstName = userSaveRessource.FirstName;
            user.LastName = userSaveRessource.LastName;
            user.Email = userSaveRessource.Email;
            user.Address1 = userSaveRessource.Address1;
            user.Address2 = userSaveRessource.Address2;
            user.PostalCode = userSaveRessource.PostalCode;
            user.City = userSaveRessource.City;
            user.Password = _passwordHasher.HashPassword(user, userSaveRessource.Password);
            user.UserRole = userSaveRessource.RoleIds.Select(roleId => new UserRole
            {
                RoleId = roleId
            }).ToList();

            _database.SaveChanges();

            return UserToUserRessource(user);
        }

        /// <summary>
        /// Supprime un utilisateur par son id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            User user = _database.users.FirstOrDefault(user => user.Id == id);

            _database.users.Remove(user);
            _database.SaveChanges();
        }

        public User UserSaveRessourceToUser(UserSaveRessource userSaveRessource)
        {
            User user = new User
            {
                FirstName = userSaveRessource.FirstName,
                LastName = userSaveRessource.LastName,
                Email = userSaveRessource.Email,
                Address1 = userSaveRessource.Address1,
                Address2 = userSaveRessource.Address2,
                PostalCode = userSaveRessource.PostalCode,
                City = userSaveRessource.City,
                UserRole = userSaveRessource.RoleIds.Select(roleId => new UserRole
                {
                    RoleId = roleId
                }).ToList()
            };

            user.Password = _passwordHasher.HashPassword(user, userSaveRessource.Password);

            return user;
        }

        public UserRessource UserToUserRessource(User user)
        {
            UserRessource userRessource = new UserRessource
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address1 = user.Address1,
                Address2 = user.Address2,
                PostalCode = user.PostalCode,
                City = user.City,
                Roles = user.UserRole
                .Select(c => new RoleRessource { Id = c.Role.Id, Name = c.Role.Name })
                .ToList()
            };

            return userRessource;
        }
    }
}