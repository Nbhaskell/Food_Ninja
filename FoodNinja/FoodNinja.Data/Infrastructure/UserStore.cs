using FoodNinja.Core.Domain;
using FoodNinja.Core.Infrastructure;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FoodNinja.Data.Infrastructure
{
    public class UserStore : Disposable,
                             IUserStore<NinjaUser, int>,
                             IUserPasswordStore<NinjaUser, int>,
                             IUserSecurityStampStore<NinjaUser, int>,
                             IUserRoleStore<NinjaUser, int>
    {
        private readonly IDatabaseFactory _databaseFactory;

        private NinjaDataContext _db;
        protected NinjaDataContext Db
        {
            get
            {
                return _db ?? (_db = _databaseFactory.GetDataContext());
            }
        }

        public UserStore(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region IUserStore
        public Task CreateAsync(NinjaUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() => {
                //user.NinjaUserId = Guid.NewGuid().ToString();
                Db.NinjaUsers.Add(user);
                Db.SaveChanges();
            });
        }

        public Task UpdateAsync(NinjaUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                Db.NinjaUsers.Attach(user);
                Db.Entry(user).State = EntityState.Modified;

                Db.SaveChanges();
            });
        }

        public Task DeleteAsync(NinjaUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                Db.NinjaUsers.Remove(user);
                Db.SaveChanges();
            });
        }

        public Task<NinjaUser> FindByIdAsync(int userId)
        {
            return Task.Factory.StartNew(() => Db.NinjaUsers.Find(userId));
        }

        public Task<NinjaUser> FindByNameAsync(string userName)
        {
            return Task.Factory.StartNew(() => Db.NinjaUsers.FirstOrDefault(u => u.UserName == userName));
        }
        #endregion

        #region IUserPasswordStore
        public Task SetPasswordHashAsync(NinjaUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(NinjaUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(NinjaUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }
        #endregion

        #region IUserSecurityStampStore
        public Task SetSecurityStampAsync(NinjaUser user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(NinjaUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }
        #endregion

        #region IUserRoleStore

        public Task AddToRoleAsync(NinjaUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            return Task.Factory.StartNew(() =>
            {
                // Check if the role even exists in the database
                if (!Db.Roles.Any(r => r.Name == roleName))
                {
                    // add it to the database
                    Db.Roles.Add(new Role
                    {
                        Name = roleName
                    });
                    Db.SaveChanges();
                }

                Db.UserRoles.Add(new UserRole
                {
                    RoleId = Db.Roles.FirstOrDefault(r => r.Name == roleName).Id,
                    UserId = user.Id
                });

                Db.SaveChanges();
            });
        }

        public Task RemoveFromRoleAsync(NinjaUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            return Task.Factory.StartNew(() =>
            {
                var userRole = user.UserRoles.FirstOrDefault(r => r.Role.Name == roleName);

                if (userRole == null)
                {
                    throw new InvalidOperationException("User does not have that role");
                }

                Db.UserRoles.Remove(userRole);

                Db.SaveChanges();
            });
        }

        public Task<IList<string>> GetRolesAsync(NinjaUser user)
        {
            return Task.Factory.StartNew(() =>
            {
                return (IList<string>)Db.Roles.Select(r => r.Name).ToList();
            });
        }

        public Task<bool> IsInRoleAsync(NinjaUser user, string roleName)
        {
            return Task.Factory.StartNew(() =>
            {
                return user.UserRoles.Any(r => r.Role.Name == roleName);
            });
        }

        #endregion
    }
}
